﻿using System;
using Autofac;
using SpectrumTV.Modules;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpectrumTV.iOS.iOSModule))]
namespace SpectrumTV.iOS
{
    /// <summary>
    /// Here you can register specific service for this platform
    /// like firebase analytics o push notifications
    /// </summary>

    public class iOSModule : RuntimePlatformModule
    {
        // Autofac builder
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
