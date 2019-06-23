using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clocks.ViewModels;

namespace Clocks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();

            BindingContext = new LogInViewModel(Navigation);
        }      
    }
}