namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Curencies1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies", "Vrednost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Currencies", "Vrednost");
        }
    }
}
