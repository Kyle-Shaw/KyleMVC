namespace KyleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JournalModels");
            AddPrimaryKey("dbo.JournalModels", new[] { "UserID", "Date" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.JournalModels");
            AddPrimaryKey("dbo.JournalModels", "Date");
        }
    }
}
