using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MPB
{
    public partial class SignUpPage : ContentPage
    {
        IAuth auth;
        public SignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async private void Signup(object sender, EventArgs e)
        {
            var nameValue = name.Text;
            var lastNameValue = lastName.Text;
            var emailValue = email.Text;
            var passwordValue = password.Text;
            if (string.IsNullOrEmpty(nameValue) || string.IsNullOrEmpty(lastNameValue) || string.IsNullOrEmpty(emailValue) || string.IsNullOrEmpty(passwordValue))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please fill all fields", "OK");
                return;
            }

            if (passwordValue.Length < 6  )
            {
                await App.Current.MainPage.DisplayAlert("Password too short", "Password must be at least 6 characters", "OK");
                return;
            }


            string Token = await auth.SignUp(email.Text, password.Text, name.Text, lastName.Text);

            if (Token != null)
            {
                App.Current.MainPage = new MainPage();;
            }
            else if (Token == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");

            }
        }
    }
}
