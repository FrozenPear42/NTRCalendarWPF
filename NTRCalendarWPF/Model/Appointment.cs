using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTRCalendarWPF.Model {
    [Serializable]
    public class Appointment {
        [Key, Column("AppointmentID")]
        public Guid AppointmentID { get; set; }

        [MaxLength(16)]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
   
        [MaxLength(50)]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] Timestamp { get; set; }

        public override string ToString() {
            return $"{AppointmentDate} {StartTime} - {EndTime}: {Title}";
        }
    }
}