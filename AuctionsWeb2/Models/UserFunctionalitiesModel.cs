using AuctionsWeb2.Enums;
using AuctionsWeb2.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuctionsWeb2.Models
{
    public class CreateAuctionModel
    {

        [Required]
        [Display(Name = "Auction Name")]
        public string AuctionName { get; set; }

        [Required]
        [Display(Name = "Photo URL")]
        public string PhotoURL { get; set; }

        [Required]
        [Display(Name = "Price Start")]
        public decimal PriceStart { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public int Duration { get; set; } = SystemParameters.DEFAULT_AUCTION_DURATION;


    }

    public class ShopTokensModel
    {
        public int NumTokens { get; set; }

        public Currencies Currency { get; set; }

        public ShopTokensModel(Currencies currency, int numTokens)
        {
            Currency = currency;
            NumTokens = numTokens;
        }

        public ShopTokensModel() { }

        //public ShopTokensModel()
        //{
        //    Currency = SystemParameters.DEFAULT_CURRENCY;
        //}

    }

    public class PurchaseTokensModel
    {
        public Currencies Currency { get; set; }

        public int NumTokens { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Total price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Credit Card Number")]
        public string CreditCard { get; set; }

        public PurchaseTokensModel(Currencies currency, int numTokens)
        {
            Currency = currency;
            NumTokens = numTokens;
        }

        public PurchaseTokensModel() { }

    }

    public class WonAuctionsModel
    {
        public List<Auction> auctions { get; set; }
    }
}