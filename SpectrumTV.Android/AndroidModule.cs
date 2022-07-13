using System;
using Autofac;
using SpectrumTV.Modules;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpectrumTV.Droid.AndroidModule))]
namespace SpectrumTV.Droid
{
    /// <summary>
    /// Here you can register specific service for this platform
    /// like firebase analytics o push notifications
    /// </summary>
    /// 
    public class AndroidModule : RuntimePlatformModule
    {
        // Autofac builder
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
