using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clocks.ViewModels;

namespace Clocks.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClocksListPage : ContentPage
    {
        public ClocksListPage()
        {
            InitializeComponent();

            BindingContext = new ClockListViewModel() { Navigation = this.Navigation };
        }
    }
}