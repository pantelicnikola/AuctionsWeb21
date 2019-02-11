using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AuctionsWeb2.Hubs
{
    public class MyHub : Hub
    {
        public void AnnounceBid(string auctionName, string username, string newValue, string numTokens)
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hc.Clients.All.announceBid(auctionName, username, newValue, numTokens);
        }

        public static void Hello()
        {
            IHubContext hc = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hc.Clients.All.hello();
        }
    }
}