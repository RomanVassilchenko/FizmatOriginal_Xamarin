
using Microsoft.AppCenter.Push;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            notificationAsync();
        }
        async void notificationAsync()
        {
            await Push.SetEnabledAsync(true);
        }

        private async void WebViewShowButton_Clicked(object sender, System.EventArgs e)
        {
            WebPage webPage = new WebPage("https://astana.fizmat.kz/kontakty/");
            await Navigation.PushAsync(webPage);
        }
    }
}