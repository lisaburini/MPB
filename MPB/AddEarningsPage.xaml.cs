using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{
    public partial class AddEarningsPage : ContentPage
    {
        IFirestore firestore;
        public AddEarningsPage()
        {
            InitializeComponent();
            firestore = DependencyService.Get<IFirestore>();
        }


        async private void AddEarnings(object sender, EventArgs e)
        {

            //tutti i campi devono essere compilati
            if (String.Equals(EarningsCategory.SelectedItem, null) || String.IsNullOrEmpty(money.Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");
            }
            else if (!IsDigitsOnly(money.Text))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Insert only digits, dot or comma", "OK");
            }
            else
            {
                string category = EarningsCategory.SelectedItem.ToString();
                string moneyTrans = money.Text.Replace(',', '.'); //se valore contiene virgola viene sostituita
                

                string task = await firestore.AddTransaction(category, moneyTrans);
                if (String.Equals(task, "ok"))
                {
                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Try Again", "OK");
                }
            }
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!String.Equals(c, '0') && !String.Equals(c, '1') && !String.Equals(c, '2')
                    && !String.Equals(c, '3') && !String.Equals(c, '4') && !String.Equals(c, '5')
                    && !String.Equals(c, '6') && !String.Equals(c, '7') && !String.Equals(c, '8')
                    && !String.Equals(c, '9') && !String.Equals(c, '.') && !String.Equals(c, ','))
                    return false;
            }
            return true;
        }


    }
}
