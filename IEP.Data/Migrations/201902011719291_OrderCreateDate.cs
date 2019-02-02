namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreateDate");
        }
    }
}
