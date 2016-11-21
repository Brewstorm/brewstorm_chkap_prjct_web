using CheckAppCore.Data;
using CheckAppCore.Models;
using CheckAppCore.Repositories;
using CheckAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CheckAppCore.Controllers
{
    public class LoginController : Controller
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

        public ActionResult RegisterUser([FromBody] RegisterModel postdata)
        {
            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            if (loginService.RegisterUser(postdata))
                return Ok();
            else
                return Unauthorized();
        }

        [Authorize]
        public IActionResult GetUserData()
        { 
            var subject = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            if(subject == null)
                return new EmptyResult();

            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            return new JsonResult(loginService.GetUserData(subject));
        }
    }
}
