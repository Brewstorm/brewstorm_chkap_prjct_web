using System;
using System.IdentityModel.Tokens.Jwt;
using CheckAppCore.Data;
using CheckAppCore.Models;
using CheckAppCore.Repositories;
using CheckAppCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CheckAppCore.Controllers
{
    public class LoginController : ApiController
    {
        private readonly CheckAppContext _context;

        public LoginController(CheckAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var oauth_id = ActionContext.HttpContext.Request.Form["oauth_id"];
            var username = ActionContext.HttpContext.Request.Form["username"];
            var password = ActionContext.HttpContext.Request.Form["password"];

            var userRepo = new UserRepository(_context);
            var isAuth = userRepo.AuthenticateUser(username, password);
            var isFbAuth = userRepo.AuthenticateFBUser(oauth_id);

            if (!isAuth && !isFbAuth)
            {
                await ActionContext.HttpContext.Response.WriteAsync("Invalid username or password.");
                return new UnauthorizedResult();
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, string.IsNullOrEmpty(username) ? oauth_id : username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Math.Round((now.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds).ToString(), ClaimValueTypes.Integer64)
            };

            var secretKey = Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
            var _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: "https://checkapptest.azurewebsites.net/",
                audience: "https://checkapptest.azurewebsites.net/",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(5).TotalSeconds
            };

            //await context.Authentication.SignInAsync("Cookie", new ClaimsPrincipal(identity));

            // Serialize and return the response
            //ActionContext.HttpContext.Response.ContentType = "application/json";
            return new JsonResult(response);
        }

        public async Task<IActionResult> FbCredentials([FromQuery]string fb_id)
        {
            if (await _context.Users.AnyAsync(o => o.FacebookID == fb_id))
                return new OkResult();
            else
                return new NotFoundResult();
        }

        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel postdata)
        {
            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            if (loginService.RegisterUser(postdata))
                return Ok();

            return NotFound();
        }
        
        public async Task<IActionResult> GetUserData()
        { 
            var subject = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            if(subject == null)
                return new EmptyResult();

            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            return new JsonResult(loginService.GetUserData(subject));
        }

        public async Task<IActionResult> Logout()
        {
            await ActionContext.HttpContext.Authentication.SignOutAsync("Cookie");

            return Ok();
        }        
    }
}
