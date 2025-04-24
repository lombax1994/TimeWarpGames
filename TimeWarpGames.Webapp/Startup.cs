using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeWarpGames.Webapp.Startup))]
namespace TimeWarpGames.Webapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
