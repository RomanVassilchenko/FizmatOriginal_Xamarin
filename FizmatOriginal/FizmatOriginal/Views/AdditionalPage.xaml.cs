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
        }

        private void Btn_exit_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Remove("login_key");
            Application.Current.Properties.Remove("password_key");
            App.Current.MainPage = new LoginPage();
        }

        private void Town_switch_Toggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["town_switch_key"] = e.Value.ToString();
        }
    }
}