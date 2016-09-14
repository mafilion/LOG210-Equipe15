using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionBibliotheque.Startup))]
namespace GestionBibliotheque
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
