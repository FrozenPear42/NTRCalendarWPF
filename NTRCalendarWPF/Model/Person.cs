using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTRCalendarWPF.Model {
    public class Person {
        [Key]
        public Guid PersonId { get; set; }
        [MaxLength(16)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(16)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        [Required]
        public string UserID { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}