namespace ProjectDone.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToProject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Message = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        BidState = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        Project_ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Project_ProjectId);
            
            AddColumn("dbo.Projects", "Image", c => c.String());
            AlterColumn("dbo.Projects", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Projects", "ApplicationUserId");
            AddForeignKey("dbo.Projects", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Bids", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ApplicationUserId" });
            DropIndex("dbo.Bids", new[] { "Project_ProjectId" });
            DropIndex("dbo.Bids", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Projects", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Projects", "Image");
            DropTable("dbo.Bids");
            CreateIndex("dbo.Projects", "ApplicationUserId");
            AddForeignKey("dbo.Projects", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
