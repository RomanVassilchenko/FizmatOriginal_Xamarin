using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public WebPage(string url)
        {
            InitializeComponent();
            WebViewPage.Source = url;
        }

        private void WebViewPage_Navigated(object sender, WebNavigatedEventArgs e)
        {
            activity_indicator.IsRunning = false;
            activity_indicator.IsVisible = false;
        }

        private void WebViewPage_Navigating(object sender, WebNavigatingEventArgs e)
        {
            activity_indicator.IsRunning = true;
            activity_indicator.IsVisible = true;
        }
    }
}