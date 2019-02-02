using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEP.Data.Domain
{
    public class Bid
    {
        public int Id { get; set; }
        public int Auction { get; set; }
        public decimal TokenNumber { get; set; }

        public DateTime BidTime { get; set; }
		public int UserID { get; set; }
		public string ContactName { get; set; }
	}
}
