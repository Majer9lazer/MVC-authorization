using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_authorization.Startup))]
namespace MVC_authorization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
