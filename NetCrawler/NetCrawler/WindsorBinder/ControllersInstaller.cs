using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NetCrawler.Services;
using System.Web.Mvc;

namespace NetCrawler.WindsorBinder
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            RegisterServices(container);
        }

        private void RegisterServices(
            IWindsorContainer container
        )
        {
            container.Register(Component.For<IHttpService>().ImplementedBy<HttpService>());
            container.Register(Component.For<ICrawlerService>().ImplementedBy<CrawlerService>());               
        }
    }
}