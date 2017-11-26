using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTRCalendarWPF.Model {
    public class Person {
        [Key, Column("PersonID")]
        public Guid PersonID { get; set; }
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