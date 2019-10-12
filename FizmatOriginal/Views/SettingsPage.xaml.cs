using FizmatOriginal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            GetStringFromKey SwitchgetTextFromKey = new GetStringFromKey("switch_town_key");
            town_switch.IsToggled = SwitchgetTextFromKey.GetText().ToLower() == "true";
        }
        private void Btn_exit_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new LoginPage();
        }

        private async void Town_switch_Toggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["switch_town_key"] = e.Value.ToString();
            LoginPage login = new LoginPage();
            await Navigation.PushAsync(login);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
