using Autofac;
using SpectrumTV.Pages;

namespace SpectrumTV.Modules
{
    public class PageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HomePage>();
            builder.RegisterType<MovieDetailsPage>();
        }
    }
}
