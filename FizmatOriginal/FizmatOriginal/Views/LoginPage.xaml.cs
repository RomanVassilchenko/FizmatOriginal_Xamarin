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
                        var a = s.login;
                        if (a.Contains('_'))
                        {
                            string[] words = a.Split(new char[] { '_' });
                            string classall = words[0];
                            string classnum = "";
                            string classletter = "";
                            if (classall.Length == 3)
                            {
                                classnum = classall[0].ToString() + classall[1].ToString();
                                classletter = classall[2].ToString().ToUpper();
                            }
                            if (classall.Length == 2)
                            {
                                classnum = classall[0].ToString();
                                classletter = classall[1].ToString().ToUpper();
                            }
                            switch (classletter)
                            {
                                case "A":
                                    Application.Current.Properties["letter_key"] = 0;
                                    break;
                                case "B":
                                    Application.Current.Properties["letter_key"] = 1;
                                    break;
                                case "D":
                                    Application.Current.Properties["letter_key"] = 2;
                                    break;
                                case "E":
                                    Application.Current.Properties["letter_key"] = 3;
                                    break;
                                case "G":
                                    Application.Current.Properties["letter_key"] = 4;
                                    break;
                                case "F":
                                    Application.Current.Properties["letter_key"] = 5;
                                    break;
                                case "X":
                                    Application.Current.Properties["letter_key"] = 6;
                                    break;
                                case "Z":
                                    Application.Current.Properties["letter_key"] = 7;
                                    break;
                                default:
                                    Application.Current.Properties["letter_key"] = 0;
                                    break;
                            }


                            switch (classnum)
                            {
                                case "1":
                                    Application.Current.Properties["class_key"] = 0;
                                    break;
                                case "2":
                                    Application.Current.Properties["class_key"] = 1;
                                    break;
                                case "3":
                                    Application.Current.Properties["class_key"] = 2;
                                    break;
                                case "4":
                                    Application.Current.Properties["class_key"] = 3;
                                    break;
                                case "5":
                                    Application.Current.Properties["class_key"] = 4;
                                    break;
                                case "6":
                                    Application.Current.Properties["class_key"] = 5;
                                    break;
                                case "7":
                                    Application.Current.Properties["class_key"] = 6;
                                    break;
                                case "8":
                                    Application.Current.Properties["class_key"] = 7;
                                    break;
                                case "9":
                                    Application.Current.Properties["class_key"] = 8;
                                    break;
                                case "10":
                                    Application.Current.Properties["class_key"] = 9;
                                    break;
                                case "11":
                                    Application.Current.Properties["class_key"] = 10;
                                    break;
                                default:
                                    Application.Current.Properties["class_key"] = 0;
                                    break;
                            }


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
    }
}