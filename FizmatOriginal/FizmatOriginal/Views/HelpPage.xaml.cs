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
                //TODO Change source to help web
                webViewHelp.Source = "https://docs.google.com/forms/d/e/1FAIpQLScbYMtTZd4DJ-M7vd_zFOdw794t6CStYKVK21m0JbdMP3fmMg/viewform";
            }
            catch
            { 
            }
            activity_indicator_Web.IsRunning = false;
        }
    }
}