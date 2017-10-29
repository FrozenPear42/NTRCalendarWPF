using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    public class CalendarEvent {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CalendarEvent(string name, DateTime start, DateTime end) {
            Name = name;
            Start = start;
            End = end;
        }
    }
}