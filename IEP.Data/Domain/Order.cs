using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEP.Data.Domain
{
    public class Order
    {
        public int Id { get; set; }

		public int UserId { get; set; }
		public int TokenNumber { get; set; }
        public decimal Price { get; set; }
		public DateTime CreateDate { get; set; }
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

    }
}
