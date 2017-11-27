using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    public class DBCalendarEventRepository : ICalendarEventRepository {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event RepositoryChangedDelegate EventRepositoryChanged;

        private readonly Person _person;
        private readonly CalendarRepository _repository;

        public DBCalendarEventRepository(Person person, CalendarRepository repository) {
            _person = person;
            _repository = repository;
            _repository.OnDataChanged += () => EventRepositoryChanged?.Invoke();
        }

        public bool AddEvent(Appointment calendarEvent) {
            _repository.AddAppointment(_person.UserID, calendarEvent.Title, calendarEvent.Description,
                calendarEvent.AppointmentDate, calendarEvent.StartTime, calendarEvent.EndTime);
            log.InfoFormat("Added appointment to DB: {0}", calendarEvent);
            return true;
        }

        public bool RemoveEvent(Appointment calendarEvent) {
            _repository.RemoveAppointment(calendarEvent);
            log.InfoFormat("Removed appointment from DB: {0}", calendarEvent);
            return true;
        }

        public bool ReplaceEvent(Appointment oldEvent, Appointment newEvent) {
            _repository.UpdateAppointment(newEvent);
            log.InfoFormat("Updated appointment in DB: {0} => {1}", oldEvent, newEvent);
            return true;
        }

        public List<Appointment> GetEvents() {
            return _repository.GetAppointmentsByUserID(_person.UserID);
        }
    }
}