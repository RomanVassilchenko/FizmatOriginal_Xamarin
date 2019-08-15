using FizmatOriginal.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        string selectedurl = "";
        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        int heightRowsList = 90;

        ObservableCollection<News> trends = new ObservableCollection<News>();

        private string Url = "https://script.google.com/macros/s/AKfycbyi9dWCzKzI1vR5u3f05KtN6rHTutTd1QoTE-4eSyLDT6XdCTQ/exec";
        private HttpClient _client = new HttpClient();

        public NewsPage()
        { 
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            myList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;
                ((ListView)sender).SelectedItem = null;
            };
            myList.ItemSelected += myList_ItemSelectedAsync;
            OnGetList();
        }

        private async void myList_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            WebPage webPage = new WebPage(selectedurl);
            await Navigation.PushAsync(webPage);
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
                    List<News> json = new List<News>(trends);
                    myList.ItemsSource = json;
                    int i = json.Count;
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