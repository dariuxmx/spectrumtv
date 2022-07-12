using System;
using Autofac;
using SpectrumTV.Bootstrap;
using SpectrumTV.PageModels;
using SpectrumTV.Services;
using Xamarin.Forms;

namespace SpectrumTV
{
    public partial class App : Application
    {
        private static IContainer _container { get; set; }

        public App()
        {
            InitializeComponent();

            // Bootstrap container setup
            _container = Bootstrapper.Run();

            // Setup the main page with navigation context container
            var navigationService = _container.Resolve<NavigationContext>();
            navigationService.SetMainPageNavigation<HomePageModel>();
            navigationService.SetNavigationBarProperties();

            // Set home page
            // MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
