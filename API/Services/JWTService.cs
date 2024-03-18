using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class JWTService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtkey;
        private readonly UserManager<User> _userManager;
        public JWTService(IConfiguration config,
        UserManager<User> userManager)
        {
            _config = config;
            _jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            _userManager = userManager;
        }
        public async Task<string> CreateJWT(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Name,user.UserName),
            };
            var roles = await _userManager.GetRolesAsync(user);
            userClaims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));
            var credentials = new SigningCredentials(_jwtkey,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInSeconds"])),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwt);
        }
    }
}