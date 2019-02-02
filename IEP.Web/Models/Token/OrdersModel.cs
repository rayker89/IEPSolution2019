using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Token
{
	public class OrdersModel
	{
		public List<OrderModel> Orders { get; set; }
		public OrdersModel()
		{
			this.Orders = new List<OrderModel>();
		}
	}
}