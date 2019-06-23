using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clocks.ViewModels;

namespace Clocks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClocksListPage : ContentPage
    {
        public ClocksListPage(INavigation navigation)
        {
            InitializeComponent();

            BindingContext = new ClockListViewModel() { Navigation = navigation };
        }

   
    }
}