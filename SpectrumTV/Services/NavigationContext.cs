using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpectrumTV.PageModels;
using SpectrumTV.Interfaces;
using SpectrumTV.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SpectrumTV.Services
{
    public class NavigationContext : BaseViewModel
    {
        #region Attached Properties
        public static readonly BindableProperty BarBackgroundColorProperty = BindableProperty.CreateAttached("BarBackgroundColor", typeof(Color), typeof(NavigationContext), Color.Red);

        public static Color GetBarBackgroundColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(BarBackgroundColorProperty);
        }

        public static void SetBarBackgroundColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(BarBackgroundColorProperty, value);
        }

        public static readonly BindableProperty BarTextColorProperty = BindableProperty.CreateAttached("BarTextColor", typeof(Color), typeof(NavigationContext), Color.White);

        public static Color GetBarTextColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(BarTextColorProperty);
        }

        public static void SetBarTextColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(BarTextColorProperty, value);
        }

        public static readonly BindableProperty AndroidBackButtonEnabledProperty = BindableProperty.CreateAttached("AndroidBackButtonEnabled", typeof(bool), typeof(NavigationContext), true);

        public static bool GetAndroidBackButtonEnabled(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AndroidBackButtonEnabledProperty);
        }

        public static void SetAndroidBackButtonEnabled(BindableObject bindable, bool value)
        {
            bindable.SetValue(AndroidBackButtonEnabledProperty, value);
        }
        #endregion

        private readonly PageFactory _pageFactory;
        private readonly ILogger _logger;

        private Page _currentPage;
        private BasePageModel _currentPageModel;
        private NavigationPage _navigationPage;

        private bool _isModal;
        public bool IsModal
        {
            get => _isModal;
            set => SetProperty(ref _isModal, value);
        }

        public NavigationContext(
            PageFactory pageFactory,
            ILogger logger)
        {
            _pageFactory = pageFactory;
            _logger = logger;
        }

        public void SetCurrentPage(Page page, BasePageModel pageModel)
        {
            _currentPage = page;
            _currentPageModel = pageModel;
        }

        public void SetNavigationBarProperties()
        {
            if (_currentPage != null)
            {
                SetNavigationBarProperties(_currentPage);
            }
        }

        public bool HasPreviousPage()
        {
            if (_currentPage?.Navigation?.NavigationStack != null)
            {
                return _currentPage.Navigation.NavigationStack.IndexOf(_currentPage) > 0;
            }

            return false;
        }

        #region Main Page
        public TPageModel SetMainPage<TPageModel>(Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            _currentPage = _pageFactory.Resolve(out TPageModel pageModel, init);
            _currentPageModel = pageModel;

            Page prevMainPage = Application.Current.MainPage;

            Application.Current.MainPage = _currentPage;

            // Dispose of the previous MainPage
            DisposePage(prevMainPage);

            return pageModel;
        }

        public TPageModel SetMainPageNavigation<TPageModel>(Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            _currentPage = _pageFactory.ResolveNavigation(out TPageModel pageModel, init);
            _currentPageModel = pageModel;

            Page prevMainPage = Application.Current.MainPage;

            Application.Current.MainPage = _currentPage;

            // Dispose of the previous MainPage
            DisposePage(prevMainPage);

            return pageModel;
        }

        public async Task<TPageModel> SetMainPageAsync<TPageModel>(Action<TPageModel> init = null) where TPageModel : BasePageModel
        {
            _currentPage = _pageFactory.Resolve(out TPageModel pageModel, init);
            _currentPageModel = pageModel;

            var tcs = new TaskCompletionSource<TPageModel>();

            Device.BeginInvokeOnMainThread(() =>
            {
                Page prevMainPage = Application.Current.MainPage;

                Application.Current.MainPage = _currentPage;

                // Dispose of the previous MainPage
                DisposePage(prevMainPage);

                tcs.TrySetResult(pageModel);
            });

            return await tcs.Task;
        }
        #endregion

        #region Page Navigation
        public async Task<TPageModel> PushAsync<TPageModel>(Action<TPageModel> init = null, bool animated = true) where TPageModel : BasePageModel
        {
            Page page = _pageFactory.Resolve(out TPageModel pageModel, init);

            SetNavigationContextProperties(pageModel);

            SetNavigationBarProperties(page);
            AttachPageDisposalHandler(page);

            await _currentPage?.Navigation?.PushAsync(page, animated);

            return pageModel;
        }

        public async Task<TPageModel> PushAsync<TPageModel>(TPageModel pageModel, bool animated = true) where TPageModel : BasePageModel
        {
            Page page = _pageFactory.Resolve(pageModel);

            SetNavigationContextProperties(pageModel);

            SetNavigationBarProperties(page);
            AttachPageDisposalHandler(page);

            await _currentPage?.Navigation?.PushAsync(page, animated);

            return pageModel;
        }

        public async Task PopAsync(bool animated = true)
        {
            if (_currentPage.Navigation != null)
            {
                IReadOnlyList<Page> navStack = _currentPage.Navigation.NavigationStack;

                if (navStack?.Count > 1)
                {
                    // Find the page just before the current page
                    Page prevPage = navStack.LastOrDefault(page => page != _currentPage);

                    if (prevPage != null)
                    {
                        SetNavigationBarProperties(prevPage);
                    }
                }

                await _currentPage.Navigation.PopAsync(animated);
            }
            else
            {
                _logger.Debug(this, $"Navigation not found for page '{_currentPage}'");
            }
        }

        public async Task<TPageModel> PushModalAsync<TPageModel>(Action<TPageModel> init = null, bool animated = true, bool backButtonEnabled = true) where TPageModel : BasePageModel
        {
            TPageModel pageModel = null;

            if (_currentPage.Navigation != null)
            {
                NavigationPage navPage = _pageFactory.ResolveNavigation(out pageModel, init);

                pageModel.NavigationContext.IsModal = true;

                AttachPageDisposalHandler(navPage);
                SetAndroidBackButtonEnabled(navPage.CurrentPage, backButtonEnabled);

                await _currentPage.Navigation.PushModalAsync(navPage, animated);
            }
            else
            {
                _logger.Debug(this, $"Navigation not found for page '{_currentPage}'");
            }

            return pageModel;
        }

        public async Task<TPageModel> PushModalAsync<TPageModel>(TPageModel pageModel, bool animated = true, bool backButtonEnabled = true) where TPageModel : BasePageModel
        {
            if (_currentPage?.Navigation != null)
            {
                NavigationPage navPage = _pageFactory.ResolveNavigation(pageModel);

                pageModel.NavigationContext.IsModal = true;

                AttachPageDisposalHandler(navPage);
                SetAndroidBackButtonEnabled(navPage.CurrentPage, backButtonEnabled);

                await _currentPage.Navigation.PushModalAsync(navPage, animated);
            }
            else
            {
                _logger.Debug(this, $"Navigation not found for page '{_currentPage}'");
            }

            return pageModel;
        }

        public async Task PopModalAsync(bool animated = true)
        {
            if (_currentPage.Navigation != null)
            {
                await _currentPage.Navigation.PopModalAsync(animated);
            }
            else
            {
                _logger.Debug(this, $"Navigation not found for page '{_currentPage}'");
            }
        }

        public async Task PopAllModalsAsync(bool animated = true)
        {
            if (_currentPage?.Navigation?.ModalStack?.Count > 0)
            {
                while (_currentPage.Navigation.ModalStack.Count > 0)
                {
                    await PopModalAsync(animated);
                }
            }
            else
            {
                _logger.Debug(this, $"ModalStack not found for page '{_currentPage}'");
            }
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            if (_currentPage?.Navigation != null)
            {
                IReadOnlyList<Page> navStack = _currentPage.Navigation.NavigationStack;

                if (navStack?.Count > 2)
                {
                    // Dispose of pages that are between root and current page
                    for (int i = 1; i < navStack.Count - 1; i++)
                    {
                        Page childPage = navStack[i];

                        DisposePage(childPage);
                    }
                }

                await _currentPage.Navigation.PopToRootAsync(animated);
            }
            else
            {
                _logger.Debug(this, $"Navigation not found for page '{_currentPage}'");
            }
        }
        #endregion

        #region Alert Navigation
        public async Task DisplayAlertAsync(string title, string message, string cancel)
        {
            if (_currentPage != null)
            {
                await _currentPage.DisplayAlert(title, message, cancel);
            }
            else
            {
                _logger.Debug(this, $"{nameof(_currentPage)} is null");
            }
        }

        public async Task DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            if (_currentPage != null)
            {
                await _currentPage.DisplayAlert(title, message, accept, cancel);
            }
            else
            {
                _logger.Debug(this, $"{nameof(_currentPage)} is null");
            }
        }

        public Task HideAlertAsync()
        {
            return PopModalAsync(false);
        }
        #endregion

        #region Private Helpers
        private NavigationPage FindNavigationPage(Page page)
        {
            if (page == null)
            {
                return null;
            }

            if (page is NavigationPage navPage)
            {
                return navPage;
            }

            return FindNavigationPage(page?.Parent as Page);
        }

        private void SetNavigationBarProperties(Page page)
        {
            if (_navigationPage == null)
            {
                _navigationPage = FindNavigationPage(_currentPage);
            }

            if (_navigationPage != null)
            {
                _navigationPage.BarBackgroundColor = GetBarBackgroundColor(page);
                _navigationPage.BarTextColor = GetBarTextColor(page);
            }
        }

        private void SetNavigationContextProperties(BasePageModel pageModel)
        {
            if (pageModel != null && _currentPageModel != null)
            {
                // Pages pushed within a modal pages are considered modal pages themselves
                pageModel.NavigationContext.IsModal = _currentPageModel.NavigationContext.IsModal;
            }
        }

        private void AttachPageDisposalHandler(Page page)
        {
            if (page != null)
            {
                page.Disappearing += OnPageDisappearing;
            }
        }

        // Listener to dispose of page models once the page has disappeared
        // Only the parent page's navigation service should receive this event
        private void OnPageDisappearing(object sender, EventArgs e)
        {
            TryDisposePage(sender as Page);
        }

        private void TryDisposePage(Page page)
        {
            if (_currentPage != null && page != null)
            {
                Page currentPage = _currentPage;

                Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(1000);

                        // Page cannot dispose of itself
                        bool canDispose = currentPage != page;

                        if (canDispose && currentPage.Navigation != null)
                        {
                            if (currentPage.Navigation.NavigationStack != null)
                            {
                                // Page cannot exist in the navigation stack of the current page
                                canDispose &= !currentPage.Navigation.NavigationStack.Contains(page);
                            }

                            if (currentPage.Navigation.ModalStack != null)
                            {
                                // Page cannot exist in the modal stack or within a navigation page in the modal stack
                                canDispose &= !currentPage.Navigation.ModalStack.Any(modalPage => modalPage == page || modalPage.Navigation?.NavigationStack == null || modalPage.Navigation.NavigationStack.Contains(page));
                            }
                        }

                        //_logger.Debug(this, $"Can dispose of page '{page}'? {canDispose}");

                        if (canDispose)
                        {
                            DisposePage(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Debug(this, $"Exception while disposing page '{page}'");
                        _logger.Error(this, ex);
                    }
                });
            }
        }

        private void DisposePage(Page page)
        {
            if (page != null)
            {
                //_logger.Debug(this, $"Disposing '{page}'");

                page.Disappearing -= OnPageDisappearing;

                if (page.BindingContext is BasePageModel pageModel)
                {
                    // Remove page lifecycle events from page model
                    page.Appearing -= pageModel.OnAppearing;
                    page.Disappearing -= pageModel.OnDisappearing;

                    pageModel.Dispose();
                }

                // Check if this was a modal window
                if (page is NavigationPage)
                {
                    IReadOnlyList<Page> navStack = page.Navigation?.NavigationStack;

                    if (navStack?.Count > 0)
                    {
                        // Dispose of all child pages
                        for (int i = 0; i < navStack.Count; i++)
                        {
                            Page childPage = navStack[i];

                            DisposePage(childPage);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
