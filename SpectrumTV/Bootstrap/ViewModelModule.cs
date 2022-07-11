using Autofac;
using SpectrumTV.ViewModels;

namespace SpectrumTV.Modules
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
