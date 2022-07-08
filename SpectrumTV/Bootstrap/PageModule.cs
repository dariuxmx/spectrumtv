using Autofac;
using SpectrumTV.Pages;

namespace SpectrumTV.Bootstrap
{
    public class PageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HomePage>();
        }
    }
}
