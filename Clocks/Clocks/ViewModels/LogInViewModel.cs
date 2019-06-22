using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Clocks.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }        
    }
}
