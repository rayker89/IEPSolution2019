namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTokenNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TokenNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TokenNumber");
        }
    }
}
