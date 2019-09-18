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
            if (!Application.Current.Properties.ContainsKey("switch_town_key"))
            {
                town_switch.IsToggled = false;
            }
            else
            {
                try
                {
                    if (bool.Parse(Application.Current.Properties["switch_town_key"].ToString()))
                    {
                        town_switch.IsToggled = true;
                    }
                    else
                    {
                        town_switch.IsToggled = false;
                    }
                }
                catch
                {
                    town_switch.IsToggled = false;
                }
            }
        }

        private void Btn_exit_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            App.Current.MainPage = new LoginPage();
        }

        private void Town_switch_Toggled(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties["switch_town_key"] = e.Value.ToString();
            
        }
    }
}