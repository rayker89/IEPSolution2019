using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEP.Web.Models.Admin

{
	public class CurrenciesModel
	{
		public List<CurrencyModel> Currencies { get; set; }
		public CurrenciesModel()
		{
			this.Currencies = new List<CurrencyModel>();
		}
	}
}