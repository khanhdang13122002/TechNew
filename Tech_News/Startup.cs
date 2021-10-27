using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tech_News.Startup))]
namespace Tech_News
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
