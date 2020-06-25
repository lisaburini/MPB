using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{
    public partial class SectionProfilePage : ContentPage
    {
        IAuth auth;
        public SectionProfilePage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private void Profile(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            Navigation.PushAsync(new ProfilePage());
        }

        private void SignOut(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            bool valueSignOut = auth.SignOut();
            if (valueSignOut)
            {
                App.Current.MainPage = new NavigationPage(new IntroPage());
            }
            else App.Current.MainPage.DisplayAlert("Error Logout", "Error", "OK");

        }

        private void EditProfile(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            Navigation.PushAsync(new EditProfilePage());
        }

        private void EditPassword(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            Navigation.PushAsync(new EditPasswordPage());
        }
    }
}
