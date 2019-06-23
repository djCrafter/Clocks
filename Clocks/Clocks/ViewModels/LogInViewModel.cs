using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Clocks.Models;
using Clocks.Views;

namespace Clocks.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        private string login;
        private string password;
        private string message;
        public ICommand LogInCommand { protected set; get; }
        public INavigation Navigation {get; set;}
                         
        public LogInViewModel(INavigation navigation)
        {
           Navigation = navigation;
           LogInCommand = new Command(LogIn);
           AutoLogIn();
        }

        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }
        private void LogIn()
        {                             
            if (App.Database.LogIn(login, password))
            {
                Message = "";
                App.Current.Properties["Login"] = login;
                App.Current.Properties["Password"] = password;

                Navigation.PushAsync(new ClocksListPage(Navigation));
            }
            else
            {
                Message = "Wrong login or password!!!";
            }
        }
 
        private void AutoLogIn()
        {
            object login = "";
            object password = "";
            if (App.Current.Properties.TryGetValue("Login", out login) &&
                App.Current.Properties.TryGetValue("Password", out password))
            {
                if (App.Database.LogIn((string)login, (string)password))
                {
                    Navigation.PushAsync(new ClocksListPage(Navigation));
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
