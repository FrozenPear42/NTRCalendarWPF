using System.Collections.Generic;

namespace NTRCalendarWPF.Model {
    public class DatabaseCalendarEventRepository : ICalendarEventRepository {
        
        public event RepositoryAddRemoveDelegate EventAdded;
        public event RepositoryAddRemoveDelegate EventRemoved;
        public event RepositoryReplaceDelegate EventReplaced;
        
        public bool AddEvent(CalendarEvent calendarEvent) {
            throw new System.NotImplementedException();
        }

        public bool RemoveEvent(CalendarEvent calendarEvent) {
            throw new System.NotImplementedException();
        }

        public bool ReplaceEvent(CalendarEvent oldEvent, CalendarEvent newEvent) {
            throw new System.NotImplementedException();
        }

        public List<CalendarEvent> GetEvents() {
            throw new System.NotImplementedException();
        }
    }
}