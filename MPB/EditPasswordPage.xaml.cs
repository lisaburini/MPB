using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{
    public partial class EditPasswordPage : ContentPage
    {
        IAuth auth;
        public EditPasswordPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private async void Edit(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(oldPassword.Text) || String.IsNullOrEmpty(oldPassword.Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");
            }
            else
            {
                string task = await auth.EditPassword(oldPassword.Text, newPassword.Text);
                if (Equals(task, "ok"))
                {
                    App.Current.MainPage = new MainPage();
                }
                else if (Equals(task, null))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");
                }
            }
        }


    }
}
