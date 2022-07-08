using System;
using SpectrumTV.Services;
using SpectrumTV.ViewModels;

namespace SpectrumTV.PageModels
{
    public abstract class BasePageModel : BaseViewModel
    {
        public NavigationContext NavigationContext { get; private set; }

        private bool _hasAppeared;
        public bool HasAppeared
        {
            get => _hasAppeared;
            private set => SetProperty(ref _hasAppeared, value);
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            private set => SetProperty(ref _isVisible, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        protected BasePageModel(NavigationContext navigationContext)
        {
            NavigationContext = navigationContext;
        }

        public virtual void OnAppearing(object sender, EventArgs e)
        {
            IsVisible = true;

            NavigationContext.SetNavigationBarProperties();

            if (!_hasAppeared)
            {
                HasAppeared = true;

                OnFirstAppearing(sender, e);
            }
        }

        protected virtual void OnFirstAppearing(object sender, EventArgs e) { }

        public virtual void OnDisappearing(object sender, EventArgs e)
        {
            IsVisible = false;
        }
    }
}
