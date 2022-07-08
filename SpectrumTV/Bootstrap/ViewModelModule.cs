using Autofac;
using SpectrumTV.ViewModels;

namespace SpectrumTV.Bootstrap
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<InitViewModel>();
        }
    }
}
