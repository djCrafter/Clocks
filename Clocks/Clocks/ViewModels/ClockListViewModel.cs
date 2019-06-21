using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Clocks.Views;
using System.Windows.Input;

namespace Clocks.ViewModels
{
    public class ClockListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ClockViewModel> Clocks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }

        public ICommand CreateClockCommand { protected set; get; }

        public ClockListViewModel()
        {
            Clocks = new ObservableCollection<ClockViewModel>();
            CreateClockCommand = new Command(CreateClock);
        }

        private void CreateClock()
        {
            Navigation.PushAsync(new ClockPage(new ClockViewModel() { ListViewModel = this }));
        }
    }
}
