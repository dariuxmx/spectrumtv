using System;
using Autofac;
using System.Collections.Generic;
using SpectrumTV.Models.Exceptions;
using SpectrumTV.PageModels;
using Xamarin.Forms;

namespace SpectrumTV.Services
{
    public class PageFactory
    {
        private readonly IComponentContext _componentContext;
        private readonly Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>();

        public PageFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void Register<TPageModel, TPage>() where TPageModel : BasePageModel where TPage : Page
        {
            _typeMap[typeof(TPageModel)] = typeof(TPage);
        }

        public Page Resolve<TPageModel>(Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            return Resolve(out TPageModel pageModel, init);
        }

        public Page Resolve<TPageModel>(out TPageModel pageModel, Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            pageModel = _componentContext.Resolve<TPageModel>();

            init?.Invoke(pageModel);

            return Resolve(pageModel);
        }

        public Page Resolve<TPageModel>(TPageModel pageModel) where TPageModel : BasePageModel
        {
            if (_typeMap.TryGetValue(pageModel.GetType(), out Type pageType))
            {
                if (_componentContext.Resolve(pageType) is Page page)
                {
                    // The the page context on the page model's NavigationContext, once created
                    pageModel.NavigationContext.SetCurrentPage(page, pageModel);

                    page.BindingContext = pageModel;
                    page.Appearing += pageModel.OnAppearing;
                    page.Disappearing += pageModel.OnDisappearing;

                    return page;
                }
            }

            throw new TypeRegistrationException(typeof(TPageModel));
        }

        public NavigationPage ResolveNavigation<TPageModel>(Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            return WrapWithNavigationPage(Resolve(init));
        }

        public NavigationPage ResolveNavigation<TPageModel>(out TPageModel pageModel, Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            return WrapWithNavigationPage(Resolve(out pageModel, init));
        }

        public NavigationPage ResolveNavigation<TPageModel>(TPageModel pageModel) where TPageModel : BasePageModel
        {
            return WrapWithNavigationPage(Resolve(pageModel));
        }

        private NavigationPage WrapWithNavigationPage(Page page)
        {
            var navPage = new NavigationPage(page)
            {
                Title = page.Title,
                IconImageSource = page.IconImageSource,
                BarBackgroundColor = NavigationContext.GetBarBackgroundColor(page),
                BarTextColor = NavigationContext.GetBarTextColor(page),
                BindingContext = page.BindingContext
            };

            return navPage;
        }
    }
}
