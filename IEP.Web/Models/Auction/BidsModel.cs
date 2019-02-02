using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Auction
{
    public class BidsModel
    {
        public List<BidModel> Bids { get; set; }
        public BidsModel()
        {
            this.Bids = new List<BidModel>();
        }
    }
}