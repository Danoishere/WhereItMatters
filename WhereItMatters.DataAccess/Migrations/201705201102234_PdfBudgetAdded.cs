namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PdfBudgetAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationRequests", "BudgetPdfUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationRequests", "BudgetPdfUrl");
        }
    }
}
