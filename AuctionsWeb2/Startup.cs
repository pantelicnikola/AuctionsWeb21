using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuctionsWeb2.Startup))]
namespace AuctionsWeb2
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            await ConfigureRole();
        }
    }
}
