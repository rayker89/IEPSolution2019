namespace IEP.Security.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTokens : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Tokens", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Tokens", c => c.Int(nullable: false));
        }
    }
}
