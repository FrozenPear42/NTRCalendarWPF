using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model
{
    public class DBCalendarEventRepository : ICalendarEventRepository {
        private Person _person;
        private CalendarRepository _repository;

        public DBCalendarEventRepository(Person person, CalendarRepository repository) {
            _person = person;
            _repository = repository;
        }

        public event RepositoryChangedDelegate EventRepositoryChanged;
        public bool AddEvent(Appointment calendarEvent) {
            _repository.AddAppointment(_person.UserID, calendarEvent.Title, calendarEvent.Description, calendarEvent.AppointmentDate, calendarEvent.StartTime, calendarEvent.EndTime);
            return true;
        }

        public bool RemoveEvent(Appointment calendarEvent) {
            _repository.RemoveAppointment(calendarEvent);
            return true;
        }

        public bool ReplaceEvent(Appointment oldEvent, Appointment newEvent) {
            _repository.UpdateAppointment(newEvent);
            return true;
        }
    
        public List<Appointment> GetEvents() {
            return _repository.GetAppointmentsByUserID(_person.UserID);
        }
    }
}
