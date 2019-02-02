using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEP.Data.Domain
{
    public class ApplicationSettings
    {
        [Key]
        public string Id { get; set; }
        public int AuctionItems { get; set; }
        public int AuctionDuration { get; set; }
        public int SilverPackageTokens { get; set; }
        public int GoldPackageTokens { get; set; }
        public int PlatinumPackageTokens { get; set; }
        public decimal TokenValue { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
