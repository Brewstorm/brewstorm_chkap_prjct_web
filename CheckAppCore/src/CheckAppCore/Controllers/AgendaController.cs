using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.Repositories;
using CheckAppCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckAppCore.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        private readonly CheckAppContext _context;

        public AgendaController(CheckAppContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> GetProfessionalInfo([FromQuery]int id)
        {
            var professional = await _context.Professionals.Include(o => o.PersonalInfo)
                                                           .FirstOrDefaultAsync(o => o.ID == id);

            if (professional == null)
                return new EmptyResult();

            var agenda = await _context.Agenda.Include(o => o.AppointmentType).FirstOrDefaultAsync(o => o.ProfessionalID == id);

            if(agenda == null)
                return new EmptyResult();

            return new JsonResult(new
            {
                professional.PersonalInfo.SrcPhoto,
                professional.PersonalInfo.Name,
                professional.Endereco,
                professional.Bairro,
                agenda.Value,
                agenda.AppointmentType.ProfessionalName
            });
        }

        public async Task<IActionResult> GetAgendaSchedule() //[FromQuery] int professionalid, [FromQuery] string date
        {
            var agenda = await _context.Agenda.Include(o => o.AppointmentType).FirstOrDefaultAsync(o => o.ProfessionalID == 1);

            if (agenda == null)
                return new EmptyResult();

            var agendaSchedule = new AgendaService(new AgendaRepository(_context)).GetAgendaScheduleRange(agenda.ID, DateTime.Today);

            return new JsonResult(await agendaSchedule);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
