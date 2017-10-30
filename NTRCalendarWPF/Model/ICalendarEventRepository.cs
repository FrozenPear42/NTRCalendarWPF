using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {

    public delegate void RepositoryAddRemoveDelegate(CalendarEvent calendarEvent);
    public delegate void RepositoryReplaceDelegate(CalendarEvent oldEvent, CalendarEvent newEvent);
    public interface ICalendarEventRepository {
        event RepositoryAddRemoveDelegate EventAdded;
        event RepositoryAddRemoveDelegate EventRemoved;
        event RepositoryReplaceDelegate EventReplaced;

        bool AddEvent(CalendarEvent calendarEvent);
        bool RemoveEvent(CalendarEvent calendarEvent);
        bool ReplaceEvent(CalendarEvent oldEvent, CalendarEvent newEvent);
        List<CalendarEvent> GetEvents();

    }
}