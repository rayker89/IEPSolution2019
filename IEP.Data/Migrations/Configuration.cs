namespace IEP.Data.Migrations
{
    using IEP.Data.Domain;
    using IEP.Data.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IEP.Data.dbContextManager.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IEP.Data.dbContextManager.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //context.Set<Order>().AddOrUpdate(new Order() { Price = 200, OrderStatusId = 1 });

            if (!context.Set<AuctionStatus>().Any())
            {
                foreach (EnAuctionStatuses EnAuctionStatus in Enum.GetValues(typeof(EnAuctionStatuses)))
                {
                    context.Set<AuctionStatus>().AddOrUpdate(new AuctionStatus() { Id = (int)EnAuctionStatus, Name = EnAuctionStatus.ToString() });
                }
            }
            if (!context.Set<OrderStatus>().Any())
            {
                foreach (EnumOrderStatuses EnumOrderStatus in Enum.GetValues(typeof(EnumOrderStatuses)))
                {
                    context.Set<OrderStatus>().AddOrUpdate(new OrderStatus() { Id = (int)EnumOrderStatus, Name = EnumOrderStatus.ToString() });
                }
            }

            if (!context.Set<Currency>().Any())
            {
                context.Set<Currency>().AddOrUpdate(new Currency() { Id = 1, Name = "RSD" });
                context.Set<Currency>().AddOrUpdate(new Currency() { Id = 2, Name = "USD" });
                context.Set<Currency>().AddOrUpdate(new Currency() { Id = 3, Name = "EUR" });
            }

            if (!context.Set<ApplicationSettings>().Any())
            {
                context.Set<ApplicationSettings>().AddOrUpdate(new ApplicationSettings() { Id = Guid.NewGuid().ToString() , AuctionItems = 10, AuctionDuration = 36000, SilverPackageTokens = 100, GoldPackageTokens = 200, PlatinumPackageTokens = 200, TokenValue = 1, CurrencyId = 1});
            }

        }
    }
}
