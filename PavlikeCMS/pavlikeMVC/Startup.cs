using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pavlikeMVC.Startup))]
namespace pavlikeMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
     
            ConfigureAuth(app);
        }
        
    }
}
