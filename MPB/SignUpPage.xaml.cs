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
            string Token = await auth.SignUp(email.Text, password.Text, name.Text, lastName.Text);

            if (Token != null)
            {
                App.Current.MainPage = new MainPage();
                //await Navigation.PushAsync(new MainPage());
            }
            else if (Token == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");

            }

            /*bool valueSignup = auth.SignUp(email.Text, password.Text, name.Text, lastName.Text);
            if (valueSignup) {
                App.Current.MainPage = new NavigationPage(new MainPage());

            } else  {
                App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");
            }
            */
        }
    }
}
