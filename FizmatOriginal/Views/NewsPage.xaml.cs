using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using FizmatOriginal.Models;
using FizmatOriginal.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private string selectedurl = "";
        private int heightRowsList = 90;
        private ObservableCollection<News> trends = new ObservableCollection<News>();

        private string Url = "https://script.google.com/macros/s/AKfycbyvUKlW6NujurXJ6xtQP88fFSn0pczYjg0IBaTxFgcHirwNmIKa/exec";
        private HttpClient _client = new HttpClient();


        public NewsPage()
        {
            InitializeComponent();

            myList.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                News data = (News)e.Item;
                selectedurl = data.url;
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
                WebPage webPage = new WebPage(selectedurl);
                await Navigation.PushAsync(webPage);
            };

            myList.RefreshCommand = new Command(() =>
            {
                OnGetList();
                myList.IsRefreshing = false;
            });

            //OnGetList();
        }

        private bool _appeared;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // To avoid repeating loading it. Remove if you want to refresh every time.
            if (!_appeared)
            {
                // Load from here
                OnGetList();

                _appeared = true;
            }
        }

        protected async void OnGetList()
        {

            GetUrl get = new GetUrl();
            Url = get.GetNewsUrl();

            string content = "";

            activity_indicator.IsRunning = true;
            activity_indicator.IsVisible = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    content = await _client.GetStringAsync(Url);
                    Application.Current.Properties["news_content_key"] = content;
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
            else
            {
                GetStringFromKey NewsgetTextFromKey = new GetStringFromKey("news_content_key");
                content = NewsgetTextFromKey.GetText();
            }
            List<News> tr = JsonConvert.DeserializeObject<List<News>>(content);
            trends = new ObservableCollection<News>(tr);
            int i = trends.Count;
            if (i > 0)
            {
                activity_indicator.IsRunning = false;
                activity_indicator.IsVisible = false;
            }
            i = (trends.Count * heightRowsList);
            activity_indicator.HeightRequest = i;
            ConvertAndShowNews();
        }
        private void ConvertAndShowNews()
        {
            List<News> json = new List<News>(trends);
            List<News> jsonfinal = new List<News>();
            foreach (News n in json)
            {
                string ntitle, ndesc;
                ntitle = n.title;
                ndesc = n.description;

                // REGEX 
                var pattern = @"&#...;";
                var rgx = new Regex(pattern);
                ntitle = rgx.Replace(ntitle, " \" ");
                ndesc = rgx.Replace(ndesc, " \" ");

                pattern = @"&#....;";
                rgx = new Regex(pattern);
                ntitle = rgx.Replace(ntitle, "-");
                ndesc = rgx.Replace(ndesc, "-");

                string ndate = n.date.ToString();
                if (ndate.Length > 9)
                {
                    ndate = ndate[0].ToString() +
                            ndate[1].ToString() +
                            ndate[2].ToString() +
                            ndate[3].ToString() +
                            ndate[4].ToString() +
                            ndate[5].ToString() +
                            ndate[6].ToString() +
                            ndate[7].ToString() +
                            ndate[8].ToString() +
                            ndate[9].ToString();


                    string[] dates = ndate.Split(new char[] { '/' });
                    try
                    {
                        if (dates[0] == "1" || dates[0] == "01") dates[0] = "Янв";
                        if (dates[0] == "2" || dates[0] == "02") dates[0] = "Фев";
                        if (dates[0] == "3" || dates[0] == "03") dates[0] = "Мар";
                        if (dates[0] == "4" || dates[0] == "04") dates[0] = "Апр";
                        if (dates[0] == "5" || dates[0] == "05") dates[0] = "Май";
                        if (dates[0] == "6" || dates[0] == "06") dates[0] = "Июн";
                        if (dates[0] == "7" || dates[0] == "07") dates[0] = "Июл";
                        if (dates[0] == "8" || dates[0] == "08") dates[0] = "Авг";
                        if (dates[0] == "9" || dates[0] == "09") dates[0] = "Сен";
                        if (dates[0] == "10") dates[0] = "Окт";
                        if (dates[0] == "11") dates[0] = "Ноя";
                        if (dates[0] == "12") dates[0] = "Дек";
                        ndate = dates[1] + " " + dates[0] + " " + dates[2].Substring(0, 5);
                    }
                    catch (Exception ey)
                    {
                        Crashes.TrackError(ey);
                    }
                }

                jsonfinal.Add(new News
                {
                    title = ntitle,
                    description = ndesc,
                    date = ndate,
                    image = n.image,
                    url = n.url
                });
            }
            switch (jsonfinal.Count)
            {
                case 0:
                    myList.BackgroundColor = Color.FromHex("#012647");
                    break;
                default:
                    myList.BackgroundColor = Color.Black;
                    break;
            }
            myList.ItemsSource = jsonfinal;
        }
    }
}
