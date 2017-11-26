using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTRCalendarWPF.Model {
    public class Appointment {
        [Key]
        public Guid AppointmentId { get; set; }
        [MaxLength(16)]
        [Required]
        public string Title { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}