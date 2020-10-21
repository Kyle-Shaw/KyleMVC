using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KyleMVC.Startup))]
namespace KyleMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
