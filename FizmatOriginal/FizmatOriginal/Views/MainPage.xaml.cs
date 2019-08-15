using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
namespace FizmatOriginal.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            Microsoft.AppCenter.AppCenter.Start("257a45d8-3a75-4e82-9d9b-554fab395414",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}