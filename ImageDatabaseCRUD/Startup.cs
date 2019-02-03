using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageDatabaseCRUD.Startup))]
namespace ImageDatabaseCRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
