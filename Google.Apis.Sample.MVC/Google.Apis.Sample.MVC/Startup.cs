using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Google.Apis.Sample.MVC.Startup))]
namespace Google.Apis.Sample.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
