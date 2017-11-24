using System;
using System.Data.Entity;

namespace NTRCalendarWPF.Model {
    public class StorageContext : DbContext  {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}