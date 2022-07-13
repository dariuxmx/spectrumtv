using Autofac;
using SpectrumTV.ViewModels;

namespace SpectrumTV.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // All viewModels should be register here
            builder.RegisterType<InitViewModel>();
            builder.RegisterType<MovieItemViewModel>();
        }
    }
}
