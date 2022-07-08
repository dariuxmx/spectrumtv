using SpectrumTV.Services;

namespace SpectrumTV.ViewModels
{
    public class InitViewModel : BaseViewModel
    {
        public NavigationContext NavigationContext { get; }

        public InitViewModel(NavigationContext navigationContext)
        {
            NavigationContext = navigationContext;
        }
    }
}
