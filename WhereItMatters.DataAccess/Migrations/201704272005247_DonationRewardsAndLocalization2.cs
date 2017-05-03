namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationRewardsAndLocalization2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalizedTexts",
                c => new
                    {
                        Identifier = c.String(nullable: false, maxLength: 128),
                        ValueEN = c.String(),
                    })
                .PrimaryKey(t => t.Identifier);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocalizedTexts");
        }
    }
}
