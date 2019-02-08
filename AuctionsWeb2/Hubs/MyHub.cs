using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AuctionsWeb2.Hubs
{
    public class MyHub : Hub
    {
        public void AnnounceBid(string auctionName, string username, string newValue)
        {
            Clients.All.announceBid(auctionName, username, newValue);
        }
    }
}