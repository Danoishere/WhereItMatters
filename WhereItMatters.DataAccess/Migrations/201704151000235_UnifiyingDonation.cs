namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnifiyingDonation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donations", "DonationRequestId", "dbo.DonationRequests");
            DropIndex("dbo.Donations", new[] { "DonationRequestId" });
            AddColumn("dbo.Donations", "MissionId", c => c.Int());
            AddColumn("dbo.Donations", "OrganisationId", c => c.Int());
            AlterColumn("dbo.Donations", "DonationRequestId", c => c.Int());
            CreateIndex("dbo.Donations", "DonationRequestId");
            CreateIndex("dbo.Donations", "MissionId");
            CreateIndex("dbo.Donations", "OrganisationId");
            AddForeignKey("dbo.Donations", "MissionId", "dbo.Missions", "Id");
            AddForeignKey("dbo.Donations", "OrganisationId", "dbo.Organisations", "Id");
            AddForeignKey("dbo.Donations", "DonationRequestId", "dbo.DonationRequests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "DonationRequestId", "dbo.DonationRequests");
            DropForeignKey("dbo.Donations", "OrganisationId", "dbo.Organisations");
            DropForeignKey("dbo.Donations", "MissionId", "dbo.Missions");
            DropIndex("dbo.Donations", new[] { "OrganisationId" });
            DropIndex("dbo.Donations", new[] { "MissionId" });
            DropIndex("dbo.Donations", new[] { "DonationRequestId" });
            AlterColumn("dbo.Donations", "DonationRequestId", c => c.Int(nullable: false));
            DropColumn("dbo.Donations", "OrganisationId");
            DropColumn("dbo.Donations", "MissionId");
            CreateIndex("dbo.Donations", "DonationRequestId");
            AddForeignKey("dbo.Donations", "DonationRequestId", "dbo.DonationRequests", "Id", cascadeDelete: true);
        }
    }
}
