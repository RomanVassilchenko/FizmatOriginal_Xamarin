
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
            webViewHelp.Source = "https://docs.google.com/forms/d/e/1FAIpQLScbYMtTZd4DJ-M7vd_zFOdw794t6CStYKVK21m0JbdMP3fmMg/viewform";
        }
    }
}