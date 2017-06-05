namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmbeddedHtmlToGalleryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryItems", "EmbeddedHtml", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GalleryItems", "EmbeddedHtml");
        }
    }
}
