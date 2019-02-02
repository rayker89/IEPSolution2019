using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Auction
{
    public class AuctionsModel
    {
        public List<AuctionModel> Auctions { get; set; }

        public AuctionsModel()
        {
            this.Auctions = new List<AuctionModel>();
        }
    }
}