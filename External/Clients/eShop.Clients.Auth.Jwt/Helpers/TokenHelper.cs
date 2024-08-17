using eShop.Clients.Auth.Jwt.Settings;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.Clients.Auth.Jwt.Helpers;

public static class TokenHelper
{
    public static JwtSecurityToken CreateToken(List<Claim> authClaims, string secret, int expirationInMinutes)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            //issuer: "Test",
            claims: authClaims,
            expires: DateTime.Now.AddMinutes(expirationInMinutes),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public static string WriteToken(List<Claim> authClaims, string secret, int expirationInMinutes)
    {
        var JwtSecurityToken = CreateToken(authClaims, secret, expirationInMinutes);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(JwtSecurityToken);

        return token;
    }
}
