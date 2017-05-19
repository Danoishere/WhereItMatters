namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationRewardCorrection : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonationRewards", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DonationRewards", "Description", c => c.Int(nullable: false));
        }
    }
}
