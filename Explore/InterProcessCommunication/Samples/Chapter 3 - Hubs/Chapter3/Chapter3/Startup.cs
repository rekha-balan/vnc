using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chapter3.Startup))]
namespace Chapter3
{

    public class Startup
    {
        void Configuration(IAppBuilder app)
        {
            //app.MapHubs();

            app.MapSignalR();
        }
    }
}