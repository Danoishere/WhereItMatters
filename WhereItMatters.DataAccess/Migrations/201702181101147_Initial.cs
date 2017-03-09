namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DonationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        NeededAmountUSD = c.Double(nullable: false),
                        Priority = c.Int(nullable: false),
                        MissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Missions", t => t.MissionId, cascadeDelete: true)
                .Index(t => t.MissionId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DonorEmail = c.String(),
                        Comment = c.String(),
                        DenySupportMoney = c.Boolean(nullable: false),
                        AmountUSD = c.Double(nullable: false),
                        DonationRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonationRequests", t => t.DonationRequestId, cascadeDelete: true)
                .Index(t => t.DonationRequestId);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        OrganisationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisations", t => t.OrganisationId, cascadeDelete: true)
                .Index(t => t.OrganisationId);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonationRequests", "MissionId", "dbo.Missions");
            DropForeignKey("dbo.Missions", "OrganisationId", "dbo.Organisations");
            DropForeignKey("dbo.Donations", "DonationRequestId", "dbo.DonationRequests");
            DropIndex("dbo.Missions", new[] { "OrganisationId" });
            DropIndex("dbo.Donations", new[] { "DonationRequestId" });
            DropIndex("dbo.DonationRequests", new[] { "MissionId" });
            DropTable("dbo.Organisations");
            DropTable("dbo.Missions");
            DropTable("dbo.Donations");
            DropTable("dbo.DonationRequests");
        }
    }
}
