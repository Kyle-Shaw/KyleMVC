namespace KyleMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUsrModl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalModels", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JournalModels", "UserModel_Id", "dbo.UserModels");
            DropPrimaryKey("dbo.UserModels");
            DropIndex("dbo.JournalModels", new[] { "UserId_Id" });
            DropIndex("dbo.JournalModels", new[] { "UserModel_Id" });
            DropColumn("dbo.JournalModels", "UserId_Id");
            DropColumn("dbo.UserModels", "Id");
            RenameColumn(table: "dbo.JournalModels", name: "UserModel_Id", newName: "UserID");
            AddColumn("dbo.UserModels", "UserID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.JournalModels", "UserID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserModels", "UserID");
            CreateIndex("dbo.JournalModels", "UserID");
            AddForeignKey("dbo.JournalModels", "UserID", "dbo.UserModels", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModels", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.JournalModels", "UserId_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.JournalModels", "UserID", "dbo.UserModels");
            DropIndex("dbo.JournalModels", new[] { "UserID" });
            DropPrimaryKey("dbo.UserModels");
            AlterColumn("dbo.JournalModels", "UserID", c => c.Int());
            DropColumn("dbo.UserModels", "UserID");
            AddPrimaryKey("dbo.UserModels", "Id");
            RenameColumn(table: "dbo.JournalModels", name: "UserID", newName: "UserModel_Id");
            CreateIndex("dbo.JournalModels", "UserModel_Id");
            CreateIndex("dbo.JournalModels", "UserId_Id");
            AddForeignKey("dbo.JournalModels", "UserModel_Id", "dbo.UserModels", "Id");
            AddForeignKey("dbo.JournalModels", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
