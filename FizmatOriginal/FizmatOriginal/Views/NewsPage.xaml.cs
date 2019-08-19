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
        private string selectedurl = "";
        private readonly int heightRowsList = 90;
        private ObservableCollection<News> trends = new ObservableCollection<News>();

        private readonly string Url = "https://script.google.com/macros/s/AKfycbyi9dWCzKzI1vR5u3f05KtN6rHTutTd1QoTE-4eSyLDT6XdCTQ/exec";
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
                WebPage webPage = new WebPage(selectedurl);
                await Navigation.PushAsync(webPage);
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
                    string content = await _client.GetStringAsync(Url);
                    List<News> tr = JsonConvert.DeserializeObject<List<News>>(content);
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