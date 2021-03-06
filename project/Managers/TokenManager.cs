using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Managers {
    public class TokenManager {
        private RNGCryptoServiceProvider RNGCSP = new();
        private SymmetricSecurityKey Key;
        private JwtSecurityTokenHandler Handler = new();
        private int AccessLifetime;

        private static long CurrentTimestamp(int offset = 0) {
            return new DateTimeOffset(DateTime.Now.AddSeconds(offset)).ToUnixTimeSeconds();
        }

        public TokenManager(TokenManagerConfig config) {
            if (config is null) {
                config = new();
            }
            
            Handler.InboundClaimTypeMap.Clear();//[JwtRegisteredClaimNames.Sub] 
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
            AccessLifetime = config.AccessLifetime;
        }

        public string Make() {
            byte[] tokenData = new byte[768];
            RNGCSP.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }

        public (string, long) Encode(AccessToken accessToken) {
            long expiresAt = CurrentTimestamp(AccessLifetime);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, accessToken.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, CurrentTimestamp().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString()),
                new Claim("email", accessToken.Email),
                new Claim("role", accessToken.UserRole.ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        Key,
                        SecurityAlgorithms.HmacSha256
                    )
                ),
                new JwtPayload(claims)
            );

            return (Handler.WriteToken(token), expiresAt);
        }

        public AccessToken Decode(string accessToken) {
            var claims = Handler.ValidateToken(
                accessToken,
                new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = Key,
                    ValidateIssuer = false,
                    ValidateAudience = false, 
                    ClockSkew = TimeSpan.FromMinutes(1),
                },
                out SecurityToken securityToken
            );

            return new AccessToken {
                UserId = long.Parse(claims.FindFirstValue(JwtRegisteredClaimNames.Sub)),
                Email = claims.FindFirstValue("email"),
                UserRole = Enum.Parse<UserRoleId>(claims.FindFirstValue("role")),
            };
        }
    }
}