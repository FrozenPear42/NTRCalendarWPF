using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    public class UserAppointment : ICloneable {
        public Guid AppointmentID { get; internal set; }
        public string UserID { get; internal set; }
        public byte[] Timestamp { get; internal set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Accepted { get; set; }


        public override string ToString() {
            return $"{AppointmentDate} {StartTime} - {EndTime}: {Title}";
        }

        public object Clone() {
            return new UserAppointment {
                AppointmentID = AppointmentID,
                UserID = UserID,
                Timestamp = Timestamp,
                Title = Title,
                Description = Description,
                AppointmentDate = AppointmentDate,
                StartTime = StartTime,
                EndTime = EndTime,
                Accepted = Accepted
            };
        }
    }
}