using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OFM.WebApi
{
    public class TokenGenerator
    {
        public string GenereteToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mollamollamolla."));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Member"));
            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost",
                                                        audience: "http://localhost",
                                                        notBefore: System.DateTime.Now,
                                                        expires: System.DateTime.Now.AddMinutes(4),
                                                        signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
