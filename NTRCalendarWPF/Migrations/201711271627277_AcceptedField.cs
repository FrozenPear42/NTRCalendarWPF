namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcceptedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "Accepted");
        }
    }
}
