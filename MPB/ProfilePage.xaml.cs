using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{
    public partial class ProfilePage : ContentPage
    {
        IFirestore firestore;
        public ProfilePage()
        {
            InitializeComponent();
            firestore = DependencyService.Get<IFirestore>();
            showData();

        }

        public async void showData()
        {

            var values = await firestore.GetData();
            LabelName.Text = values.GetValue(0).ToString();
            LabelLastName.Text = values.GetValue(1).ToString();
            LabelEmail.Text = values.GetValue(2).ToString();

        }
    }
}
