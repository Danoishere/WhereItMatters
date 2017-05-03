namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationRewardsAndLocalization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DonationRewards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.Int(nullable: false),
                        MinimalDonation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonationRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonationRequests", t => t.DonationRequestId, cascadeDelete: true)
                .Index(t => t.DonationRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonationRewards", "DonationRequestId", "dbo.DonationRequests");
            DropIndex("dbo.DonationRewards", new[] { "DonationRequestId" });
            DropTable("dbo.DonationRewards");
        }
    }
}
