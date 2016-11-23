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
using System.Globalization;
using CheckAppCore.Models;
using System.Security.Claims;

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

        public async Task<IActionResult> GetAgendaSchedule([FromQuery] int professionalid, [FromQuery] string jsdate)
        {            
            var agenda = _context.Agenda.Include(o => o.AppointmentType).FirstOrDefault(o => o.ProfessionalID == professionalid);
            if (agenda == null)
                return new EmptyResult();

            var date = DateTime.Parse(jsdate, CultureInfo.InvariantCulture);
            var agendaSchedule = new AgendaService(new AgendaRepository(_context)).GetAgendaScheduleRange(agenda.ID, date);

            return new JsonResult(await agendaSchedule);
        }
        
        public async Task<IActionResult> Confirm([FromBody] AgendamentoModel agendamento)
        {
            var agendaRepository = new AgendaRepository(_context);
            var userRepository = new UserRepository(_context);

            var authenticadedUser = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            var user = await userRepository.GetUserFromEmailOrOauthID(authenticadedUser);

            if (user == null)
                return NotFound("Usuário não encontrado");

            if (agendaRepository.IncludeAgendamento(user, agendamento))
                return Ok();

            return NotFound();
        }

        public async Task<IActionResult> GetUserAgendaAppointment()
        {
            var agendaRepository = new AgendaRepository(_context);
            var agendaService = new AgendaService(agendaRepository);
            var userRepository = new UserRepository(_context);

            var authenticadedUser = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            var user = await userRepository.GetUserFromEmailOrOauthID(authenticadedUser);

            if (user == null)
                return NotFound("Usuário não encontrado");
            
            return Json(agendaService.GetUserAgendaAppointment(user).Result);
        }
    }
}
