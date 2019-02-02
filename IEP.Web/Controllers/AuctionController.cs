using IEP.Data.dbContextManager;
using IEP.Data.Domain;
using IEP.Data.Enums;
using IEP.Security;
using IEP.Web.Models.Admin;
using IEP.Web.Models.Auction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEP.Web.Controllers
{
	public class AuctionController : Controller
	{
		private ApplicationDbContext context;
		
		public AuctionController()
		{
			this.context = new ApplicationDbContext();		
		}
		
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Create()
		{
			ApplicationSettings applicationSettings = context.ApplicationSettings.FirstOrDefault();
			List<Currency> currencies = context.Currencies.ToList();
			
			var item = currencies.First(i => i.Id == applicationSettings.CurrencyId);

			AuctionModel model = new AuctionModel();
			model.TokenValue = applicationSettings.TokenValue;
			model.Currency = item.Name;
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(AuctionModel model)
		{
			if (ModelState.IsValid)
			{
				HttpPostedFileBase file = model.ImagePath;

				context = new ApplicationDbContext();
				if (file != null && file.ContentLength > 0)
					try
					{
						//string path = Path.Combine(Server.MapPath("~/Images"),
						//                           Path.GetFileName(file.FileName));
						string FileLocation = string.Format("{0}/{1}", HttpContext.Server.MapPath("~/Uploads/Images"), file.FileName);
						file.SaveAs(FileLocation);

						//file.SaveAs(path);

					}
					catch (Exception ex)
					{
						ViewBag.Message = "ERROR:" + ex.Message.ToString();
					}
				else
				{
					ViewBag.Message = "You have not specified a file.";
				}

				Auction auction = new Auction
				{
					Name = model.Name,
					ImagePath = file.FileName.ToString(),
					DurationTime = model.DurationTime,
					StartPrice = model.StartPrice,
					AuctionStatusId = 1,
					CreatedDate = DateTime.Now,
					CurrentPrice = model.StartPrice,
					TokenValue = model.TokenValue,
					Currency = model.Currency
				};

				context.Auctions.Add(auction);
				context.SaveChanges();

				// ako ovo ok prodje, onda mozes da uradis prebacivanje npr. na HomePage
				return RedirectToAction("Index", "Auction");
			}
			//ako dodjes ovde, znaci da je doslo do neke greske i potrebn je vratiti se na isti View

			return View("NewAuction", model);
		}

		public ActionResult Edit(int Id)
		{
			context = new ApplicationDbContext();
			var item = context.Auctions.Where(m => m.Id == Id &&
											m.AuctionStatusId == (int)EnAuctionStatuses.OPENED).FirstOrDefault(); ;

			AuctionModel model = new AuctionModel();
			if (item != null)
			{
				model.Img = item.ImagePath;
			}


			return View("Edit", model);
		}

		public ActionResult OpenAuctions()
		{
			return View("OpenAuctions");
		}

		public ActionResult ChangeStatus (int id)
		{
			context = new ApplicationDbContext();
			var item = context.Auctions.Where(m => m.Id == id).FirstOrDefault();
			item.AuctionStatusId = 3;
			context.SaveChanges();
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Open ()
		{
			
				context = new ApplicationDbContext();
				var data = context.Auctions.Where(m =>(m.AuctionStatusId == 1)).OrderByDescending(m => m.CreatedDate).Take(10).ToList();
				List<AuctionModel> model = new List<AuctionModel>();
				foreach (var item in data)
				{
						model.Add(new AuctionModel()
						{
							Id = item.Id,
							Name = item.Name,
							DurationTime = item.DurationTime,
							StartPrice = item.StartPrice,
							CurrentPrice = item.CurrentPrice,
							Img = item.ImagePath,
							AuctionStatusId = item.AuctionStatusId,
							AuctionStatus = item.AuctionStatus.Name,
							Currency = item.Currency
						});
					
				}

				return PartialView("_OpenPartial",model);
		}

		public ActionResult OpenAuction(int id)
		{
			

			context = new ApplicationDbContext();
			var auction = context.Auctions.Where(m => m.Id == id).FirstOrDefault();
			auction.AuctionStatusId = 2;
			auction.OpenedDate = DateTime.Now;
			auction.ClosedDate = DateTime.Now.AddSeconds(auction.DurationTime);
			context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Edit(AuctionModel model)
		{
			return View();
		}
		
		public ActionResult Details(int Id)
		{
			AuctionModel mymodel = new AuctionModel();
			mymodel.Id = Id;

			return View(mymodel);
		}

		public ActionResult DetailsPartialView(int Id)
		{
			var item = context.Auctions.Where(m => m.Id == Id).FirstOrDefault();
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			AuctionModel mymodel = new AuctionModel();
			var user = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();
			if (item != null && item.AuctionBiderId != 0)
			{
				mymodel.Id = item.Id;
				mymodel.Name = item.Name;
				mymodel.Img = item.ImagePath;
				mymodel.CurrentPrice = item.CurrentPrice;
				mymodel.DurationTime = item.DurationTime;
				mymodel.AuctionStatus = item.AuctionStatus.Name;
				mymodel.ClosedDate = item.ClosedDate.Value;
				mymodel.AuctionStatusId = item.AuctionStatusId;
				mymodel.AuctionBiderId = item.AuctionBiderId;
				mymodel.AuctionBider = user.FirstName + " " + user.LastName;
				mymodel.Currency = item.Currency;
				mymodel.TokenValue = item.TokenValue;

			} else
			{
				mymodel.Id = item.Id;
				mymodel.Name = item.Name;
				mymodel.Img = item.ImagePath;
				mymodel.CurrentPrice = item.CurrentPrice;
				mymodel.DurationTime = item.DurationTime;
				mymodel.AuctionStatus = item.AuctionStatus.Name;
				mymodel.ClosedDate = item.ClosedDate.Value;
				mymodel.AuctionStatusId = item.AuctionStatusId;
				mymodel.AuctionBiderId = item.AuctionBiderId;
				mymodel.Currency = item.Currency;
				mymodel.TokenValue = item.TokenValue;
			}

			var data = context.Bids.Where(m => (m.Auction == item.Id)).OrderByDescending(m => m.BidTime).Take(10).ToList();

			mymodel.Bids = new List<BidModel>();
			foreach (var item1 in data)
			{
				mymodel.Bids.Add(new BidModel() { Id = item1.Id, UserID = item1.UserID, ContactName = item1.ContactName, TokenNumber = item1.TokenNumber, BidTime = item1.BidTime });
			}

			return PartialView("_Details", mymodel);
		}


		public ActionResult BidNow(int productId, decimal TokenNum)
		{
			int userId = Int32.Parse(User.Identity.GetUserId());
			context = new ApplicationDbContext();
			var item = context.Auctions.Where(m => m.Id == productId).FirstOrDefault();
		    var sda =  HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			var user = sda.Users.Where(m => m.Id == userId).FirstOrDefault();
			var bid = context.Bids.Where(m => m.Auction == productId).OrderByDescending(m => m.BidTime).FirstOrDefault();

			if (bid != null)
			{
				if (bid.UserID == user.Id)
			{
				if (TokenNum <= (user.Tokens + bid.TokenNumber))
				{
					Bid ponuda = new Bid
					{
						Auction = productId,
						UserID = userId,
						TokenNumber = TokenNum,
						BidTime = DateTime.Now,
						ContactName = user.FirstName + " " + user.LastName
					};

					item.CurrentPrice = TokenNum * item.TokenValue;
					item.AuctionBiderId = userId;
					user.Tokens = user.Tokens - TokenNum + bid.TokenNumber;
					sda.Update(user);
					context.Bids.Add(ponuda);
					context.SaveChanges();

					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = false }, JsonRequestBehavior.AllowGet);
				}
			}else
				{
					if (TokenNum <= user.Tokens)
					{
						Bid ponuda = new Bid
						{
							Auction = productId,
							UserID = userId,
							TokenNumber = TokenNum,
							BidTime = DateTime.Now,
							ContactName = user.FirstName + " " + user.LastName
						};
						var user1 = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();
						if (user1 != null)
						{
							user1.Tokens += bid.TokenNumber;
							sda.Update(user);
						}

						item.CurrentPrice = TokenNum * item.TokenValue;
						item.AuctionBiderId = userId;
						user.Tokens = user.Tokens - TokenNum;
						sda.Update(user);
						context.Bids.Add(ponuda);
						context.SaveChanges();


						return Json(new { success = true }, JsonRequestBehavior.AllowGet);
					}
					else
					{
						return Json(new { success = false }, JsonRequestBehavior.AllowGet);
					}
				}
			

			}
			else
			{
				if (TokenNum <= user.Tokens)
				{
					Bid ponuda = new Bid
					{
						Auction = productId,
						UserID = userId,
						TokenNumber = TokenNum,
						BidTime = DateTime.Now,
						ContactName = user.FirstName + " " + user.LastName
					};
					var user1 = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();
					if (user1 != null)
					{
						user1.Tokens += bid.TokenNumber;
						sda.Update(user);
					}
					
					item.CurrentPrice = TokenNum * item.TokenValue;
					item.AuctionBiderId = userId;
					user.Tokens = user.Tokens - TokenNum;
					sda.Update(user);
					context.Bids.Add(ponuda);
					context.SaveChanges();


					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = false }, JsonRequestBehavior.AllowGet);
				}

			}
			
		}


		public ActionResult GetDefaultList()
		{

			var data = context.Auctions.Where(m => (m.AuctionStatusId == 2)).OrderByDescending(m => m.OpenedDate).Take(10).ToList();
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			List<AuctionModel> model = new List<AuctionModel>();
			foreach (var item in data)
			{
				if (item.AuctionBiderId != 0)
				{
					var user = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();

					model.Add(new AuctionModel()
					{
						Id = item.Id,
						Name = item.Name,
						DurationTime = item.DurationTime,
						ClosedDate = item.ClosedDate.Value,
						StartPrice = item.StartPrice,
						CurrentPrice = item.CurrentPrice,
						Img = item.ImagePath,
						AuctionStatusId = item.AuctionStatusId,
						AuctionStatus = item.AuctionStatus.Name,
						AuctionBiderId = item.AuctionBiderId,
						AuctionBider = user.FirstName + " " + user.LastName
					});
				}
				else
				{
					model.Add(new AuctionModel()
					{
						Id = item.Id,
						Name = item.Name,
						DurationTime = item.DurationTime,
						ClosedDate = DateTime.Now.AddMinutes(60),
						StartPrice = item.StartPrice,
						CurrentPrice = item.CurrentPrice,
						Img = item.ImagePath,
						AuctionStatusId = item.AuctionStatusId,
						AuctionStatus = item.AuctionStatus.Name,
						AuctionBiderId = item.AuctionBiderId,
					});
				}
			}

			return PartialView("_List", model);
		}

		public ActionResult GetList(int min = 0, int max = 0, string searchText = "", int auctionTypeId = 2)
		{
			ApplicationSettings applicationSettings = context.ApplicationSettings.FirstOrDefault();


			var data = context.Auctions.Where(m => (min == 0 || m.StartPrice >= min) &&
												   (max == 0 || m.StartPrice <= max) &&
												   (string.IsNullOrEmpty(searchText) || m.Name.Contains(searchText)) &&
												   ( m.AuctionStatusId == auctionTypeId)).OrderByDescending(m => m.OpenedDate).Take(applicationSettings.AuctionItems).ToList();
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			List<AuctionModel> model = new List<AuctionModel>();
			foreach (var item in data)
			{
				if (item.AuctionBiderId != 0)
				{
					var user = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();
					
						model.Add(new AuctionModel()
						{
							Id = item.Id,
							Name = item.Name,
							DurationTime = item.DurationTime,
							ClosedDate = item.ClosedDate.Value,
							StartPrice = item.StartPrice,
							CurrentPrice = item.CurrentPrice,
							Img = item.ImagePath,
							AuctionStatusId = item.AuctionStatusId,
							AuctionStatus = item.AuctionStatus.Name,
							AuctionBiderId = item.AuctionBiderId,
							AuctionBider = user.FirstName + " " + user.LastName,
							Currency = item.Currency,
							TokenValue = item.TokenValue
						});
				}else
				{
					model.Add(new AuctionModel()
					{
						Id = item.Id,
						Name = item.Name,
						DurationTime = item.DurationTime,
						ClosedDate = item.ClosedDate.Value,
						StartPrice = item.StartPrice,
						CurrentPrice = item.CurrentPrice,
						Img = item.ImagePath,
						AuctionStatusId = item.AuctionStatusId,
						AuctionStatus = item.AuctionStatus.Name,
						AuctionBiderId = item.AuctionBiderId,
						Currency = item.Currency,
						TokenValue = item.TokenValue
					});
				}
			}

			return PartialView("_List", model);
		}

		public ActionResult WonAuctionList()
		{
			int id = Int32.Parse(User.Identity.GetUserId()); ;
			var data = context.Auctions.Where(m => m.AuctionBiderId == id && m.AuctionStatusId == 3).OrderByDescending(m => m.ClosedDate).ToList();
			var sda = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			List<AuctionModel> model = new List<AuctionModel>();

			foreach (var item in data)
			{
					var user = sda.Users.Where(m => m.Id == item.AuctionBiderId).FirstOrDefault();

					model.Add(new AuctionModel()
					{
						Id = item.Id,
						Name = item.Name,
						DurationTime = item.DurationTime,
						ClosedDate = item.ClosedDate.Value,
						StartPrice = item.StartPrice,
						CurrentPrice = item.CurrentPrice,
						Img = item.ImagePath,
						AuctionStatusId = item.AuctionStatusId,
						AuctionStatus = item.AuctionStatus.Name,
						AuctionBiderId = item.AuctionBiderId,
						AuctionBider = user.FirstName + " " + user.LastName,
						Currency = item.Currency,
						TokenValue = item.TokenValue
					});
				
			}

			return View("ListaDobijenihAukcija", model);
		}



	}
}