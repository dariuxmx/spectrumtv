using System;
using Autofac;
using SpectrumTV.Modules;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpectrumTV.iOS.iOSModule))]
namespace SpectrumTV.iOS
{
    public class iOSModule : RuntimePlatformModule
    {
        // Autofac container
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
