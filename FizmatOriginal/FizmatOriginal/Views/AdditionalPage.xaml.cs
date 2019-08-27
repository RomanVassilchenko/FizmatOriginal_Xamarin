
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
            menuList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null)
                {
                    return;
                } ((ListView)sender).SelectedItem = null;
            };
        }
    }
}