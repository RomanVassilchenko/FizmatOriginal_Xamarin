using FizmatOriginal.Models;
using FizmatOriginal.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        private ObservableCollection<Access> trends = new ObservableCollection<Access>();


        private string Url = "https://script.google.com/macros/s/AKfycbwVtdW3WNTkWaYh7wjNZqRv2zK0Ix-vRgm4uwPAfpzEmwpElWcc/exec";
        private string classUrl = "https://script.google.com/macros/s/AKfycbz7ofb88NYRa-hcsyJNkOof_r5vO3qpwBSPdgeLIqXtAAK41Dw/exec";
        private bool isLoaded;
        public LoginPage()
        {
            InitializeComponent();

            btn_login.TextColor = Color.Gray;
            activity_indicator.IsRunning = true;

            PasswordLoading();

            GetStringFromKey LogintextFromKey = new GetStringFromKey("login_key");
            entry_login.Text = LogintextFromKey.GetText();

            GetStringFromKey PasswordtextFromKey = new GetStringFromKey("password_key");
            entry_password.Text = PasswordtextFromKey.GetText();

            btn_login.Clicked += (sender, args) => PasswordCheck();
        }

        private async void PasswordLoading()
        {
            try
            {
                GetContent get = new GetContent(Url, "login_content_key");
                string content = await get.GetContentAsync();
                List<Access> tr = JsonConvert.DeserializeObject<List<Access>>(content);
                trends = new ObservableCollection<Access>(tr);
                int i = trends.Count;
                if (i > 0)
                {
                    isLoaded = true;
                    btn_login.TextColor = Color.White;
                }
            }
            catch (Exception ey)
            {
                Crashes.TrackError(ey);
            }
        }
        public async void PasswordCheck()
        {
            if (!isLoaded)
            {
                await DisplayAlert("Слабый интернет или нет сигнала", "Попробуйте еще раз", "Ок");
                PasswordLoading();
            }

            List<Access> json = new List<Access>(trends);

            if (entry_login.Text != "")
            {
                string logintext = entry_login.Text;
                logintext = logintext.Replace(" ", "");
                bool isActive = false;
                foreach (Access s in json)
                {
                    if (s.login == logintext && s.password == entry_password.Text)
                    {
                        Application.Current.Properties["login_key"] = s.login;
                        Application.Current.Properties["password_key"] = s.password;
                        var a = s.login;
                        if (a.Contains('_'))
                        {
                            string content = "";
                            GetContent get = new GetContent(classUrl, "class_list_content_key");
                            content = await get.GetContentAsync();

                            string[] words = a.Split(new char[] { '_' });
                            string classall = words[0];

                            content = content.ToLower();
                            words = content.Split(new char[] { ' ' });

                            List<string> list = new List<string>(words);
                            int indexClass = Array.IndexOf(words, classall);
                            Application.Current.Properties["selected_class_key"] = (indexClass != -1) ? indexClass.ToString() : 0.ToString();

                        }
                        isActive = true;
                        break;
                    }
                }

                if (isActive)
                {
                    MainPage main = new MainPage();
                    await Navigation.PushModalAsync(main);

                }
                else
                {
                    await DisplayAlert("Не правильный логин и пароль", "Попробуйте еще раз", "Ок");
                    entry_password.Text = "";
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите логин и пароль", "Ок");
                entry_password.Text = "";
            }
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}