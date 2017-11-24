namespace NTRCalendarWPF.Model {
    public class Attendance {
        public virtual Appointment Appointment { get; set; }
        public virtual Person Person { get; set; }
//        public bool Accepted { get; set; }       TODO: MIGRATION
    }
}