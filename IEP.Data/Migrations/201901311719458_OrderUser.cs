namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "UserId");
        }
    }
}
