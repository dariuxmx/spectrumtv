using Autofac;
using SpectrumTV.PageModels;

namespace SpectrumTV.Bootstrap
{
    public class PageModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HomePageModel>();
        }
    }
}
