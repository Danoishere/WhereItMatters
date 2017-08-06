namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBudgetItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonationRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonationRequests", t => t.DonationRequestId, cascadeDelete: true)
                .Index(t => t.DonationRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetItems", "DonationRequestId", "dbo.DonationRequests");
            DropIndex("dbo.BudgetItems", new[] { "DonationRequestId" });
            DropTable("dbo.BudgetItems");
        }
    }
}
