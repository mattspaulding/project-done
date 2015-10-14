namespace ProjectDone.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 100),
                        CreationDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUserId" });
            DropTable("dbo.Jobs");
        }
    }
}
