﻿using FizmatOriginal.Models;
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
                string stitle, sdesc;
                stitle = s.title;
                sdesc = s.description;

                // REGEX 
                var pattern = @"&#...;";
                var rgx = new Regex(pattern);
                stitle = rgx.Replace(stitle, "");
                sdesc = rgx.Replace(sdesc, "");


                jsonfinal.Add(new News
                {
                    title = stitle,
                    date = datadate,
                    description = sdesc,
                    image = s.image,
                    url = s.url
                });
            }
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