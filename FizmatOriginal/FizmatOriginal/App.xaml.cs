using FizmatOriginal.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;

namespace FizmatOriginal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start("257a45d8-3a75-4e82-9d9b-554fab395414",
                   typeof(Analytics), typeof(Crashes));
            Microsoft.AppCenter.AppCenter.Start("257a45d8-3a75-4e82-9d9b-554fab395414", typeof(Push));
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
