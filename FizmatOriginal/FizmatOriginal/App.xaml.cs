﻿using Com.OneSignal;
using FizmatOriginal.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace FizmatOriginal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (!Application.Current.Properties.ContainsKey("login_key"))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new MainPage();
            }
            OneSignal.Current.StartInit("22d3ebfe-fc04-4004-bed3-0a42c316c55c")
                  .EndInit();
        }

        protected override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start("257a45d8-3a75-4e82-9d9b-554fab395414",
                   typeof(Analytics), typeof(Crashes));

            OneSignal.Current.RegisterForPushNotifications();
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
