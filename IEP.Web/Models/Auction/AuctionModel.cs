using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Auction
{
    public class AuctionModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "PRAZNO")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public HttpPostedFileBase ImagePath { get; set; }

        public string Img { get; set; }

        [Required]
        [Display(Name = "Duration time")]
        public int DurationTime { get; set; }

        [Required]
        [Display(Name = "Start Price")]
        public decimal StartPrice { get; set; }

        [Display(Name = "Current Price")]
        public decimal CurrentPrice { get; set; }

		public List<BidModel> Bids { get; set; }

		public int AuctionStatusId { get; set; }

		public string AuctionStatus { get; set; }

		public string AuctionBider { get; set; }
		public int AuctionBiderId { get; internal set; }
		
		public DateTime ClosedDate { get; set; }

		[Display(Name = "Token Value")]
		public decimal TokenValue { get; set; }

		[Display(Name = "Currency")]
		public string Currency { get; set; }


		public TimeSpan OstaloVreme {
			get
			{
				return this.ClosedDate - DateTime.Now;
			}
		}

		public AuctionModel()
		{
			this.Bids = new List<BidModel>();

		}

	}
}