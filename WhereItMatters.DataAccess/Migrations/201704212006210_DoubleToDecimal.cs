namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoubleToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonationRequests", "NeededAmountUSD", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Donations", "AmountUSD", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Donations", "AmountUSD", c => c.Double(nullable: false));
            AlterColumn("dbo.DonationRequests", "NeededAmountUSD", c => c.Double(nullable: false));
        }
    }
}
