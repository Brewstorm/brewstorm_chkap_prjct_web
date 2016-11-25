using CheckAppCore.Data;
using CheckAppCore.Models;
using CheckAppCore.Repositories;
using CheckAppCore.Services;
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

        public IActionResult Index()
        {
            return Ok();
        }
        
        public async Task<IActionResult> FbCredentials([FromQuery]string fb_id)
        {
            if (await _context.Users.AnyAsync(o => o.FacebookID == fb_id))
                return new OkResult();

            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel postdata)
        {
            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            if (loginService.RegisterUser(postdata))
                return Ok();

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserData()
        { 
            var subject = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            if(subject == null)
                return new EmptyResult();

            var userRepository = new UserRepository(_context);
            var loginService = new LoginService(userRepository);

            return new JsonResult(loginService.GetUserData(subject));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");

            return Ok();
        }        
    }
}
