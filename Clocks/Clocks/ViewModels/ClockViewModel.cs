using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Clocks.Models;


namespace Clocks.ViewModels
{

    public class ClockViewModel : INotifyPropertyChanged
    {      
        private ClockListViewModel cvm;
        private Color clockHeadColor = Color.WhiteSmoke;
        private Color clockFaceColor = Color.Black;
        private TimeZoneInfo clockTimeZoneInfo = TimeZoneInfo.Local;
        private string clockHeadSelected;
        private string clockFaceSelected;
        
        public bool editingMode = false;
        public ClockModel selectedClock;
        public List<string> Colors { get; } = colors.Keys.ToList();
        public ReadOnlyCollection<TimeZoneInfo> timeZonesList { get; } = TimeZoneInfo.GetSystemTimeZones();
        static Dictionary<string, Color> colors { get; } = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, {"Black", Color.Black },
            { "Blue", Color.Blue }, {"Fuchishia", Color.Fuchsia },
            { "Gray", Color.Gray }, {"Green", Color.Green },
            { "Lime", Color.Lime }, {"Maroon", Color.Maroon },
            { "Navy", Color.Navy }, {"Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, {"Teal", Color.Teal },
            { "White", Color.WhiteSmoke}, {"Yellow", Color.Yellow }
        };

        public ClockViewModel(ClockModel cm)
        {
            if(cm != null)
            {
                editingMode = true;
                selectedClock = cm;
               
                ClockHeadColor = selectedClock.HeadColor;
                clockFaceColor = selectedClock.FaceColor;
                clockTimeZoneInfo = selectedClock.ClockTimeZone;               
            }          
        }
     
        public string ClockHeadSelected
        {
            get { return clockHeadSelected; }
            set
            {
                clockHeadSelected = value;
                ClockHeadColor = colors[value];
            }
        }

        public string ClockFaceSelected
        {
            get { return clockFaceSelected; }
            set
            {
                clockFaceSelected = value;
                ClockFaceColor = colors[value];
            }
        }

        public TimeZoneInfo TimeZoneSelected
        {
            get { return clockTimeZoneInfo; }
            set
            {
                ClockTimeZoneInfo = value;
            }
        }

        public ClockListViewModel ListViewModel
        {
            get { return cvm; }
            set
            {
                if (cvm != value)
                {
                    cvm = value;
                    OnPropertyChanged(nameof(ListViewModel));
                }
            }
        }

        public Color ClockHeadColor
        {
            get { return clockHeadColor; }
            set
            {
                if (clockHeadColor != value)
                {
                    clockHeadColor = value;
                    OnPropertyChanged(nameof(ClockHeadColor));
                }
            }
        }

        public Color ClockFaceColor
        {
            get { return clockFaceColor; }
            set
            {
                if (clockFaceColor != value)
                {
                    clockFaceColor = value;
                    OnPropertyChanged(nameof(ClockFaceColor));
                }
            }
        }

        public TimeZoneInfo ClockTimeZoneInfo
        {
            get { return clockTimeZoneInfo; }
            set
            {
                if (clockTimeZoneInfo != value)
                {
                    clockTimeZoneInfo = value;
                    OnPropertyChanged(nameof(ClockTimeZoneInfo));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
