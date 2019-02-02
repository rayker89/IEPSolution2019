using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Auction
{
    public class BidModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int Auction { get; set; }
        public decimal TokenNumber { get; set; }

        public DateTime BidTime { get; set; }
		public string ContactName { get; set; }
	}
}