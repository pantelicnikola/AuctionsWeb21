using AuctionsWeb2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuctionsWeb2.Constants
{
    public class SystemParameters
    {
        public static int DEFAULT_NUMBER_AUCTIONS { get; private set; } = 10;

        public static decimal TOKEN_VALUE { get; set; } = 10m;

        public static Currencies DEFAULT_CURRENCY { get; set; } = Currencies.EUR;

        public static int DEFAULT_AUCTION_DURATION { get; set; } = 60;

        public static int SILVER_PACKAGE { get; set; } = 30;
        public static int GOLDER_PACKAGE { get; set; } = 50;
        public static int PLATINUM_PACKAGE { get; set; } = 100;

        public static decimal EUR_DIN { get; set; } = 118.2951m;
        public static decimal USD_DIN { get; set; } = 102.4377m;
        public static decimal USD_EUR { get; set; } = 0.8638m;
    }
}