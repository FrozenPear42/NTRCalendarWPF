using System;
using System.Collections.Generic;

namespace NTRCalendarWPF.Model {
    public class Appointment {
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
//        public Date AppointmentDate { get; set; } TODO: MIGRATION
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}