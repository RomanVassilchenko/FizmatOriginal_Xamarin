using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Microsoft.AppCenter.Push;
namespace FizmatOriginal.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}