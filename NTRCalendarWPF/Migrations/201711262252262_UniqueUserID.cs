namespace NTRCalendarWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueUserID : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.People", "UserID", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.People", new[] { "UserID" });
        }
    }
}
