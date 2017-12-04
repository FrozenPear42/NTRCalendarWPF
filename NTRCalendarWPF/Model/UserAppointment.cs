using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    class UserAppointment {
        public Guid AppointmentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Accepted { get; set; }

        public override string ToString() {
            return $"{AppointmentDate} {StartTime} - {EndTime}: {Title}";
        }
    }
}