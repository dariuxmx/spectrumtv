using Autofac;

namespace SpectrumTV.Modules
{
    public class ViewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // All views should be register here
            base.Load(builder);
        }
    }
}
