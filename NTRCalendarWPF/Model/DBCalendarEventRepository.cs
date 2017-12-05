using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRCalendarWPF.Model {
    public class DBCalendarEventRepository : ICalendarEventRepository {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event RepositoryChangedDelegate EventRepositoryChanged;

        private readonly Person _person;
        private readonly CalendarRepository _repository;

        public DBCalendarEventRepository(Person person, CalendarRepository repository) {
            _person = person;
            _repository = repository;
            _repository.OnDataChanged += () => EventRepositoryChanged?.Invoke();
        }

        public bool AddEvent(UserAppointment appointment) {
            _repository.AddAppointment(_person.UserID, appointment.Title, appointment.Description,
                appointment.AppointmentDate, appointment.StartTime, appointment.EndTime, appointment.Accepted);
            log.InfoFormat("Added appointment to DB: {0}", appointment);
            return true;
        }

        public bool RemoveEvent(UserAppointment appointment) {
            _repository.RemoveAppointment(appointment);
            log.InfoFormat("Removed appointment from DB: {0}", appointment);
            return true;
        }

        public bool ReplaceEvent(UserAppointment oldAppointment, UserAppointment newAppointment) {
            _repository.UpdateAppointment(newAppointment);
            log.InfoFormat("Updated appointment in DB: {0} => {1}", oldAppointment, newAppointment);
            return true;
        }

        public List<UserAppointment> GetEvents() {
            return _repository.GetAppointmentsByUserID(_person.UserID);
        }
    }
}