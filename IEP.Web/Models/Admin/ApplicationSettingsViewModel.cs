using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEP.Web.Models.Admin
{
    public class ApplicationSettingsViewModel
    {
        [Required]
        [Range(0, 100)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Auction Items Must Be Number")]
        [Display(Name = "Auction Items")]
        public int AuctionItems { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Auction Duration Must Be Number")]
        [Display(Name = "Auction Duration")]
        public int AuctionDuration { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Silver Package Must Be Number")]
        [Display(Name = "Silver Package")]
        public int SilverPackageTokens { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Gold Package Must Be Number")]
        [Display(Name = "Gold Package")]
        public int GoldPackageTokens { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Platinum Package Must Be Number")]
        [Display(Name = "Platinum Package")]
        public int PlatinumPackageTokens { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"^[0-9]([.,][0-9]{1,3})?$", ErrorMessage = "Token Value Must Be Number")]
        [Display(Name = "Token Value")]
        public decimal TokenValue { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public List<SelectListItem> Currencies { get; set; }
    }
}