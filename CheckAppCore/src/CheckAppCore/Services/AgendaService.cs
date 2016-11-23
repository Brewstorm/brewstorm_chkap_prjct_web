using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.DTO;
using CheckAppCore.Models;
using CheckAppCore.Repositories;
using CheckAppCore.Repositories.Interfaces;

namespace CheckAppCore.Services
{
    public class AgendaService
    {
        private readonly AgendaRepository _agendaRepository;
        public AgendaService(AgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        public async Task<IList<AgendaScheduleDTO>> GetAgendaScheduleRange(int agendaID, DateTime date)
        {
            return await _agendaRepository.GetAgendaSchedules(agendaID, date);
        }

        public async Task<IList<AgendaAppointmentDTO>> GetUserAgendaAppointment(User user)
        {
            var list = _agendaRepository.GetUserAgendaAppointment(user);

            return list.Select(o =>
                            new AgendaAppointmentDTO
                            {
                                ID = o.ID,
                                Date = o.DateSchedule,
                                Professional = o.AgendaSchedule.Agenda.Professional.PersonalInfo.Name,
                                AppointmentType = o.AgendaSchedule.Agenda.AppointmentType.Name,
                                BeginTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.BeginTime).ToString("HH:mm"),
                                EndTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.EndTime).ToString("HH:mm")                                
                            }).ToList();
        }
    }
}
