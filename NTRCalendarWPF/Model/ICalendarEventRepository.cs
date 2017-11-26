using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {

    public delegate void RepositoryChangedDelegate();

    public interface ICalendarEventRepository {
        event RepositoryChangedDelegate EventRepositoryChanged;

        bool AddEvent(Appointment calendarEvent);
        bool RemoveEvent(Appointment calendarEvent);
        bool ReplaceEvent(Appointment oldEvent, Appointment newEvent);
        List<Appointment> GetEvents();

    }
}