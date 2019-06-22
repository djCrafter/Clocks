using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clocks.Views;
using Clocks.Services;

namespace Clocks
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "clocks.db";
        public static ClockRepository database;

        public static ClockRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new ClockRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogInPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
