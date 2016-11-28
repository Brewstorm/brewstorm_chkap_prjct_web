using System;
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
using CheckAppCore.Enumerators;

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
            var professional = await _context.Professionals.Include("User.PersonalInfo").FirstOrDefaultAsync(o => o.ID == id);

            if (professional == null)
                return new EmptyResult();

            var agenda = await _context.Agenda.Include(o => o.AppointmentType).FirstOrDefaultAsync(o => o.ProfessionalID == id);

            if(agenda == null)
                return new EmptyResult();

            return new JsonResult(new
            {
                professional.User.PersonalInfo.SrcPhoto,
                professional.User.PersonalInfo.Name,
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

        public async Task<IActionResult> GetProfessionalSchedule([FromQuery] string jsdate)
        {
            var agendaRepository = new AgendaRepository(_context);
            var agendaService = new AgendaService(agendaRepository);
            var userRepository = new UserRepository(_context);
            var date = DateTime.Parse(jsdate, CultureInfo.InvariantCulture);

            var authenticadedUser = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            var user = await userRepository.GetUserFromEmailOrOauthID(authenticadedUser);

            if (user == null)
                return NotFound("Usuário não encontrado");

            return Json(agendaService.GetProfessionalUserSchedules(user, date).Result);
        }

        public async Task<IActionResult> ConfirmSchedule([FromQuery] int scheduleId)
        {
            var agendaRepository = new AgendaRepository(_context);
            var agendaService = new AgendaService(agendaRepository);
            var userRepository = new UserRepository(_context);

            var authenticadedUser = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            var user = await userRepository.GetUserFromEmailOrOauthID(authenticadedUser);

            if (user == null)
                return NotFound("Usuário não encontrado");

            if (agendaService.ConfirmSchedule(user, scheduleId).Result)
                return Ok();

            return NotFound("Agendamento não encontrado");
        }

        public async Task<IActionResult> CompleteSchedule([FromQuery] int scheduleId, [FromQuery] StatusAgendamento status)
        {
            var agendaRepository = new AgendaRepository(_context);
            var agendaService = new AgendaService(agendaRepository);
            var userRepository = new UserRepository(_context);

            var authenticadedUser = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault()?.Value;
            var user = await userRepository.GetUserFromEmailOrOauthID(authenticadedUser);

            if (user == null)
                return NotFound("Usuário não encontrado");

            if (agendaService.CompleteSchedule(user, scheduleId, status).Result)
                return Ok();

            return NotFound("Agendamento não encontrado");
        }
    }
}
