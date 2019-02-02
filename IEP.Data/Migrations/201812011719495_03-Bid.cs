namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03Bid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Auction = c.Int(),
                        TokenNumber = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false)
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bids");
        }
    }
}
