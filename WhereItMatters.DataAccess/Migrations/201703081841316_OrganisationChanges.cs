namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organisations", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Organisations", "Mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organisations", "Mail");
            DropColumn("dbo.Organisations", "IsApproved");
        }
    }
}
