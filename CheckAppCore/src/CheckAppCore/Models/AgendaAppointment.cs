
using System;

namespace CheckAppCore.Models
{
    public class AgendaAppointment
    {
        public int ID { get; set; }
        public int AgendaScheduleID { get; set; }
        public AgendaSchedule AgendaSchedule { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public DateTime DateSchedule { get; set; }
        public int BeginTime { get; set; }
        public int EndTime { get; set; }
    }
}
