using AutoMapper;
using Microsoft.Owin;
using Owin;
using PodcastFeedGenerator.Infrastructure;

[assembly: OwinStartupAttribute(typeof(PodcastFeedGenerator.Startup))]
namespace PodcastFeedGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Mapper.Initialize(cfg => AutoMapConfigurator.Configurate(cfg));
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
