using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampMa8.Startup))]
namespace CampMa8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
