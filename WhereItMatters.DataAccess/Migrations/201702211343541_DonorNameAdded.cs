namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonorNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "DonorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "DonorName");
        }
    }
}
