namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimestampsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationRequests", "ApproxPopulationImpacted", c => c.Int(nullable: false));
            AddColumn("dbo.DonationRequests", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.DonationRequests", "ClosedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.DonationRequests", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Missions", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Missions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Organisations", "IsActive", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organisations", "IsActive");
            DropColumn("dbo.Missions", "IsActive");
            DropColumn("dbo.Missions", "CreatedAt");
            DropColumn("dbo.DonationRequests", "EndDate");
            DropColumn("dbo.DonationRequests", "ClosedOn");
            DropColumn("dbo.DonationRequests", "CreatedOn");
            DropColumn("dbo.DonationRequests", "ApproxPopulationImpacted");
        }
    }
}
