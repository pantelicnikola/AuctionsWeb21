using AuctionsWeb2.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuctionsWeb2.Models
{
    public class SearchViewModel
    {

        [Display(Name = "Auction Name")]
        public string Name { get; set; }

        [Display(Name = "Min Price")]
        public int? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public int? MaxPrice { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Min Date")]
        public DateTime? MinDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Max Date")]
        public DateTime? MaxDate { get; set; }

        [Display(Name = "State")]
        public AuctionStates? State { get; set; }

        public List<Auction> Auctions { get; set; }
        
        public int idAuction { get; set; }
        public int numTokens { get; set; }

    }


    public class AuctionViewModel
    {
        public Auction Auction { get; set; }
    }

    //public class BidModalModel
    //{

    //    [Display(Name = "Bid Amount")]
    //    public int BidAmount { get; set; }

    //    //public string AuctionName { get; set; }

    //    //public string username{ get; set; }

    //    public BidModalModel() { }


    //}
    public class BidModalModel
    {

        [Display(Name = "Bid Amount")]
        public int BidAmount { get; set; }

        public BidModalModel() { }


    }
}