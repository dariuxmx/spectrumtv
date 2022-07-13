using Autofac;
using SpectrumTV.PageModels;

namespace SpectrumTV.Modules
{
    public class PageModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HomePageModel>();
            builder.RegisterType<MovieDetailsPageModel>();
        }
    }
}
