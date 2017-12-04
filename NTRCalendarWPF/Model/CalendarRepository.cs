using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Security.RightsManagement;

namespace NTRCalendarWPF.Model {
    public class CalendarRepository {
        public delegate void CalendarModifiedDelegate();

        public event CalendarModifiedDelegate OnDataChanged;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Person> GetPeople() {
            using (var db = new StorageContext()) {
                return db.People.ToList();
            }
        }

        public Person GetPersonByUserID(string userID) {
            using (var db = new StorageContext()) {
                return db.People.FirstOrDefault(user => user.UserID.Equals(userID));
            }
        }

        public List<Appointment> GetAppointments() {
            using (var db = new StorageContext()) {
                return db.Appointments.ToList();
            }
        }

        public List<Appointment> GetAppointmentsByUserID(string user) {
            using (var db = new StorageContext()) {
                return db.Attendances.Where(a => a.Person.UserID.Equals(user)).Select(a => a.Appointment).ToList();
            }
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
                try {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e) {
                    var message = e.EntityValidationErrors
                        .SelectMany(errors => errors.ValidationErrors)
                        .Aggregate("", (current, error) => current + (error.ErrorMessage + '\n'));
                    log.Error(message);
                    throw new Exception(message);
                }
                catch (Exception e) {
                    log.Error(e.Message);
                    throw new Exception("DB error. " + e.Message);
                }
                return person;
            }
        }

        public Appointment AddAppointment(string userID, string title, string description, DateTime date,
            TimeSpan start, TimeSpan end) {
            var appointment = new Appointment {
                AppointmentID = Guid.NewGuid(),
                Title = title,
                AppointmentDate = date,
                StartTime = start,
                EndTime = end,
                Description = description,
            };
            var attendance = new Attendance {
                Appointment = appointment,
            };

            using (var db = new StorageContext()) {
                var person = db.People.First(a => a.UserID.Equals(userID));
                attendance.Person = person;

                db.Appointments.Add(appointment);
                db.Attendances.Add(attendance);

                try {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e) {
                    var message = e.EntityValidationErrors
                        .SelectMany(errors => errors.ValidationErrors)
                        .Aggregate("", (current, error) => current + (error.ErrorMessage + '\n'));
                    log.Error(message);
                    throw new Exception(message);
                }
                catch (Exception e) {
                    log.Error(e.Message);
                    throw new Exception("DB error. " + e.Message);
                }
            }
            OnDataChanged?.Invoke();
            return appointment;
        }

        public Appointment RemoveAppointment(Appointment appointment) {
            using (var db = new StorageContext()) {
                var app = db.Appointments.First(a => a.AppointmentID.Equals(appointment.AppointmentID));
                db.Appointments.Remove(app);
                try {
                    db.SaveChanges();
                }
                catch (DBConcurrencyException e) {
                    throw new Exception(e.Message);
                }
                catch (Exception e) {
                    log.Error(e.Message);
                    throw new Exception("DB error. " + e.Message);
                }
                OnDataChanged?.Invoke();
                return app;
            }
        }

        public Appointment UpdateAppointment(Appointment appointment) {
            using (var db = new StorageContext()) {
                db.Appointments.AddOrUpdate(appointment);
                var dbRef = db.Appointments.First(a => a.AppointmentID.Equals(appointment.AppointmentID));
                try {
                    db.SaveChanges();
                }
                catch (DBConcurrencyException e) {
                    throw new Exception(e.Message);
                }
                catch (DbEntityValidationException e) {
                    var message = e.EntityValidationErrors
                        .SelectMany(errors => errors.ValidationErrors)
                        .Aggregate("", (current, error) => current + (error.ErrorMessage + '\n'));
                    log.Error(message);
                    throw new Exception(message);
                }
                catch (Exception e) {
                    log.Error(e.Message);
                    throw new Exception("DB error. " + e.Message);
                }
                OnDataChanged?.Invoke();
            }
            return appointment;
        }
    }
}