namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageInfosAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageInfoes",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            AlterColumn("dbo.Organisations", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organisations", "IsActive", c => c.Int(nullable: false));
            DropTable("dbo.PageInfoes");
        }
    }
}
