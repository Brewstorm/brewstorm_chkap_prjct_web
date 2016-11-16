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
    }
}
