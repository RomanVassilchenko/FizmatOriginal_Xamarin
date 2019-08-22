
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
            isEnablePush();
        }
        private async void isEnablePush()
        {
            await Push.SetEnabledAsync(true);
        }
        private void SendEmailButton_Clicked(object sender, System.EventArgs e)
        {
            string toEmail = "fizmat.original@gmail.com";
            string emailSubject = "Сообщение об ошибке";
            string emailBody = "";

            if (string.IsNullOrEmpty(toEmail))
            {
                return;
            }

            Device.OpenUri(new System.Uri(System.String.Format("mailto:{0}?subject={1}&body={2}", toEmail, emailSubject, emailBody)));

        }

        private void CallButton_Clicked(object sender, System.EventArgs e)
        {
            string phoneNumber = "87759004626";

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return;
            }

            // Following line used to display given phone number in dialer  
            Device.OpenUri(new System.Uri(System.String.Format("tel:{0}", phoneNumber)));
        }
    }
}