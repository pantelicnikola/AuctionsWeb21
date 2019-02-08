using AuctionsWeb2.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionsWeb2.Models
{
    public class OpenAuctionsModel
    {
        public List<Auction> auctions { get; set; }
    }

    public class ApproveTokensModel
    {
        public List<Order> orders { get; set; }
    }

    public class ControlPanelModel
    {
        public ControlPanelModel()
        {
            this.defaultNumberAuctions = SystemParameters.DEFAULT_NUMBER_AUCTIONS;
            this.tokenValue = SystemParameters.TOKEN_VALUE;
            this.defaultCurrency = SystemParameters.DEFAULT_CURRENCY.ToString();
            this.defaultAuctionDuration = SystemParameters.DEFAULT_AUCTION_DURATION;
            this.silverPackage = SystemParameters.SILVER_PACKAGE;
            this.goldenPackage = SystemParameters.GOLDER_PACKAGE;
            this.platinumPackage = SystemParameters.PLATINUM_PACKAGE;
            this.eurDin = SystemParameters.EUR_DIN;
            this.usdDin = SystemParameters.USD_DIN;
            this.usdEur = SystemParameters.USD_EUR;
        }

        public int defaultNumberAuctions { get; set; }
        public decimal tokenValue { get; set; }
        public string defaultCurrency { get; set; }
        public int defaultAuctionDuration { get; set; }
        public int silverPackage { get; set; }
        public int goldenPackage { get; set; }
        public int platinumPackage { get; set; }
        public decimal eurDin { get; set; }
        public decimal usdDin { get; set; }
        public decimal usdEur { get; set; }


    }
}