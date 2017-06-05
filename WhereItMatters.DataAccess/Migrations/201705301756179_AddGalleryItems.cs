namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGalleryItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ItemUrl = c.String(),
                        DonationRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonationRequests", t => t.DonationRequestId, cascadeDelete: true)
                .Index(t => t.DonationRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryItems", "DonationRequestId", "dbo.DonationRequests");
            DropIndex("dbo.GalleryItems", new[] { "DonationRequestId" });
            DropTable("dbo.GalleryItems");
        }
    }
}
