using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;

namespace NTRCalendarWPF.Model {
    public class Attendance {
        [Key]
        [Column(Order = 0)]
        public Guid AppointmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PersonID { get; set; }

        public virtual Appointment Appointment { get; set; }

        public virtual Person Person { get; set; }

        public bool Accepted { get; set; }
    }
}