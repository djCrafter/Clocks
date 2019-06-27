using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Clocks.Views;
using System.Windows.Input;
using Clocks.Models;

namespace Clocks.ViewModels
{
    public class ClockListViewModel : INotifyPropertyChanged
    {
        private int currentUserId = 0;

        public ObservableCollection<ClockModel> Clocks { get; set; }
        ClockModel selectedClock;
           
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

            GetCurrentUserId();
            GetClocksFromDB();
        }
      
        public ClockModel SelectedClock
        {
            get { return selectedClock; }
            set
            {
                if (selectedClock != value)
                {
                    ClockModel tempClock = value;                 
                    selectedClock = null;
                    OnPropertyChanged(nameof(SelectedClock));
                    Navigation.PushAsync(new ClockPage(new ClockViewModel(tempClock) { ListViewModel = this }, false));
                }
            }
        }
        
        private void CreateClock()
        {
            Navigation.PushAsync(new ClockPage(new ClockViewModel(null) { ListViewModel = this }, true));
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
                if(cvm.editingMode)
                {
                    int index = Clocks.IndexOf(cvm.selectedClock);
                    Clocks.RemoveAt(index);
                    Clocks.Insert(index, new ClockModel()
                    {
                        HeadColor = cvm.ClockHeadColor,
                        FaceColor = cvm.ClockFaceColor,
                        ClockTimeZone = cvm.ClockTimeZoneInfo
                    });
                    UpdateDB();
                }
                else
                {
                    var newClock = new ClockModel()
                    {
                        HeadColor = cvm.ClockHeadColor,
                        FaceColor = cvm.ClockFaceColor,
                        ClockTimeZone = cvm.ClockTimeZoneInfo
                    };

                    Clocks.Add(newClock);
                    App.Database.AddClock(newClock, currentUserId);                    
                }               
            }
            Back();
        }

        private void UpdateDB()
        {
            List<ClockModel> clockModels = new List<ClockModel>();
            clockModels.AddRange(Clocks);
            App.Database.UpdateClockListAsync(clockModels, currentUserId);
        }

        private void GetCurrentUserId()
        {
            object login = "";
            App.Current.Properties.TryGetValue("Login", out login);
            currentUserId = App.Database.GetUserId((string)login);
        }

        private void GetClocksFromDB()
        {
            var list = App.Database.GetClocks(currentUserId);
            foreach (var item in list)
            {
                Clocks.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
