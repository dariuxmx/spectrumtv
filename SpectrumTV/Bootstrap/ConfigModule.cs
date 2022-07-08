using Autofac;

namespace SpectrumTV.Bootstrap
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.RegisterType<UserContext>().SingleInstance();
        }
        
    }
}
