using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Security.RightsManagement;

namespace NTRCalendarWPF.Model {
    public class CalendarRepository {
        public delegate void CalendarModifiedDelegate();

        public event CalendarModifiedDelegate OnDataChanged;

        public List<Person> GetPeople() {
            using (var db = new StorageContext())
                return db.People.ToList();
        }

        public Person GetPersonByUserID(string userID) {
            using (var db = new StorageContext()) {
                return db.People.FirstOrDefault(user => user.UserID.Equals(userID));
            }
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
                PersonID = Guid.NewGuid()
            };
            using (var db = new StorageContext()) {
                db.People.Add(person);
                db.SaveChanges();
                return person;
            }
        }

        public void AddAppointment(string userID, string title, DateTime start, DateTime end) {
            var appointment = new Appointment {
                Title = title,
                StartTime = start.TimeOfDay,
                EndTime = end.TimeOfDay,
                AppointmentID = Guid.NewGuid()
            };
            var attendance = new Attendance {
                Appointment = appointment,
            };

            using (var db = new StorageContext()) {
                var person = db.People.First(a => a.UserID.Equals(userID));
                attendance.Person = person;

                db.Appointments.Add(appointment);
                db.Attendances.Add(attendance);
                db.SaveChanges();
            }
            OnDataChanged?.Invoke();
        }

        public void RemoveAppointment(Appointment appointment) {
            using (var db = new StorageContext()) {
                db.Appointments.Remove(appointment);
                db.SaveChanges();
            }
            OnDataChanged?.Invoke();
        }

        public void UpdateAppointment(Appointment appointment) {
            using (var db = new StorageContext()) {
                db.Appointments.AddOrUpdate(appointment);
                db.SaveChanges();
            }
            OnDataChanged?.Invoke();
        }
    }
}