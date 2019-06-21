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
    public partial class ClockPage : ContentPage
    {
        public ClockViewModel viewModel { get; private set; }
        public ClockPage(ClockViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = viewModel;
        }
    }
}