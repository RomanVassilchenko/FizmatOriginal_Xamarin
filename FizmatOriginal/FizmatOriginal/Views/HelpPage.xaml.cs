using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                activity_indicator_Web.IsRunning = true;
                webViewHelp.Source = "https://astana.fizmat.kz/";
            }
            catch
            { 
            }
            activity_indicator_Web.IsRunning = false;
        }
    }
}