
using SpectrumTV.PageModels;
using Xamarin.Forms;

namespace SpectrumTV.Pages
{
    public partial class SearchResultsPage : ContentPage
    {
        public SearchResultsPage()
        {
            InitializeComponent();
        }

        // This method executes when the user press Done button in the keyboard
        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            if (BindingContext is SearchResultsPageModel pageModel)
            {
                pageModel.SearchCommand.Execute(_searchEntry.Text);
            }
        }
    }
}
