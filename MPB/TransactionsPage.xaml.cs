using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MPB.Models;
using Xamarin.Forms;


namespace MPB
{
    public partial class TransactionsPage : ContentPage
    {
        IFirestore firestore;
        public ObservableCollection<Transaction> MyList { get; set; } //lista di transazioni

        public TransactionsPage()
        {
            InitializeComponent();
            firestore = DependencyService.Get<IFirestore>();
            this.BindingContext = this;
            CheckFilter();

        }

        private void CheckFilter()
        {
            if ((String.IsNullOrEmpty((string)PickerCategory.SelectedItem) || String.Equals((string)PickerCategory.SelectedItem, "All")))
            {
                GetListTransactions(); //se non ci sono filtri mostra tutte le transazioni
                GetTotal(); //bilancio totale
            }
            else
            {
                GetListSomeTransactions((string)(PickerCategory.SelectedItem)); //mostra transazioni del filtro totale
                GetPartial((string)(PickerCategory.SelectedItem)); //mostra i soldi spesi/guadagnati per una categoria
            }

        }



        private void ShowAddTrans(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTransPage());
        }

        private async void GetListTransactions()
        {
            MyList = new ObservableCollection<Transaction>();
            ObservableCollection<Transaction> task = await firestore.GetTransactions(); //ritorna la lista delle transazioni
            MyList = task;
            TransactionsList.ItemsSource = MyList;

        }

        private async void GetTotal()
        {
            string total = await firestore.TotalSum();
            labelSum.Text = total;
        }




        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                CheckFilter();
            }
        }

        private async void GetListSomeTransactions(string categorySelected)
        {
            MyList = new ObservableCollection<Transaction>();
            ObservableCollection<Transaction> task = await firestore.GetSomeTransactions(categorySelected); //ritorna la lista delle transazioni della categoria selezionta
            MyList = task;
            TransactionsList.ItemsSource = MyList;

        }

        private async void GetPartial(string categorySelected)
        {
            string total = await firestore.PartialSum(categorySelected);
            labelSum.Text = total; //setto il valore della label
        }






    }

}
