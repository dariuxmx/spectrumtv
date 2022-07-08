
using Autofac;
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

            var pageFactory = container.Resolve<PageFactory>();

            RegisterPages(pageFactory);

            return container;
        }

        private static void RegisterPages(PageFactory pageFactory)
        {
            pageFactory.Register<HomePageModel, HomePage>();
        }
    }
}
