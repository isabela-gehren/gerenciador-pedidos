using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GEPED.Web.Startup))]
namespace GEPED.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
