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
            Application.Current.Properties["login_key"] = "";
            Application.Current.Properties["password_key"] = "";
            App.Current.MainPage = new LoginPage();
        }
    }
}