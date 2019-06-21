using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Clocks.Views;
using System.Windows.Input;
using Clocks.Widgets;

namespace Clocks.ViewModels
{
    public class ClockListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Clock> Clocks { get; set; }
        Clock selectedClock;

        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }

        public ICommand CreateClockCommand { protected set; get; }
        public ICommand SaveClockCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
       

        public ClockListViewModel()
        {
            Clocks = new ObservableCollection<Clock>();
            CreateClockCommand = new Command(CreateClock);
            SaveClockCommand = new Command(SaveClock);
            BackCommand = new Command(Back);
        }

        private void CreateClock()
        {
            Navigation.PushAsync(new ClockPage(new ClockViewModel() { ListViewModel = this }));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void SaveClock(object clockObject)
        {
            //ClockViewModel cvm = clockObject as ClockViewModel;
            //if(cvm != null)
            //{
            //    Clocks.Add(new Clock(cvm.clockHeadColor, cvm.clockFaceColor, cvm.ClockTimeZoneInfo));
            //}

            Back();
        } 
    }
}
