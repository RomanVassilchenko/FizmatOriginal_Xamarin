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
    public partial class SchedulePage : ContentPage
    {
        private readonly int heightRowsList = 90;
        private ObservableCollection<Subject> trends = new ObservableCollection<Subject>();

        public string classnumChanged = "10", classletterChanged = "D", LanguageChanged = "RU", OldLanguage = "RU";
        public int numChanged = 1;

        private string Url = "https://script.google.com/macros/s/AKfycbxlGnl54weDQqW6Z6FnMLP18lVA8fCtJKKACdTegeRGR3MQOlc/exec";

        private readonly HttpClient _client = new HttpClient();

        public SchedulePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            pickerclassnum.SelectedIndex = 5;
            pickerclassletter.SelectedIndex = 2;
            pickerdayofweek.SelectedIndex = 0;



            myList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };

            OnGetList(LanguageChanged);
        }

        protected async void OnGetList(string Language)
        {
            if (Language.ToUpper() == "RU")
            {
                Url = "https://script.google.com/macros/s/AKfycbxlGnl54weDQqW6Z6FnMLP18lVA8fCtJKKACdTegeRGR3MQOlc/exec";
            }

            if (Language.ToUpper() == "KZ")
            {
                Url = "https://script.google.com/macros/s/AKfycbxbCvm8IEiUue9GGIRyn3zxqrTMM4uhznB9bxpe14m7_lpuu3XF/exec";
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    activity_indicator.IsRunning = true;
                    string content = await _client.GetStringAsync(Url);
                    List<Subject> tr = JsonConvert.DeserializeObject<List<Subject>>(content);
                    trends = new ObservableCollection<Subject>(tr);
                    ChoseClassAndDay(classnumChanged, classletterChanged, numChanged, LanguageChanged);
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
        private void ChoseClassAndDay(string classnum, string classletter, int num, string Language)
        {
            List<Subject> json = new List<Subject>(trends);
            List<Subject> weekclassjson = new List<Subject>();
            foreach (Subject s in json)
            {
                if (s.classnumber == classnum + classletter)
                {
                    if (s.weeknum == num.ToString())
                    {
                        if (s.subject != "")
                        {
                            weekclassjson.Add(new Subject
                            {
                                classnumber = s.classnumber,
                                number = s.number + ")",
                                classroom = s.classroom,
                                subject = s.subject,
                                weeknum = s.weeknum,
                                times = s.times
                            });
                        }
                    }
                }
            }
            myList.ItemsSource = weekclassjson;
        }


        private void Pickerclassnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            classnumChanged = (pickerclassnum.Items[pickerclassnum.SelectedIndex]).ToString();
            ShowSchedule();
        }

        private void Pickerclassletter_SelectedIndexChanged(object sender, EventArgs e)
        {
            classletterChanged = (pickerclassletter.Items[pickerclassletter.SelectedIndex]).ToString();
            LanguageChanged = LanguageCheck(classletterChanged);
            ShowSchedule();
        }

        private void Pickerdayofweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            numChanged = WeekCheck(pickerdayofweek.Items[pickerdayofweek.SelectedIndex]);
            ShowSchedule();
        }
        private int WeekCheck(string day)
        {
            if (day == "Понедельник")
            {
                return 1;
            }
            else if (day == "Вторник")
            {
                return 2;
            }
            else if (day == "Среда")
            {
                return 3;
            }
            else if (day == "Четверг")
            {
                return 4;
            }
            else if (day == "Пятница")
            {
                return 5;
            }
            else if (day == "Суббота")
            {
                return 6;
            }
            else
            {
                return 1;
            }
        }

        private string LanguageCheck(string letter)
        {
            if (letter == "A" || letter == "B")
            {
                return "KZ";
            }
            else
            {
                return "RU";
            }
        }
        public void ShowSchedule()
        {
            if (LanguageChanged != OldLanguage)
            {
                OldLanguage = LanguageChanged;
                OnGetList(LanguageChanged);
            }
            else
            {
                ChoseClassAndDay(classnumChanged, classletterChanged, numChanged, LanguageChanged);
            }
        }


    }
}