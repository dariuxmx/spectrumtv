using Autofac;
using SpectrumTV.Pages;

namespace SpectrumTV.Modules
{
    public class PageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // All pages should be register here
            builder.RegisterType<HomePage>();
            builder.RegisterType<MovieDetailsPage>();
        }
    }
}
