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
            if (String.IsNullOrEmpty(oldPassword.Text) || String.IsNullOrEmpty(newPassword.Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please, fill all fields", "OK");

            } else if (newPassword.Text.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert("Error", "New Password must be at least 6 characters", "OK");
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
