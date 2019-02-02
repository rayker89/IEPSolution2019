namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                        DurationTime = c.Int(nullable: false),
                        StartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        OpenedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        AuctionStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionStatus", t => t.AuctionStatusId, cascadeDelete: true)
                .Index(t => t.AuctionStatusId);
            
            CreateTable(
                "dbo.AuctionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "AuctionStatusId", "dbo.AuctionStatus");
            DropIndex("dbo.Auctions", new[] { "AuctionStatusId" });
            DropTable("dbo.AuctionStatus");
            DropTable("dbo.Auctions");
        }
    }
}
