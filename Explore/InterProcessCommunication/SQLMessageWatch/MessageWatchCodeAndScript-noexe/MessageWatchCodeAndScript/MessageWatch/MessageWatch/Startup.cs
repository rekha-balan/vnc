using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessageWatch.Startup))]
namespace MessageWatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
