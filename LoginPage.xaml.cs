using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapKing1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {


        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isUserNameEmpty = string.IsNullOrEmpty(userNameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            if (isUserNameEmpty || isPasswordEmpty || userNameEntry.Text != "test" || passwordEntry.Text != "test")
            {
                DisplayAlert("Error", "Invalid credentials. Please double-check your entry and try again.", "OK");
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }

   
}