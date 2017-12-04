namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcurrencyCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.People", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Timestamp");
            DropColumn("dbo.Appointments", "Timestamp");
        }
    }
}
