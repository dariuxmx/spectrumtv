using System;
using Autofac;
using SpectrumTV.Interfaces;
using SpectrumTV.Interfaces.Logging;
using SpectrumTV.Services;

namespace SpectrumTV.Bootstrap
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterUnconditionalServices(builder);
            RegisterDebugServices(builder);
        }

        private void RegisterUnconditionalServices(ContainerBuilder builder)
        {
            builder.RegisterType<PageFactory>().SingleInstance();
            builder.RegisterType<NavigationContext>(); // Instance per page
        }

        private void RegisterDebugServices(ContainerBuilder builder)
        {
            builder.RegisterType<DebugLogger>().As<ILogger>().SingleInstance();
        }
    }
}
