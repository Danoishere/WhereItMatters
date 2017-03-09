namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableClosedOn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonationRequests", "ClosedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DonationRequests", "ClosedOn", c => c.DateTime(nullable: false));
        }
    }
}
