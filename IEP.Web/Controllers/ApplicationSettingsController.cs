using IEP.Data.dbContextManager;
using IEP.Data.Domain;
using IEP.Web.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEP.Web.Controllers
{
    public class ApplicationSettingsController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: ApplicationSettings
        public ActionResult Index()
        {
            ApplicationSettingsViewModel model = new ApplicationSettingsViewModel();
            ApplicationSettings applicationSettings = context.ApplicationSettings.FirstOrDefault();
            List<Currency> currencies = context.Currencies.ToList();
            model.Currencies = currencies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();

            if (applicationSettings != null)
            {
                model.AuctionItems = applicationSettings.AuctionItems;
                model.SilverPackageTokens = applicationSettings.SilverPackageTokens;
                model.GoldPackageTokens = applicationSettings.GoldPackageTokens;
                model.PlatinumPackageTokens = applicationSettings.PlatinumPackageTokens;
                model.TokenValue = applicationSettings.TokenValue;
                model.CurrencyId = applicationSettings.CurrencyId;
            }


            
            return View(model);
        }

        



        [HttpPost]
        public ActionResult ApplicationSettings(ApplicationSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationSettings applicationSettings = context.ApplicationSettings.FirstOrDefault();

                applicationSettings.AuctionItems = model.AuctionItems;
                applicationSettings.GoldPackageTokens = model.GoldPackageTokens;
                applicationSettings.SilverPackageTokens = model.SilverPackageTokens;
                applicationSettings.PlatinumPackageTokens = model.PlatinumPackageTokens;
                applicationSettings.TokenValue = model.TokenValue;
                applicationSettings.CurrencyId = model.CurrencyId;
                
                context.SaveChanges();
                
                return RedirectToAction("Index");
            }

            List<Currency> currencies = context.Currencies.ToList();
            model.Currencies = currencies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();

            return View("Index", model);
        }

    }
}