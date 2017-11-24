using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.RightsManagement;

namespace NTRCalendarWPF.Model {
    public class CalendarRepository {
        public List<Person> GetPeople() {
            using (var db = new StorageContext())
                return db.People.ToList();
        }

        public Person GetPersonByUserID(string userID) {
            using (var db = new StorageContext())
                return db.People.First(user => user.UserID.Equals(userID));
        }

        public List<Appointment> GetAppointments() {
            using (var db = new StorageContext())
                return db.Appointments.ToList();
        }

        public List<Appointment> GetAppointmentsByUserID(string user) {
            using (var db = new StorageContext())
                return db.Attendances.Where(a => a.Person.UserID.Equals(user)).Select(a => a.Appointment).ToList();
        }

        public Person AddPerson(string firstName, string lastName, string userID) {
            var person = new Person {
                FirstName = firstName,
                LastName = lastName,
                UserID = userID,
                PersonId = Guid.NewGuid()
            };
            using (var db = new StorageContext()) {
                db.People.Add(person);
                db.SaveChanges();
                return person;
            }
        }

        public void AddAppointment(string userID) {
            var appointment = new Appointment { };
            var attendance = new Attendance { };
            using (var db = new StorageContext()) {
                db.People.First(a => a.UserID.Equals(userID)).Attendances.Add(attendance);
                db.Appointments.Add(appointment);
                db.Attendances.Add(attendance);
                db.SaveChanges();
            }
        }
    }
}