﻿using FizmatOriginal.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        int heightRowsList = 90;
        ObservableCollection<News> trends = new ObservableCollection<News>();
        private string Url = "https://script.google.com/macros/s/AKfycbyi9dWCzKzI1vR5u3f05KtN6rHTutTd1QoTE-4eSyLDT6XdCTQ/exec";
        private HttpClient _client = new HttpClient();
        WebView webView;

        public NewsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            OnGetList();
            myList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;
                ((ListView)sender).SelectedItem = null; // de-select the row
            };
            myList.ItemSelected += myList_ItemSelectedAsync;
        }

        private async void myList_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedCategory = e.SelectedItem as News;
            string selectedItem = "";
            if (selectedCategory != null)
                selectedItem = selectedCategory.url;
            await Navigation.PushAsync(new WebPage(selectedItem));
        }

        protected async void OnGetList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    activity_indicator.IsRunning = true;
                    var content = await _client.GetStringAsync(Url);
                    var tr = JsonConvert.DeserializeObject<List<News>>(content);
                    trends = new ObservableCollection<News>(tr);
                    myList.ItemsSource = trends;
                    int i = trends.Count;
                    if (i > 0)
                    {
                        activity_indicator.IsRunning = false;
                    }
                    i = (trends.Count * heightRowsList);
                    activity_indicator.HeightRequest = i;
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
        }
    }
}