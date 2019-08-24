using FizmatOriginal.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private string selectedurl = "";
        private readonly int heightRowsList = 90;
        private ObservableCollection<News> trends = new ObservableCollection<News>();

        private readonly string Url = "https://script.google.com/macros/s/AKfycbyvUKlW6NujurXJ6xtQP88fFSn0pczYjg0IBaTxFgcHirwNmIKa/exec";
        private readonly HttpClient _client = new HttpClient();

        public NewsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            myList.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                News data = (News)(e.Item);
                selectedurl = data.url;
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
                NavigationPage webPage = new NavigationPage(new WebPage(selectedurl))
                {
                    BarBackgroundColor = Color.FromHex("#002e56")
                };
                await Navigation.PushModalAsync(webPage);
            };
            OnGetList();
        }

        protected async void OnGetList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    activity_indicator.IsRunning = true;
                    activity_indicator.IsVisible = true;
                    string content = await _client.GetStringAsync(Url);
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
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
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


                    string[] dates = ndate.Split(new char[] { '.' });
                    if (dates[1] == "1" || dates[1] == "01") dates[1] = "Янв";
                    if (dates[1] == "2" || dates[1] == "02") dates[1] = "Фев";
                    if (dates[1] == "3" || dates[1] == "03") dates[1] = "Мар";
                    if (dates[1] == "4" || dates[1] == "04") dates[1] = "Апр";
                    if (dates[1] == "5" || dates[1] == "05") dates[1] = "Май";
                    if (dates[1] == "6" || dates[1] == "06") dates[1] = "Июн";
                    if (dates[1] == "7" || dates[1] == "07") dates[1] = "Июл";
                    if (dates[1] == "8" || dates[1] == "08") dates[1] = "Авг";
                    if (dates[1] == "9" || dates[1] == "09") dates[1] = "Сен";
                    if (dates[1] == "10") dates[1] = "Окт";
                    if (dates[1] == "11") dates[1] = "Ноя";
                    if (dates[1] == "12") dates[1] = "Дек";
                    ndate = dates[0] + " " + dates[1] + " " + dates[2];
                }
                if (ndate.Length == 9)
                {
                    //01 Апр 19
                    ndate = ndate[0].ToString() +
                            ndate[1].ToString() +
                            ndate[2].ToString() +
                            ndate[3].ToString() +
                            ndate[4].ToString() +
                            ndate[5].ToString() +
                            ndate[6].ToString() +
                            "20" +
                            ndate[7].ToString() +
                            ndate[8].ToString();
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
            /*
             foreach (News s in json)
            {
                string datadate;
                if ((s.date.ToString()).Length > 9)
                {
                    datadate = s.date.ToString().Substring(0, 9);
                    string[] dates = datadate.Split(new char[] { '/' });
                    if (dates[0] == "1") dates[0] = "Янв";
                    if (dates[0] == "2") dates[0] = "Фев";
                    if (dates[0] == "3") dates[0] = "Мар";
                    if (dates[0] == "4") dates[0] = "Апр";
                    if (dates[0] == "5") dates[0] = "Май";
                    if (dates[0] == "6") dates[0] = "Июн";
                    if (dates[0] == "7") dates[0] = "Июл";
                    if (dates[0] == "8") dates[0] = "Авг";
                    if (dates[0] == "9") dates[0] = "Сен";
                    if (dates[0] == "10") dates[0] = "Окт";
                    if (dates[0] == "11") dates[0] = "Ноя";
                    if (dates[0] == "12") dates[0] = "Дек";
                    datadate = dates[1] + " " + dates[0] + " " + dates[2];
                }
                else
                {
                    datadate = s.date.ToString();
                }
                string ntitle, ndesc;
                ntitle = s.title;
                ndesc = s.description;

                // REGEX 
                var pattern = @"&#...;";
                var rgx = new Regex(pattern);
                ntitle = rgx.Replace(ntitle, "");
                ndesc = rgx.Replace(ndesc, "");


                jsonfinal.Add(new News
                {
                    title = ntitle,
                    date = datadate,
                    description = ndesc,
                    image = s.image,
                    url = s.url
                });
            }
             */
            if (jsonfinal.Count == 0)
            {
                myList.BackgroundColor = Color.FromHex("#012647");

            }
            else
            {
                myList.BackgroundColor = Color.Black;
            }
            myList.ItemsSource = jsonfinal;
        }
    }
}