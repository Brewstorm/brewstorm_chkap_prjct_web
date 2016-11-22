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
using System.Threading.Tasks;
using System.Web.Http;

namespace CheckAppCore.Controllers
{
    public class LoginController : ApiController
    {
        private readonly CheckAppContext _context;

        public LoginController(CheckAppContext context)
        {
            _context = context;
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
            else
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
