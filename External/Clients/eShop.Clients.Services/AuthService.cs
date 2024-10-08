﻿using eShop.Clients.Auth.Jwt.Helpers;
using eShop.Clients.Auth.Jwt.Settings;
using eShop.Clients.Domain;
using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.Models;
using eShop.Clients.Interfaces.Repositories;
using eShop.Clients.Interfaces.Services;
using eShop.Clients.Interfaces.ServicesExternal;
using Microsoft.Extensions.Options;
using NUlid;
using System.Security.Claims;

namespace eShop.Clients.Services;

internal class AuthService(
    IClientRepository clientRepository,
    ITokenRepository tokenRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IEmailSender emailSender,
    ISmsSender smsSender,
    IOptions<JWTSettings> options) : IAuthService
{
    private readonly JWTSettings jwtSettings = options.Value ?? throw new Exception($"Секция настроек не определена: 'JWTSettings'");

    public async Task<Result<bool>> AuthByPhone(long phone)
    {
        var clientResult = await clientRepository.GetClientByPhoneAsync(phone);
        if(!clientResult.Success)
        {
            return clientResult.MapTo<Client, bool>();
        }
        var client = clientResult.Value;

        var tokenResult = await tokenRepository.CreateAsync(client.ClientId, TokenTypes.Auth, phone.ToString());
        if (!tokenResult.Success)
        {
            return new Result<bool>
            {
                Value = false,
                Success = tokenResult.Success,
                Type = tokenResult.Type,
                Detail = tokenResult.Detail,
                Errors = tokenResult.Errors
            };
        }

        await smsSender.SendAsync(client.Phone, $@"Код подтверждения для входа: '{tokenResult.Value}'");

        return Results.Ok(true);
    }

    public async Task<Result<bool>> AuthByEmail(string email)
    {
        var clientResult = await clientRepository.GetClientByEmailAsync(email);
        if (!clientResult.Success)
        {
            return clientResult.MapTo<Client, bool>();
        }
        var client = clientResult.Value;

        var tokenResult = await tokenRepository.CreateAsync(client.ClientId, TokenTypes.Auth, email);
        if (!tokenResult.Success)
        {
            return tokenResult.MapTo<int, bool>();
        }

        await emailSender.SendAsync(client.Email,
            "[eShop] Код подтверждения для входа",
            $@"Код подтверждения для входа: '{tokenResult.Value}'");

        return Results.Ok(true);
    }

    private string CreateJwt(Client client)
    {
        var claims = new List<Claim>
        {
            new Claim(ClientClaims.UserName, client.UserName),
            new Claim(ClientClaims.ClientId, client.ClientId.ToString()),
            new Claim(ClientClaims.Phone, client.Phone.ToString()),
            new Claim(ClientClaims.Email, client.Email),
        };

        string jwtToken = TokenHelper.WriteToken(claims, jwtSettings.Secret, jwtSettings.ExpirationInMinutes);

        return jwtToken;
    }

    public async Task<Result<AuthModel>> AcceptAuthToken(long clientId, int tokenId)
    {
        var clientResult = await clientRepository.GetClientByIdAsync(clientId);
        if (!clientResult.Success)
        {
            return clientResult.MapTo<Client, AuthModel>();
        }
        var tokenResult = await tokenRepository.GetTokenAsync(clientId, tokenId);
        if (!tokenResult.Success)
        {
            return tokenResult.MapTo<ClientToken, AuthModel>();
        }

        var token = tokenResult.Value;

        if (token.TokenType == TokenTypes.Auth)
        {
            Ulid refreshTokenId = await refreshTokenRepository.CreateAsync(clientId);
            return new AuthModel
            {
                RefreshToken = refreshTokenId,
                JwtToken = CreateJwt(clientResult.Value)
            };
        }
        else
        {
            return Results.InvalidOperation<AuthModel>(
                $"Токен({clientId}, {tokenId}) с типом '{token.TokenType}' не поддерживается в этом методе");
        }
    }

    public async Task<Result<AuthModel>> AuthByEmailAndPassword(string email, string password)
    {
        var clientResult = await clientRepository.GetClientByEmailAsync(email);
        if (!clientResult.Success)
        {
            return clientResult.MapTo<Client, AuthModel>();
        }
        var client = clientResult.Value;

        if (!PasswordHasher.String.Verify(password, client.Password.Hash, client.Password.Salt))
        {
            return Results.InvalidOperation<AuthModel>($"Неправильный пароль");
        }

        Ulid refreshTokenId = await refreshTokenRepository.CreateAsync(client.ClientId);

        return new AuthModel
        {
            RefreshToken = refreshTokenId,
            JwtToken = CreateJwt(client)
        };
    }

    public async Task<Result<AuthModel>> RefreshToken(Ulid refreshTokenId)
    {
        var refreshTokenResult = await refreshTokenRepository.GetByIdAsync(refreshTokenId);
        if (!refreshTokenResult.Success)
        {
            return refreshTokenResult.MapTo<RefreshToken, AuthModel>();
        }

        var refreshToken = refreshTokenResult.Value;
        var clientResult = await clientRepository.GetClientByIdAsync(refreshToken.ClientId);
        if (!clientResult.Success)
        {
            return clientResult.MapTo<Client, AuthModel>();
        }
        var client = clientResult.Value;

        Ulid newRefreshTokenId = await refreshTokenRepository.CreateAsync(client.ClientId);

        return new AuthModel
        {
            RefreshToken = newRefreshTokenId,
            JwtToken = CreateJwt(client)
        };
    }
}
