using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    [Serializable]
    public class CalendarEvent {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CalendarEvent(string name, string description, DateTime start, DateTime end) {
            Name = name;
            Description = description;
            Start = start;
            End = end;
        }

        public CalendarEvent() {
            Name = "";
            Description = "";
            Start = DateTime.Today;
            End = DateTime.Today;
        }

        public CalendarEvent(DateTime day) {
            Name = "";
            Description = "";
            Start = day;
            End = day;
        }

        public override string ToString() {
            return Start.ToString("dd.MM HH:mm - ") + End.ToString("HH:mm ") + Name;
        }
    }
}