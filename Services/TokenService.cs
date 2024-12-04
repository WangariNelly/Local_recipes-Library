using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace LocalRecipes.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration) {
            this.configuration = configuration;
        }
      public string GenerateToken(Guid userId, string username)
        {
//             var claims = new[]
//             {
//                 new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
//                 new Claim(JwtRegisteredClaimNames.UniqueName, username),
//                 new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(12).ToString()),
// };
                
//             };

var claims = new[]
{
    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
    new Claim(JwtRegisteredClaimNames.UniqueName, username),
    new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(12).ToString())
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                audience: configuration["Jwt:Audience"],
                issuer: configuration["Jwt:Issuer"],   
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        
    }
}