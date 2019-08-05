using FizmatOriginal.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        int heightRowsList = 90;
        private string Url = "https://script.google.com/macros/s/AKfycby3InMdcI8rP1AC9JvGfZYfYlYEMIqmHV-ZGXTUDQV7PTz27_c/exec";

        private HttpClient _client = new HttpClient();

        public SchedulePage()
        {
            InitializeComponent();
            OnGetList("5A", 1, "KZ");
        }
        protected async void OnGetList(string classnum, int num, string Language)
        {
            if (Language.ToUpper() == "RU") Url = "https://script.google.com/macros/s/AKfycby3InMdcI8rP1AC9JvGfZYfYlYEMIqmHV-ZGXTUDQV7PTz27_c/exec";
            if (Language.ToUpper() == "KZ") Url = "https://script.google.com/macros/s/AKfycbxYd2luv9dXqr8DUAMiuAHvfuJ_JBJ3V9Df4aBWMWrfcXQORW8/exec";
            if (CrossConnectivity.Current.IsConnected)
            {

                try
                {

                    activity_indicator.IsRunning = true;

                    var content = await _client.GetStringAsync(Url);

                    var tr = JsonConvert.DeserializeObject<List<Subject>>(content);

                    ObservableCollection<Subject> trends = new ObservableCollection<Subject>(tr);

                    List<Subject> json = new List<Subject>(trends);
                    List<Subject> weekclassjson = new List<Subject>();
                    foreach (Subject s in json)
                    {
                        if (s.classnumber == classnum)
                        {
                            if (s.weeknum == num.ToString())
                            {
                                weekclassjson.Add(new Subject
                                {
                                    classnumber = s.classnumber,
                                    number = s.number,
                                    classroom = s.classroom,
                                    subject = s.subject,
                                    weeknum = s.weeknum
                                });
                            }
                        }
                    }

                    myList.ItemsSource = weekclassjson;

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
                    Debug.WriteLine("" + ey);
                }

            }

        }
    }
}