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
        String OldLanguage = "RU";
        ObservableCollection<Subject> trends = new ObservableCollection<Subject>();

        public string classnumChanged = "10D", LanguageChanged = "RU";
        public int numChanged = 1;

        private string Url = "https://script.google.com/macros/s/AKfycby3InMdcI8rP1AC9JvGfZYfYlYEMIqmHV-ZGXTUDQV7PTz27_c/exec";

        private HttpClient _client = new HttpClient();

        public SchedulePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            OnGetList(LanguageChanged);
        }
        protected async void OnGetList(string Language)
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
                    trends = new ObservableCollection<Subject>(tr);
                    ChoseClassAndDay(classnumChanged, numChanged, LanguageChanged);
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
        private void ChoseClassAndDay(string classnum, int num, string Language)
        {
            List<Subject> json = new List<Subject>(trends);
            List<Subject> weekclassjson = new List<Subject>();
            foreach (Subject s in json)
            {
                if (s.classnumber == classnum)
                {
                    if (s.weeknum == num.ToString())
                    {
                        if (s.subject != "")
                        {
                            weekclassjson.Add(new Subject
                            {
                                classnumber = s.classnumber,
                                number = s.number,
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
            string classnumb = pickerclassnum.Items[pickerclassnum.SelectedIndex];
            if (classnumChanged.Length == 2) classnumChanged = classnumb + classnumChanged[1];
            else if (classnumChanged.Length == 3) classnumChanged = classnumb + classnumChanged[2];
        }

        private void Pickerclassletter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classletter = pickerclassletter.Items[pickerclassletter.SelectedIndex];
            if (classnumChanged.Length == 2) classnumChanged = classnumChanged[0] + classletter;
            if (classnumChanged.Length == 3) classnumChanged = classnumChanged[0] + classnumChanged[1] + classletter;
            LanguageChanged = LanguageCheck(classletter);
        }

        private void Pickerdayofweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            numChanged = WeekCheck(pickerdayofweek.Items[pickerdayofweek.SelectedIndex]);
        }

        private string LanguageCheck(string letter)
        {
            if (letter == "A" || letter == "B") return "KZ";
            else return "RU";
        }

        private void ButtonShowClass_Clicked(object sender, EventArgs e)
        {
            if (LanguageChanged != OldLanguage)
            {
                OldLanguage = LanguageChanged;
                OnGetList(LanguageChanged);
            }
            else ChoseClassAndDay(classnumChanged, numChanged, LanguageChanged);
        }

        private int WeekCheck(string day)
        {
            if (day == "Понедельник") return 1;
            else if (day == "Вторник") return 2;
            else if (day == "Среда") return 3;
            else if (day == "Четверг") return 4;
            else if (day == "Пятница") return 5;
            else if (day == "Суббота") return 6;
            else return 1;
        }
    }
}