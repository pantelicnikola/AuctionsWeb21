using AuctionsWeb2.Constants;
using AuctionsWeb2.Enums;
using AuctionsWeb2.Hubs;
using AuctionsWeb2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionsWeb2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
            
        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.Message = "Your Search page.";
            SearchViewModel model = new SearchViewModel();

            var db = new auctiondbEntities();
            var auctions = db.Auctions.AsQueryable();
            auctions = auctions.Where(a => a.State.Equals(AuctionStates.OPEN.ToString())).OrderByDescending(a => a.TimeOpen).Take(SystemParameters.DEFAULT_NUMBER_AUCTIONS);
            var auctionsList = new List<Auction>();
            auctionsList = auctions.ToList();
            auctionsList = closeExpiredAuctions(auctionsList);
            model.Auctions = auctionsList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            SearchViewModel newModel = new SearchViewModel();
            List<Auction> auctionList = getSearchResult(model);
            auctionList = closeExpiredAuctions(auctionList);
            newModel.Auctions = auctionList;

            return View(newModel);
        }

        public List<Auction> closeExpiredAuctions(List<Auction> auctionList)
        {
            foreach (Auction auction in auctionList.ToList())
            {
                if (auction.State.Contains(AuctionStates.OPEN.ToString()) && auction.TimeEnd < DateTime.Now)
                {
                    var db = new auctiondbEntities();
                    var newAuction = db.Auctions.Where(a => a.Id == auction.Id).First();
                    newAuction.State = AuctionStates.CLOSED.ToString();
                    var bids = newAuction.Bids.OrderByDescending(b => b.Time);
                    if (bids.Any())
                    {
                        var winner = bids.First().AspNetUser;
                        newAuction.Winner = winner.Id;
                        newAuction.PriceEnd = newAuction.PriceNow;
                        winner.NumTokens -= newAuction.TotalTokens;
                    }
                    db.SaveChanges();
                    auctionList.Remove(auction);
                }
            }
            return auctionList;
        }

        public List<Auction> getSearchResult(SearchViewModel model)
        {
            var auctionsList = new List<Auction>();

            var db = new auctiondbEntities();
            var auctions = db.Auctions.AsQueryable();

            if (model.Name != null)
            {
                auctions = auctions.Where(a => a.Name.Contains(model.Name));
            }
            if (model.MinPrice != null)
            {
                auctions = auctions.Where(a => a.PriceNow.Value > model.MinPrice);
            }
            if (model.MaxPrice != null)
            {
                auctions = auctions.Where(a => a.PriceNow.Value < model.MaxPrice);
            }
            if (model.MinDate != null)
            {
                auctions = auctions.Where(a => a.TimeCreate > model.MinDate);
            }
            if (model.MaxDate != null)
            {
                auctions = auctions.Where(a => a.TimeCreate < model.MaxDate);
            }
            if (model.State != null)
            {
                auctions = auctions.Where(a => a.State.Equals(model.State.ToString()));
            }
            
            auctionsList = auctions.ToList();
            return auctionsList;

        }

        public ActionResult Auction(int id)
        {
            AuctionViewModel model = new AuctionViewModel();
            var entity = new auctiondbEntities();
            model.Auction = entity.Auctions.Single<Auction>(a => a.Id.Equals(id));
            return View(model);
        }
        [Authorize]
        public ActionResult CreateBid(SearchViewModel model, int auctionId)
        {
            var db = new auctiondbEntities();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var auction = db.Auctions.First(a => a.Id == auctionId);
            if (user.NumTokens >= model.numTokens && model.numTokens > 0)
            {
                var bid = new Bid()
                {
                    IdUser = User.Identity.GetUserId(),
                    IdAuction = auctionId,
                    Amount = model.numTokens,
                    Time = System.DateTime.Now
                };

                db.Bids.Add(bid);
                auction.PriceNow += model.numTokens * SystemParameters.TOKEN_VALUE;
                auction.TotalTokens += model.numTokens;

                user.NumTokens -= model.numTokens; 

                db.SaveChanges();
                ViewBag.ErrorMessage = "Success";
                MyHub.Hello();
                //AuctionViewModel auctionViewModel = new AuctionViewModel() { Auction = auction };
                return RedirectToAction("Auction", new { id = auction.Id } );
            }
            
            ViewBag.ErrorMessage = "Insufficient number of tokens";
            return RedirectToAction("Search");
            
        }

    }
}
