namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spelling : DbMigration
    {
        public override void Up() {
            RenameColumn("dbo.People", "PersonId", "PersonID");
            RenameColumn("dbo.Appointments", "AppointmentId", "AppointmentID");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.People", "PersonID", "PersonId");
            RenameColumn("dbo.Appointments", "AppointmentID", "AppointmentId");
        }
    }
}
