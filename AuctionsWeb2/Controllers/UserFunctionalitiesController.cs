using AuctionsWeb2.Enums;
using AuctionsWeb2.Constants;
using AuctionsWeb2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuctionsWeb2.Controllers
{
    [Authorize]
    public class UserFunctionalitiesController : Controller
    {
        // GET: UserFunctionalities
        public ActionResult CreateAuction()
        {
            ViewBag.Message = "Create Auction page.";
            var model = new CreateAuctionModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuction(CreateAuctionModel model)
        {
            if (ModelState.IsValid)
            {
                var auction = new Auction
                {
                    IdUser = User.Identity.GetUserId(),
                    Name = model.AuctionName,
                    Duration = (model.Duration == 0) ? SystemParameters.DEFAULT_AUCTION_DURATION : model.Duration,
                    Photo = model.PhotoURL,
                    PriceStart = model.PriceStart,
                    PriceNow = model.PriceStart,
                    State = "READY",
                    TimeCreate = System.DateTime.Now,
                    TotalTokens = 0
                };

                var db = new auctiondbEntities();
                db.Auctions.Add(auction);
                await db.SaveChangesAsync();
                return Redirect("/");
            }
            return View();
        }


        public ActionResult ShopTokens()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShopTokens(ShopTokensModel model)
        {
            if (model.NumTokens == 0)
            {
                ViewBag.ErrorMessage = "Choose one of three token packages";
                return View();
            }
            TempData["ShopTokenInfo"] = new ShopTokensModel(model.Currency, model.NumTokens);
            return Redirect("PurchaseTokens");
        }

        public ActionResult PurchaseTokens()
        {
            var db = new auctiondbEntities();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            ShopTokensModel shopTokenInfo = TempData["ShopTokenInfo"] as ShopTokensModel;
            PurchaseTokensModel model = new PurchaseTokensModel()
            {
                Currency = shopTokenInfo.Currency,
                NumTokens = shopTokenInfo.NumTokens,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalPrice = shopTokenInfo.NumTokens * SystemParameters.TOKEN_VALUE
            };
            TempData["PurchaseTokensModel"] = model;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> PurchaseTokens(PurchaseTokensModel modele)
        {
            var db = new auctiondbEntities();
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            PurchaseTokensModel model = TempData["PurchaseTokensModel"] as PurchaseTokensModel;
            var order = new Order()
            {
                IdUser = User.Identity.GetUserId(),
                NumTokens = model.NumTokens,
                Price = model.TotalPrice,
                State = OrderStates.SUBMITTED.ToString()
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Redirect("/");
        }

        public ActionResult Orders()
        {
            var db = new auctiondbEntities();
            var id = User.Identity.GetUserId();
            var user = db.AspNetUsers.First(u => u.Id == id);
            var orders = user.Orders;
            return View(orders);
        }

        public ActionResult WonAuctions()
        {
            var model = new WonAuctionsModel();
            model.auctions = new List<Auction>();
            var db = new auctiondbEntities();
            var userId = User.Identity.GetUserId();
            var auctions = db.Auctions;
            foreach (var auction in auctions)
            {
                if (auction.Winner == userId)
                {
                    model.auctions.Add(auction);
                }
            }
            return View(model);
        }

        public ActionResult AuctionDetails(int idAuction)
        {

            return Redirect("~/Home/Auction/" + idAuction);
        }
    }
}
