namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpellingAndTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
        }
    }
}
