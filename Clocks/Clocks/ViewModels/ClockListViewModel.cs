using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Clocks.Views;
using System.Windows.Input;
using Clocks.Widgets;
using Clocks.Models;

namespace Clocks.ViewModels
{
    public class ClockListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ClockModel> Clocks { get; set; }
        Clock selectedClock;

        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }

        public ICommand CreateClockCommand { protected set; get; }
        public ICommand SaveClockCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
       

        public ClockListViewModel()
        {
            Clocks = new ObservableCollection<ClockModel>();
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
            ClockViewModel cvm = clockObject as ClockViewModel;
            if (cvm != null)
            {
                Clocks.Add(new ClockModel()
                {
                    HeadColor = cvm.ClockHeadColor,
                    FaceColor = cvm.ClockFaceColor,
                    ClockTimeZone = cvm.ClockTimeZoneInfo
                });
            }

            Back();
        } 
    }
}
