using IEP.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEP.Data.dbContextManager
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ApplicationSettings> ApplicationSettings { get; set; }


        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
