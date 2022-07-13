using System;
using Autofac;
using SpectrumTV.Interfaces;
using SpectrumTV.Interfaces.Logging;
using SpectrumTV.Services;
using SpectrumTV.Services.ApiService;
using SpectrumTV.Services.Implementation;

namespace SpectrumTV.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterUnconditionalServices(builder);
#if DEBUG
            RegisterDebugServices(builder);
#endif
        }

        private void RegisterUnconditionalServices(ContainerBuilder builder)
        {
            // All services should be register here
            builder.RegisterType<PageFactory>().SingleInstance();
            builder.RegisterType<NavigationContext>(); // Instance per page
            builder.RegisterType<ApiService>().As<IApiService>().SingleInstance();
            builder.RegisterType<MovieService>().As<IMovieService>().SingleInstance();
        }

        private void RegisterDebugServices(ContainerBuilder builder)
        {
            builder.RegisterType<DebugLogger>().As<ILogger>().SingleInstance();
        }
    }
}
