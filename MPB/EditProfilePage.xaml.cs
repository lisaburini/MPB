using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{

    public partial class EditProfilePage : ContentPage
    {
        IFirestore firestore;
        public EditProfilePage()
        {
            InitializeComponent();
            firestore = DependencyService.Get<IFirestore>();
        }

        private async void Edit(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(name.Text) || String.IsNullOrEmpty(lastName.Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please, fill all fields", "OK");
            }

            else
            {

                var token = await firestore.EditData(name.Text, lastName.Text);

                if(!string.Equals(token, "error"))
                {
                    App.Current.MainPage = new MainPage();
                } else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                }

            }
        }
    }

}
