namespace ProjectDone.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ImagePath", c => c.String());
            DropColumn("dbo.Jobs", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Image", c => c.String());
            DropColumn("dbo.Jobs", "ImagePath");
        }
    }
}
