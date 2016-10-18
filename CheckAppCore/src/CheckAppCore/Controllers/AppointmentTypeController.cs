using System.Threading.Tasks;
using CheckAppCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckAppCore.Controllers
{
    [Authorize]
    public class AppointmentTypeController : Controller
    {
        private readonly CheckAppContext _context;
        
        public AppointmentTypeController(CheckAppContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return new JsonResult(await _context.AppointmentTypes.ToListAsync());
        }
    }
}
