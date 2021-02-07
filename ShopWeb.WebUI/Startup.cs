using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopWeb.WebUI.Startup))]
namespace ShopWeb.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
