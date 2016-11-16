using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.DTO;
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

            return GetAgendaScheduleRange(agendaSchedule);

        }

        public List<AgendaScheduleDTO> GetAgendaScheduleRange(AgendaSchedule agendaSchedule)
        {
            var scheduleList = new List<AgendaScheduleDTO>();

            for (var i = agendaSchedule.BeginTime; i < agendaSchedule.EndTime; i += agendaSchedule.IntervalTime)
            {
                var agendaException = Context.AgendaExceptions.FirstOrDefault(o => o.AgendaScheduleID == agendaSchedule.ID && (i >= o.BeginTime && i < o.EndTime));
                if (agendaException != null)
                {
                    i = agendaException.EndTime;
                    continue;
                }

                scheduleList.Add(new AgendaScheduleDTO { AgendaScheduleID = agendaSchedule.ID, Date = agendaSchedule.Date, BeginTime = ConvertMinutesToDateTime(agendaSchedule.Date, i).ToString("HH:mm"), EndTime = ConvertMinutesToDateTime(agendaSchedule.Date, i + agendaSchedule.IntervalTime).ToString("HH:mm") });
            }

            return scheduleList;
        }

        public static DateTime ConvertMinutesToDateTime(DateTime date, int minutes)
        {
            return date.AddMinutes(minutes);
        }
    }
}
