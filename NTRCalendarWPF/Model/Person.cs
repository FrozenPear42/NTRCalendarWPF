using System;
using System.Collections.Generic;

namespace NTRCalendarWPF.Model {
    public class Person {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}