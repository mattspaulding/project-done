namespace ProjectDone.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToJob : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUserId" });
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
                        Job_JobId = c.Int(),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Jobs", t => t.Job_JobId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Job_JobId);
            
            AddColumn("dbo.Jobs", "Image", c => c.String());
            AlterColumn("dbo.Jobs", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "ApplicationUserId");
            AddForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "Job_JobId", "dbo.Jobs");
            DropForeignKey("dbo.Bids", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUserId" });
            DropIndex("dbo.Bids", new[] { "Job_JobId" });
            DropIndex("dbo.Bids", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Jobs", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Jobs", "Image");
            DropTable("dbo.Bids");
            CreateIndex("dbo.Jobs", "ApplicationUserId");
            AddForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
