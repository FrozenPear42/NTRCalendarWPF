﻿using System;
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


        public Person GetPersonByUserID(string userID) {
            using (var db = new StorageContext()) {
                return db.People.FirstOrDefault(user => user.UserID.Equals(userID));
            }
        }

//        public List<Person> GetPeople() {
//            using (var db = new StorageContext()) {
//                return db.People.ToList();
//            }
//        }
//
//        public List<Appointment> GetAppointments() {
//            using (var db = new StorageContext()) {
//                return db.Appointments.ToList();
//            }
//        }

        public List<UserAppointment> GetAppointmentsByUserID(string user) {
            using (var db = new StorageContext()) {
                return db.Attendances
                    .Where(a => a.Person.UserID.Equals(user))
                    .Select(a => new UserAppointment {
                        AppointmentID = a.Appointment.AppointmentID,
                        UserID = user,
                        Title = a.Appointment.Title,
                        Description = a.Appointment.Description,
                        AppointmentDate = a.Appointment.AppointmentDate,
                        StartTime = a.Appointment.StartTime,
                        EndTime = a.Appointment.EndTime,
                        Accepted = a.Accepted
                    })
                    .ToList();
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

        public void AddAppointment(string userID, string title, string description, DateTime date, TimeSpan start,
            TimeSpan end, bool accepted) {
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
                Accepted = accepted,
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
        }

        public void RemoveAppointment(UserAppointment appointment) {
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
            }
        }

        public void UpdateAppointment(UserAppointment appointment) {
            using (var db = new StorageContext()) {
                var app = db.Appointments.First(a => a.AppointmentID.Equals(appointment.AppointmentID));
                app.Title = appointment.Title;
                app.Description = appointment.Description;
                app.StartTime = appointment.StartTime;
                app.EndTime = appointment.EndTime;
                var att = app.Attendances.First(a => a.Person.UserID.Equals(appointment.UserID));
                att.Accepted = appointment.Accepted;
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
        }
    }
}