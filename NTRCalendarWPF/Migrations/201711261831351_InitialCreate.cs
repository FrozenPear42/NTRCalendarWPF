namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Guid(nullable: false),
                        Title = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AppointmentID = c.Guid(nullable: false),
                        PersonID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppointmentID, t.PersonID })
                .ForeignKey("dbo.Appointments", t => t.AppointmentID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.AppointmentID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "PersonID", "dbo.People");
            DropForeignKey("dbo.Attendances", "AppointmentID", "dbo.Appointments");
            DropIndex("dbo.Attendances", new[] { "PersonID" });
            DropIndex("dbo.Attendances", new[] { "AppointmentID" });
            DropTable("dbo.People");
            DropTable("dbo.Attendances");
            DropTable("dbo.Appointments");
        }
    }
}
