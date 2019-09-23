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

        public string classChanged = "10D";
        public int numChanged = 0;

        private string Url = "https://script.google.com/macros/s/AKfycbw_Cf4YMALGRKny552qe9u9f86Ui7Iq7vbGDBVGoFKh2IhYH0w/exec";

        private string classUrl = "https://script.google.com/macros/s/AKfycbz7ofb88NYRa-hcsyJNkOof_r5vO3qpwBSPdgeLIqXtAAK41Dw/exec";

        private readonly HttpClient _client = new HttpClient();

        private List<string> list;

        public SchedulePage()
        {
            InitializeComponent();

            GetTownUrl get = new GetTownUrl();
            Url = get.GetScheduleUrl();
            classUrl = get.GetClassUrl();

            classGetList();

            GetTextFromKey SchedulegetTextFromKey = new GetTextFromKey("day_key");
            pickerdayofweek.SelectedIndex = (SchedulegetTextFromKey.GetText() == "") ? 0 : int.Parse(SchedulegetTextFromKey.GetText());
            numChanged = WeekCheck(pickerdayofweek.Items[pickerdayofweek.SelectedIndex]);



            myList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };

            OnGetList();
        }

        private async void classGetList()
        {
            string content = "";
            string[] words;

            GetContent get = new GetContent(classUrl, "class_content_key");
            content = await get.getContentAsync();

            words = content.Split(new char[] { ' ' });
            list = new List<string>(words);
            pickerclass.ItemsSource = list;

            if (list.Count > 0)
            {
                GetTextFromKey SchedulegetTextFromKey = new GetTextFromKey("selected_class_key");
                pickerclass.SelectedIndex = (SchedulegetTextFromKey.GetText() == "") ? 0 : int.Parse(SchedulegetTextFromKey.GetText());
                classChanged = pickerclass.Items[pickerclass.SelectedIndex];

            }
        }

        protected async void OnGetList()
        {
            lbl_bug.IsVisible = false;
            string content = "";
            activity_indicator.IsRunning = true;
            activity_indicator.IsVisible = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                lbl_bug.Text = "Oops! Произошла ошибка! Класса не существует или сегодня нет уроков. Попробуйте еще раз.";
                try
                {
                    content = await _client.GetStringAsync(Url);
                    Application.Current.Properties["schedule_content_key"] = content;
                }
                catch (Exception ey)
                {
                    Crashes.TrackError(ey);
                }
            }
            else
            {

                lbl_bug.Text = "Oops. Нет подключения к интерету. Попробуйте еще раз";
                if (Application.Current.Properties.ContainsKey("schedule_content_key"))
                {
                    try
                    {
                        content = Application.Current.Properties["schedule_content_key"].ToString();
                    }
                    catch
                    {
                    }
                }
            }
            List<Subject> tr = JsonConvert.DeserializeObject<List<Subject>>(content);
            trends = new ObservableCollection<Subject>(tr);
            int i = trends.Count;
            if (i > 0)
            {
                activity_indicator.IsRunning = false;
                activity_indicator.IsVisible = false;
            }
            i = (trends.Count * heightRowsList);
            activity_indicator.HeightRequest = i;
            ChoseClassAndDay();
        }
        private void ChoseClassAndDay()
        {
            List<Subject> json = new List<Subject>(trends);
            List<Subject> weekclassjson = new List<Subject>();
            foreach (Subject s in json)
            {
                if (s.classnumber == classChanged)
                {
                    if (s.weeknum == numChanged.ToString())
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
            if (weekclassjson.Count == 0)
            {
                myList.BackgroundColor = Color.FromHex("#012647");
                if (!activity_indicator.IsVisible)
                {
                    //TODO fix bug
                    lbl_bug.IsVisible = false;
                }
                else
                {
                    lbl_bug.IsVisible = false;
                }
            }
            else
            {
                lbl_bug.IsVisible = false;
                myList.BackgroundColor = Color.Black;
            }

            myList.ItemsSource = weekclassjson;
            if (weekclassjson.Count == 0)
            {
                //TODO Fix Bug
                lbl_bug.IsVisible = false;
            }
        }

        private void Pickerdayofweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["day_key"] = pickerdayofweek.SelectedIndex;
            numChanged = WeekCheck(pickerdayofweek.Items[pickerdayofweek.SelectedIndex]);
            ChoseClassAndDay();
        }

        private void Pickerclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["selected_class_key"] = pickerclass.SelectedIndex;
            string getClass = pickerclass.Items[pickerclass.SelectedIndex].ToString();
            classChanged = getClass;
            ChoseClassAndDay();
        }

        private int WeekCheck(string day)
        {
            switch (day)
            {
                case "Понедельник":
                    return 1;
                case "Вторник":
                    return 2;
                case "Среда":
                    return 3;
                case "Четверг":
                    return 4;
                case "Пятница":
                    return 5;
                case "Суббота":
                    return 6;
                default:
                    return 1;
            }
        }


    }
}