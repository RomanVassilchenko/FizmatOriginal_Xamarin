using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace FizmatOriginal.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BarBackgroundColor = Color.FromHex("#252525");
            BarTextColor = Color.White;

            NavigationPage newsPage = new NavigationPage(new NewsPage())
            {
                IconImageSource = "tab_news.png",
                Title = "Новости"
            };

            NavigationPage schedulePage = new NavigationPage(new SchedulePage())
            {
                IconImageSource = "tab_schedule.png",
                Title = "Расписание"
            };

            NavigationPage settingsPage = new NavigationPage(new SettingsPage())
            {
                IconImageSource = "tab_settings.png",
                Title = "Настройки"
            };

            Children.Add(newsPage);
            Children.Add(schedulePage);
            Children.Add(settingsPage);

            tbdPage.CurrentPage = tbdPage.Children[1];
        }
    }
}