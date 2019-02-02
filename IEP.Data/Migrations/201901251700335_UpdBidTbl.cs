namespace IEP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdBidTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "ContactName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bids", "ContactName");
        }
    }
}
