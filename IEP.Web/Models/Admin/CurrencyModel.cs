using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Admin
{
	public class CurrencyModel
	{
		public int Id { get; set; }

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Token Value")]
		public decimal Vrednost { get; set; }
	}
}