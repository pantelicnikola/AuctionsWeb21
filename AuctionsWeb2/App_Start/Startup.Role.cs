
using AuctionsWeb2.App_Start;
using System.Threading.Tasks;

namespace AuctionsWeb2
{
    public partial class Startup
    {
        public async Task ConfigureRole()
        {
            RoleConfig rolesData = new RoleConfig();
            await rolesData.EnsureSeedDataAsync();
        }
    }
}