using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsPortal.Login;
using NewsPortal.Models.CSharpModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private DAL dal = new DAL();

        // GET api/values
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            string salt;

            try
            {
                salt = dal.GetSaltForUser(user.UserName);
            }
            catch (NotFoundException e)
            {
                return Unauthorized();
            }

            var saltedHashedClaimedPassword = sha256(user.Password+salt).ToUpper();

            if (dal.LoginCorrect(user.UserName, saltedHashedClaimedPassword))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.secretKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: Startup.url,
                    audience: Startup.url,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
