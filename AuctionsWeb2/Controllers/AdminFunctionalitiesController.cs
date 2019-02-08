using AuctionsWeb2.Enums;
using AuctionsWeb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionsWeb2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminFunctionalitiesController : Controller
    {
        [HttpGet]
        public ActionResult OpenAuctions(OpenAuctionsModel model)
        {
            model = new OpenAuctionsModel();
            auctiondbEntities entities = new auctiondbEntities();
            model.auctions = entities.Auctions.Where(x => x.State.Equals("READY")).ToList();
            return View(model);
        }

        public ActionResult Add(int id)
        {
            auctiondbEntities db = new auctiondbEntities();
            var auction = db.Auctions.First(a => a.Id == id);
            auction.State = AuctionStates.OPEN.ToString();
            auction.TimeOpen = DateTime.Now;
            auction.TimeEnd = DateTime.Now.AddSeconds((int)auction.Duration);
            db.SaveChanges();

            return RedirectToAction("OpenAuctions");
        }

        public ActionResult ApproveTokens()
        {
            var model = new ApproveTokensModel();
            auctiondbEntities db = new auctiondbEntities();
            List<Order> orders = db.Orders.Where(o => o.State == OrderStates.SUBMITTED.ToString()).ToList();
            model.orders = orders;
            return View(model);

        }

        public ActionResult Approve(int id)
        {
            auctiondbEntities db = new auctiondbEntities();
            var order = db.Orders.First(o => o.Id == id);
            order.State = OrderStates.COMPLETED.ToString();
            order.AspNetUser.NumTokens += order.NumTokens;
            db.SaveChanges();
            return RedirectToAction("ApproveTokens");

        }

        public ActionResult Cancel(int id)
        {
            auctiondbEntities db = new auctiondbEntities();
            var order = db.Orders.First(o => o.Id == id);
            order.State = OrderStates.CANCELED.ToString();
            db.SaveChanges();
            return RedirectToAction("ApproveTokens");
        }

        public ActionResult ControlPanel()
        {
            var model = new ControlPanelModel();
            return View(model);
        }
    }
}