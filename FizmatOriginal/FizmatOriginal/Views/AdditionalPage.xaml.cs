using FizmatOriginal.Models;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FizmatOriginal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalPage : ContentPage
    {
        public AdditionalPage()
        {
            InitializeComponent();
            GetTextFromKey SwitchgetTextFromKey = new GetTextFromKey("switch_town_key");
            town_switch.IsToggled = (SwitchgetTextFromKey.GetText().ToLower() == "true") ? true : false;
        }

        private void Btn_exit_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            App.Current.MainPage = new LoginPage();
        }

        private async void Town_switch_Toggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["switch_town_key"] = e.Value.ToString();
            await Navigation.PushAsync(new LoginPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}