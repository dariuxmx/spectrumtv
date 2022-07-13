
using Autofac;
using SpectrumTV.Modules;
using SpectrumTV.PageModels;
using SpectrumTV.Pages;
using SpectrumTV.Services;
using Xamarin.Forms;

namespace SpectrumTV.Bootstrap
{
    public static class Bootstrapper
    {
        public static IContainer Run()
        {
            var builder = new ContainerBuilder();

            // Register modules
            builder.RegisterModule<ViewModule>();
            builder.RegisterModule<ViewModelModule>();
            builder.RegisterModule<PageModule>();
            builder.RegisterModule<PageModelModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ConfigModule>();

            // Register iOS or Android Module
            var platformModule = DependencyService.Get<RuntimePlatformModule>();
            builder.RegisterModule(platformModule);

            IContainer container = builder.Build();

            // Create the AutoFac pageFactory
            var pageFactory = container.Resolve<PageFactory>();

            RegisterPages(pageFactory);

            return container;
        }

        private static void RegisterPages(PageFactory pageFactory)
        {
            // All pages with their pageModel should be register here
            pageFactory.Register<HomePageModel, HomePage>();
            pageFactory.Register<MovieDetailsPageModel, MovieDetailsPage>();
            pageFactory.Register<SearchResultsPageModel, SearchResultsPage>();
        }
    }
}
