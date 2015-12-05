namespace ProjectDone.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ImagePath", c => c.String());
            DropColumn("dbo.Projects", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Image", c => c.String());
            DropColumn("dbo.Projects", "ImagePath");
        }
    }
}
