namespace IEP.Web.Models.Token
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderModel
    {
        public int Id { get; set; }
		
		public int UserId { get; set; }

		public decimal Price { get; set; }

		public int TokenNumber { get; set; }
		public DateTime CreatedDate { get; set; }

		public string Status { get; set; }
        public int OrderStatusId { get; set; }
    }
}
