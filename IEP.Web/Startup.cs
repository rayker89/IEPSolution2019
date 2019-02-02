using System;
using System.Threading.Tasks;
using IEP.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(IEP.Web.Startup))]

namespace IEP.Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
			AuthenticationConfiguration.ConfigureAuth(app);
			app.MapSignalR();
		}
	}
}
