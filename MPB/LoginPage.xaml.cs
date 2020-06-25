using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MPB
{
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;
        public LoginPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async private void Login(object sender, EventArgs e)
        {
            var emailValue = email.Text;
            var passwordValue = password.Text;

            //App.Current.MainPage = new NavigationPage(new MainPage());

            if (string.IsNullOrEmpty(emailValue) || string.IsNullOrEmpty(passwordValue))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
                return;
            }

            string Token = await auth.LoginWithEmailPassword(email.Text, password.Text);

            if (Token != null)
            {
                App.Current.MainPage = new MainPage();
                //await Navigation.PushAsync(new MainPage());
            }
            else if (Token == null)
            {
                ShowError();

            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }


    }
}
