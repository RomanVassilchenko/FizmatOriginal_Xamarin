using FizmatOriginal.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        private ObservableCollection<Access> trends = new ObservableCollection<Access>();


        private string Url = "https://script.google.com/macros/s/AKfycbwVtdW3WNTkWaYh7wjNZqRv2zK0Ix-vRgm4uwPAfpzEmwpElWcc/exec";

        private readonly HttpClient _client = new HttpClient();

        public LoginPage()
        {
            InitializeComponent();
            activity_indicator.IsRunning = true;
            passwordLoading();

            if (!Application.Current.Properties.ContainsKey("login_key"))
            {
                entry_login.Text = "";
            }
            else
            {
                try
                {
                    entry_login.Text = Application.Current.Properties["login_key"].ToString();
                }
                catch
                {
                    entry_login.Text = "";
                }
            }

            if (!Application.Current.Properties.ContainsKey("password_key"))
            {
                entry_login.Text = "";
            }
            else
            {
                try
                {
                    entry_password.Text = Application.Current.Properties["password_key"].ToString();
                }
                catch
                {
                    entry_password.Text = "";
                }
            }

            btn_login.Clicked += (sender, args) => passwordCheck();
        }

        private async void passwordLoading()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string content = await _client.GetStringAsync(Url);
                    List<Access> tr = JsonConvert.DeserializeObject<List<Access>>(content);
                    trends = new ObservableCollection<Access>(tr);
                    int i = trends.Count;
                    if (i > 0)
                    {
                        btn_login.IsEnabled = true;
                    }
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
        }
        public async void passwordCheck()
        {
            List<Access> json = new List<Access>(trends);
            List<Access> weekclassjson = new List<Access>();

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
                    entry_login.Text = "";
                    entry_password.Text = "";
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите email и пароль", "Ок");
                entry_login.Text = "";
                entry_password.Text = "";
            }
        }
    }
}