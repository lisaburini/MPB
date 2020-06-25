using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace progetto_lisa
{
    public partial class AddTransPage : ContentPage
    {
        public AddTransPage()
        {
            InitializeComponent();
        }

        private void AddEarnings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEarningsPage());
        }

        private void AddOutflows(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOutflowsPage());
        }
    }
}
