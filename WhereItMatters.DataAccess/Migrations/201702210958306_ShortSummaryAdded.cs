namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortSummaryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationRequests", "ShortSummary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationRequests", "ShortSummary");
        }
    }
}
