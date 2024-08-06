﻿using eShop.Clients.Domain;
using eShop.Clients.Interfaces.Models;
using eShop.Clients.Interfaces.Repositories;
using eShop.Clients.Interfaces.Services;
using eShop.Clients.Interfaces.ServicesExternal;
using NUlid;

namespace eShop.Clients.Services;

internal class AuthService(
    IClientRepository clientRepository,
    ITokenRepository tokenRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IEmailSender emailSender,
    ISmsSender smsSender) : IAuthService
{
    public async Task<Result<bool>> AuthByPhone(long phone)
    {
        var client = await clientRepository.GetClientByPhoneAsync(phone);
        if (client == null)
        {
            return Results.NotFound<bool>($"Клиент с этим номером телефона не найден");
        }

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
        var client = await clientRepository.GetClientByEmailAsync(email);
        if (client == null)
        {
            return Results.NotFound<bool>($"Клиент с этим адресом почты не найден");
        }

        var tokenResult = await tokenRepository.CreateAsync(client.ClientId, TokenTypes.Auth, email);
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

        await emailSender.SendAsync(client.Email,
            "[eShop] Код подтверждения для входа",
            $@"Код подтверждения для входа: '{tokenResult.Value}'");

        return Results.Ok(true);
    }

    public async Task<Result<AuthModel>> AcceptAuthToken(long clientId, int tokenId)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);

        if (client == null)
        {
            return Results.NotFound<AuthModel>($"Клиент '{clientId}' не найден");
        }

        var tokenResult = await tokenRepository.GetTokenAsync(clientId, tokenId);
        if (!tokenResult.Success)
        {
            return Results.NotFound<AuthModel>($"Токен ('{clientId}', '{tokenId}') не найден");
        }

        var token = tokenResult.Value;

        if (token.TokenType == TokenTypes.Auth)
        {
            //Создаем Jwt


            Ulid refreshTokenId = await refreshTokenRepository.CreateAsync(clientId);

            return new AuthModel
            {
                RefreshToken = refreshTokenId,
                //JwtToken = Jwt
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
        var client = await clientRepository.GetClientByEmailAsync(email);

        if (client == null)
        {
            return Results.NotFound<AuthModel>($"Клиент '{email}' не найден");
        }

        if (!PasswordHasher.String.Verify(password, client.Password.Hash, client.Password.Salt))
        {
            return Results.InvalidOperation<AuthModel>($"Неправильный пароль");
        }

        //Создаем Jwt


        Ulid refreshTokenId = await refreshTokenRepository.CreateAsync(client.ClientId);

        return new AuthModel
        {
            RefreshToken = refreshTokenId,
            //JwtToken = Jwt
        };
    }

    public async Task<Result<AuthModel>> RefreshToken(Ulid refreshTokenId)
    {
        var refreshTokenResult = await refreshTokenRepository.GetByIdAsync(refreshTokenId);

        if (refreshTokenResult == null)
        {
            return Results.NotFound<AuthModel>($"Refresh токен '{refreshTokenId}' не найден");
        }

        var refreshToken = refreshTokenResult.Value;

        var client = await clientRepository.GetClientByIdAsync(refreshToken.ClientId);

        if (client == null)
        {
            return Results.NotFound<AuthModel>($"Клиент '{refreshToken.ClientId}' не найден");
        }

        //Создаем Jwt


        Ulid newRefreshTokenId = await refreshTokenRepository.CreateAsync(client.ClientId);

        return new AuthModel
        {
            RefreshToken = newRefreshTokenId,
            //JwtToken = Jwt
        };
    }
}