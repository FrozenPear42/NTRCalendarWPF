namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldSizeAndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Description", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "Title", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Appointments", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Appointments", "EndTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.People", "UserID", c => c.String(nullable: false, maxLength: 10));
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "UserID", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            AlterColumn("dbo.Appointments", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "Title", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentDate");
            DropColumn("dbo.Appointments", "Description");
        }
    }
}
