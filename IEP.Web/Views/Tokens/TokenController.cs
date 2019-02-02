using IEP.Data.dbContextManager;
using IEP.Data.Domain;
using IEP.Security;
using IEP.Web.Models.Admin;
using IEP.Web.Models.Token;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Cors;
using System.Net.Http;

namespace IEP.Web.Views.Tokens
{

	public class TokenController : Controller
	{
		private static readonly HttpClient client = new HttpClient();
		private ApplicationDbContext context = new ApplicationDbContext();
		// GET: Token
		public ActionResult Index()
		{
			ApplicationSettingsViewModel model = new ApplicationSettingsViewModel();
			ApplicationSettings applicationSettings = context.ApplicationSettings.FirstOrDefault();
			List<Currency> currencies = context.Currencies.ToList();
			//model.Currencies = new List<CurrencyModel>();
			var data = context.Currencies.ToList();
			//foreach (var item in data)
			//{
			//	model.Currencies.Add(new CurrencyModel() { Id = item.Id, Name = item.Name, Vrednost = item.Vrednost });
			//}
			if (applicationSettings != null)
			{
				model.AuctionItems = applicationSettings.AuctionItems;
				model.AuctionDuration = applicationSettings.AuctionDuration;
				model.SilverPackageTokens = applicationSettings.SilverPackageTokens;
				model.GoldPackageTokens = applicationSettings.GoldPackageTokens;
				model.PlatinumPackageTokens = applicationSettings.PlatinumPackageTokens;
				model.TokenValue = applicationSettings.TokenValue;
				model.CurrencyId = applicationSettings.CurrencyId;
			}



			return View(model);
		}

		public async System.Threading.Tasks.Task<ActionResult> CentiliAsync()
		{

			var values = new Dictionary<string, string>
			{
			 { "apikey", "3854cc76184c32794a747683c1e424ec" }
			};

			var content = new FormUrlEncodedContent(values);

			var response = await client.PostAsync("https://api.centili.com/api/payment/1_3/transactionHTTP/1.1", content);

			var responseString = await response.Content.ReadAsStringAsync();

			if (responseString != null) {
				return Json(new { success = true, responseString, response }, JsonRequestBehavior.AllowGet);
			} else {
				//return RedirectToAction("Index","Auction");
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult List()
		{
			int id = Int32.Parse(User.Identity.GetUserId());
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			var user = sda.Users.Where(m => m.Id == id).FirstOrDefault();

			var data = context.Orders.Where(m => (m.UserId == user.Id)).OrderByDescending(m => m.Id).ToList();
			List<OrderModel> model = new List<OrderModel>();
			foreach (var item in data)
			{
				model.Add(new OrderModel()
				{
					Id = item.Id,
					Price = item.Price,
					OrderStatusId = item.OrderStatusId,
					TokenNumber = item.TokenNumber,
					Status = item.OrderStatus.Name,
					CreatedDate = item.CreateDate
				});
			}

			return View("Pregled", model);
		}

		public ActionResult CreateOrder(int package, decimal TokenValue)
		{
			int userId = Int32.Parse(User.Identity.GetUserId());
			var context = new ApplicationDbContext();
			Order order = new Order()
			{
				UserId = userId,
				Price = package * TokenValue,
				OrderStatusId = 1,
				TokenNumber = package,
				CreateDate = DateTime.Now
			};
			context.Orders.Add(order);
			context.SaveChanges();
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Centili ()
		{
			var context = new ApplicationDbContext();
			int userId = Int32.Parse(User.Identity.GetUserId());
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			var user = sda.Users.Where(m => m.Id == userId).FirstOrDefault();

			var status = Request.QueryString["status"];
			var package = Request.QueryString["userid"];
			if (status == "success")
			{
				Order order = context.Orders.Where(m => m.UserId == userId).OrderByDescending(m=> m.CreateDate).FirstOrDefault();
				order.OrderStatusId = 3;
				user.Tokens += decimal.Parse(package);
				sda.Update(user);
				context.SaveChanges();

				return RedirectToAction("Index", "Auction");

			}
			else
			{
				Order order = context.Orders.Where(m => m.UserId == userId).OrderByDescending(m => m.CreateDate).FirstOrDefault();
				order.OrderStatusId = 2;
				context.SaveChanges();

				return RedirectToAction("Index", "Auction");
			}
			
		}
	} 
	
}
