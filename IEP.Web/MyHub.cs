using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace IEP.Web
{
	public class MyHub : Hub
	{
		public MyHub()
		{
		
		}
		public void Loadall()
		{
			Clients.All.loadList();
		}

		public void Reload(int id )
		{
			Clients.All.detailsReload(id);
		}
	}
}