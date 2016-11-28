using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.DTO;
using CheckAppCore.Enumerators;
using CheckAppCore.Models;
using CheckAppCore.Services;
using Microsoft.EntityFrameworkCore;

namespace CheckAppCore.Repositories
{
    public class AgendaRepository : EntityFrameworkRepository<CheckAppContext, Agenda>
    {
        public AgendaRepository(CheckAppContext context)
            : base(context)
        { }

        public async Task<IList<AgendaScheduleDTO>> GetAgendaSchedules(int agendaId, DateTime date)
        {
            var agendaSchedule = await Context.AgendaSchedules.Include(o => o.AgendaExceptions).FirstOrDefaultAsync(o => o.AgendaID == agendaId);

            if(agendaSchedule == null)
                return new List<AgendaScheduleDTO>();

            return GetAgendaScheduleRange(agendaSchedule, date);
        }

        public List<AgendaScheduleDTO> GetAgendaScheduleRange(AgendaSchedule agendaSchedule, DateTime? date = null)
        {
            var scheduleList = new List<AgendaScheduleDTO>();

            if (date.HasValue)
                date = GetCleanDateTime(date.Value);

            for (var i = agendaSchedule.BeginTime; i < agendaSchedule.EndTime; i += agendaSchedule.IntervalTime)
            {
                var agendaException = Context.AgendaExceptions.FirstOrDefault(o => o.AgendaScheduleID == agendaSchedule.ID && (i >= o.BeginTime && i < o.EndTime));
                var agendaAppointment = Context.AgendaAppointments.FirstOrDefault(o => o.AgendaScheduleID == agendaSchedule.ID && o.DateSchedule == date.Value && (i >= o.BeginTime && i < o.EndTime));

                if (agendaException != null)
                {
                    i = agendaException.EndTime;
                    continue;
                }

                if(agendaAppointment != null)
                {
                    continue;
                }

                scheduleList.Add(new AgendaScheduleDTO { AgendaScheduleID = agendaSchedule.ID, Date = agendaSchedule.Date, BeginTime = ConvertMinutesToDateTime(agendaSchedule.Date, i).ToString("HH:mm"), EndTime = ConvertMinutesToDateTime(agendaSchedule.Date, i + agendaSchedule.IntervalTime).ToString("HH:mm") });
            }

            return scheduleList;
        }

        public bool IncludeAgendamento(User user, AgendamentoModel agendamento)
        {
            try
            {
                var dateSchedule = DateTime.Parse(agendamento.DateSchedule);
                var agendaApp = new AgendaAppointment
                {
                    UserID = user.ID,
                    AgendaScheduleID = agendamento.AgendaScheduleID,
                    DateSchedule = GetCleanDateTime(dateSchedule),
                    BeginTime = ConvertTimeToMinutes(agendamento.BeginTime),
                    EndTime = ConvertTimeToMinutes(agendamento.EndTime)
                };

                Context.AgendaAppointments.Add(agendaApp);
                Context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool CreateDefaultAgendaByProfessional(Professional professional)
        {
            try
            {
                var agenda = new Agenda
                {
                    Name = "Principal",
                    Professional = professional,
                    AppointmentTypeID = professional.ProfessionalAppointmentTypes.FirstOrDefault().AppointmentTypeId,
                    Value = 100,
                    AgendaSchedule = new AgendaSchedule
                    {
                        BeginTime = 480,
                        EndTime = 1080,
                        Date = DateTime.Today,
                        IntervalTime = 30
                    }
                };

                Context.Agenda.Add(agenda);
                Context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AgendaAppointment> GetUserAgendaAppointment(User user)
        {
            return Context.AgendaAppointments
                            .Include(o => o.AgendaSchedule.Agenda.AppointmentType)
                            .Include(o => o.AgendaSchedule.Agenda.Professional.User.PersonalInfo)
                            .Where(o => o.UserID == user.ID).ToList();
        }

        public List<AgendaAppointment> GetProfessionalUserSchedules(User user, DateTime date)
        {
            date = GetCleanDateTime(date);

            return Context.AgendaAppointments
                            .Include(o => o.AgendaSchedule.Agenda.AppointmentType)
                            .Include(o => o.User.PersonalInfo)
                            .Where(o => o.AgendaSchedule.Agenda.ProfessionalID == user.ProfessionalID.Value &&
                                        o.DateSchedule == date)
                            .ToList();
        }

        public static DateTime ConvertMinutesToDateTime(DateTime date, int minutes)
        {
            return date.AddMinutes(minutes);
        }

        public static int ConvertTimeToMinutes(string time)
        {
            var timespan = TimeSpan.Parse(time);
            var dateTime = DateTime.Today.Add(timespan);
            return (int)(dateTime - DateTime.Today).TotalMinutes;
        }

        public static DateTime GetCleanDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public bool ChangeScheduleStatus(User user, int scheduleId, StatusAgendamento status = StatusAgendamento.Confirmado)
        {
            var agendaAppointment = Context.AgendaAppointments
                                        .FirstOrDefault(o => o.ID == scheduleId &&
                                                             o.AgendaSchedule.Agenda.ProfessionalID == user.ProfessionalID);

            if (agendaAppointment != null)
            {
                agendaAppointment.Status = status;
                Context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
