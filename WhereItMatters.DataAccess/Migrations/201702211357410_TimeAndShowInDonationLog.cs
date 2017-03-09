namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeAndShowInDonationLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "ShowInDonationLog", c => c.Boolean(nullable: false));
            AddColumn("dbo.Donations", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "TimeStamp");
            DropColumn("dbo.Donations", "ShowInDonationLog");
        }
    }
}
