namespace WhereItMatters.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageInfosNameCorrection : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PageInfoes", newName: "PageInfos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PageInfos", newName: "PageInfoes");
        }
    }
}
