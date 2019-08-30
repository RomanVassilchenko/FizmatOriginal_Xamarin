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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Btn_register_Clicked(object sender, EventArgs e)
        {
            if(entry_password.Text == entry_repeat_password.Text)
            {
                if (entry_email.Text.Contains("@astana.fizmat.kz"))
                {

                }
                else
                {
                    DisplayAlert("Ошибка", "Используйте школьную почту", "Ок");
                    entry_email.Text = "";
                    entry_password.Text = "";
                    entry_repeat_password.Text = "";
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Пароли не совпадают. Попробуйте еще раз", "Ок");
                entry_password.Text = "";
                entry_repeat_password.Text = "";
            }
        }

        private async void Btn_login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}