using System.Linq;
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

        public async Task<IActionResult> GetProfessionals([FromQuery] int? appTypeId)
        {
            if (appTypeId.HasValue)
            {
                return new JsonResult(await _context.ProfessionalsAppointmentTypes
                                                    .Include(p => p.Professional)
                                                    .Include(p => p.Professional.PersonalInfo)
                                                    .Where(o => o.AppointmentType.ID == appTypeId.Value)
                                                    .Select(pat => new { pat.Professional.ID, pat.Professional.PersonalInfo.Name, pat.Professional.PersonalInfo.SrcPhoto})
                                                    .ToListAsync());
            }

            return new EmptyResult();
        }
    }
}
