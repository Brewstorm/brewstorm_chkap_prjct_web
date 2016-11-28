using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.DTO;
using CheckAppCore.Enumerators;
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
                                Professional = o.AgendaSchedule.Agenda.Professional.User.PersonalInfo.Name,
                                AppointmentType = o.AgendaSchedule.Agenda.AppointmentType.Name,
                                BeginTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.BeginTime).ToString("HH:mm"),
                                EndTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.EndTime).ToString("HH:mm"),
                                Status = o.Status.GetHashCode()                       
                            }).ToList();
        }

        public async Task<IList<ProfessionalScheduleDTO>> GetProfessionalUserSchedules(User user, DateTime date)
        {
            var list = _agendaRepository.GetProfessionalUserSchedules(user, date);

            list = list.OrderBy(o => o.DateSchedule).ThenBy(o => o.BeginTime).ToList();

            return list.Select(o =>
                            new ProfessionalScheduleDTO
                            {
                                ID = o.ID,
                                Date = o.DateSchedule,
                                Patient = o.User.PersonalInfo.Name,
                                PhotoUrl = !string.IsNullOrEmpty(o.User.PersonalInfo.SrcPhoto) ? o.User.PersonalInfo.SrcPhoto : PersonalInfo.DEFAULT_PHOTO_URL,
                                AppointmentType = o.AgendaSchedule.Agenda.AppointmentType.Name,
                                BeginTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.BeginTime).ToString("HH:mm"),
                                EndTime = AgendaRepository.ConvertMinutesToDateTime(o.DateSchedule, o.EndTime).ToString("HH:mm"),
                                Status = o.Status.GetHashCode()
                            }).ToList();
        }

        public Task<bool> ConfirmSchedule(User user, int scheduleId)
        {
            try
            {
                return Task.FromResult(_agendaRepository.ChangeScheduleStatus(user, scheduleId));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> CompleteSchedule(User user, int scheduleId, StatusAgendamento status)
        {
            try
            {
                return Task.FromResult(_agendaRepository.ChangeScheduleStatus(user, scheduleId, status));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
