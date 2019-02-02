using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEP.Data.Domain
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int DurationTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? OpenedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
		
		public decimal TokenValue { get; set; }

		public string Currency { get; set; }
		

		public int AuctionStatusId { get; set; }
		public int AuctionBiderId { get; set; }

		public virtual AuctionStatus AuctionStatus { get; set; }

    }
}
