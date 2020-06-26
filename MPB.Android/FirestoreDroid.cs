
using System;
using System.Threading.Tasks;
using MPB;
using MPB.Droid;
using Firebase.Auth;
using Firebase.Firestore;
using Xamarin.Forms;
using System.Diagnostics;
using Android.Gms.Extensions;
using Firebase;
using Java.Util;
using Android.Content;
using System.Collections.Generic;
using static Android.Media.Session.MediaSession;
using System.Collections.ObjectModel;
using System.Linq;
using MPB.Models;
using Transaction = MPB.Models.Transaction;


[assembly: Dependency(typeof(FirestoreDroid))]
namespace MPB.Droid
{

    public class FirestoreDroid : IFirestore
    {
        public ObservableCollection<Transaction> ListTransactions { get; private set; }

        public async Task<string[]> GetData()
        {

            FirebaseFirestore database;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            database = FirebaseFirestore.GetInstance(app);

            DocumentReference docRef = database.Collection("utenti").Document(uid);
            DocumentSnapshot snapshot = (DocumentSnapshot)await docRef.Get();
            String data_name = snapshot.GetString("nome");
            String data_lName = snapshot.GetString("cognome");
            String data_email = snapshot.GetString("email");

            List<string> list = new List<string>();
            list.Add(data_name);
            list.Add(data_lName);
            list.Add(data_email);
            String[] str = list.ToArray();
            return str;

        }

        public async Task<string> EditData(String name, String lastName)
        {
            try
            {

                FirebaseFirestore database;
                string uid = FirebaseAuth.Instance.CurrentUser.Uid;
                var app = FirebaseApp.Instance;
                database = FirebaseFirestore.GetInstance(app);

                DocumentReference docRef = database.Collection("utenti").Document(uid);
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(lastName))
                {
                    var task = await docRef.Update("nome", name, "cognome", lastName);
                    return task.ToString(); //.update ritorna task<void>
                }
                else return "error";
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public async Task<string> AddTransaction(string category, string money)
        {
            try
            {
                FirebaseFirestore database;
                string uid = FirebaseAuth.Instance.CurrentUser.Uid;
                var app = FirebaseApp.Instance;
                database = FirebaseFirestore.GetInstance(app);
                string date = DateTime.Today.ToString("dd/MM/yyyy");
                string type = "Earnings";
                



                HashMap map = new HashMap();
                map.Put("tipologia", type);
                map.Put("cifra", money);
                map.Put("categoria", category);
                map.Put("data", date);
                map.Put("createdAt", Timestamp.Now()); //servirà per ordinare i documenti quando si estraggono


                DocumentReference docRef = database.Collection("utenti").Document(uid).Collection("transazioni").Document();
                var task = await docRef.Set(map);
                
                return "ok";

            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return "error";
            }

        }

        public async Task<string> AddTransaction2(string category, string money)
        {
            try
            {
                FirebaseFirestore database;
                string uid = FirebaseAuth.Instance.CurrentUser.Uid;
                var app = FirebaseApp.Instance;
                database = FirebaseFirestore.GetInstance(app);
                string date = DateTime.Today.ToString("dd/MM/yyyy");
                string type = "Outflows";

                HashMap map = new HashMap();
                map.Put("tipologia", type);
                map.Put("cifra", money);
                map.Put("categoria", category);
                map.Put("data", date);
                map.Put("createdAt", Timestamp.Now());



                DocumentReference docRef = database.Collection("utenti").Document(uid).Collection("transazioni").Document();
                var task = await docRef.Set(map);
                return "ok";

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return "error";
            }

        }

        public async Task<ObservableCollection<Transaction>> GetTransactions()
        {
            ObservableCollection<Transaction> ListTransactions = new ObservableCollection<Transaction>();
            FirebaseFirestore db;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            db = FirebaseFirestore.GetInstance(app);
            Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                Collection("transazioni").OrderBy("createdAt", Query.Direction.Ascending); //vettore lista documenti
            QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //vettore lista delle informazioni (dati) di ogni documento

            foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
            {
                ListTransactions.Add(new Transaction()
                {
                    Type = documentSnapshot.GetString("tipologia"),
                    Category = documentSnapshot.GetString("categoria"),
                    Money = documentSnapshot.GetString("cifra"),
                    Date = documentSnapshot.GetString("data")
                });
            }

            return ListTransactions;
        }

        public async Task<string> TotalSum()
        {
            float sum = 0;
            FirebaseFirestore db;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            db = FirebaseFirestore.GetInstance(app);
            Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                Collection("transazioni"); //lista documenti
            QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //lista delle informazioni (dati) di ogni documento

            foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
            {
                if (String.Equals(documentSnapshot.GetString("tipologia"), "Earnings"))
                {
                    sum += (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
                   

                }
                else if (String.Equals(documentSnapshot.GetString("tipologia"), "Outflows"))
                {
                    sum -= (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
                }
               

            }
            string sumString;
            if (sum >= 0)
            {
                sumString = "+" + sum.ToString();
            }
            else
            {
                sumString = sum.ToString();
            }
            return sumString;

        }

        public async Task<ObservableCollection<Transaction>> GetSomeTransactions(string category)
        {
            try
            {
                ObservableCollection<Transaction> ListTransactions = new ObservableCollection<Transaction>();
                FirebaseFirestore db;
                string uid = FirebaseAuth.Instance.CurrentUser.Uid;
                var app = FirebaseApp.Instance;
                db = FirebaseFirestore.GetInstance(app);

                //prendo solo i documenti con il campo categoria uguale alla categoria selezionata
                Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                    Collection("transazioni").WhereEqualTo("categoria", category).OrderBy("createdAt", Query.Direction.Ascending); //vettore lista documenti
                QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //vettore lista delle informazioni (dati) di ogni documento

                foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
                {
                    ListTransactions.Add(new Transaction()
                    {
                        Type = documentSnapshot.GetString("tipologia"),
                        Category = documentSnapshot.GetString("categoria"),
                        Money = documentSnapshot.GetString("cifra"),
                        Date = documentSnapshot.GetString("data")
                    });
                }

                return ListTransactions;
            }
            catch (Exception e)
            {
                //Debug.WriteLine($"Error:{e}");
                return ListTransactions;
            }

        }

        public async Task<string> PartialSum(string category)
        {
            float sum = 0;
            FirebaseFirestore db;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            db = FirebaseFirestore.GetInstance(app);
            Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                Collection("transazioni").WhereEqualTo("categoria", category); //vettore lista documenti
            QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //vettore lista delle informazioni (dati) di ogni documento

            foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
            {
                if (String.Equals(documentSnapshot.GetString("tipologia"), "Earnings"))
                {
                    sum += (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
                }
                else if (String.Equals(documentSnapshot.GetString("tipologia"), "Outflows"))
                {
                    sum -= (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
                }

            }

            
            string sumString;
            if (sum >= 0)
            {
                sumString = "+" + sum.ToString();
            }
            else
            {
                sumString = sum.ToString();
            }
            return sumString;

        }


        

        public async Task<float> SumEarnings()
        {
            float sum = 0;
            FirebaseFirestore db;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            db = FirebaseFirestore.GetInstance(app);
            Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                Collection("transazioni").WhereEqualTo("tipologia", "Earnings"); //lista documenti
            QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //lista delle informazioni (dati) di ogni documento

            foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
            {
                sum += (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
            }

            return sum;

        }

        public async Task<float> SumOutflows()
        {
            float sum = 0;
            FirebaseFirestore db;
            string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            var app = FirebaseApp.Instance;
            db = FirebaseFirestore.GetInstance(app);
            Query allTransactionsQuery = db.Collection("utenti").Document(uid).
                Collection("transazioni").WhereEqualTo("tipologia", "Outflows"); //lista documenti
            QuerySnapshot allTransactionsQuerySnapshot = (QuerySnapshot)await allTransactionsQuery.Get(); //lista delle informazioni (dati) di ogni documento

            foreach (DocumentSnapshot documentSnapshot in allTransactionsQuerySnapshot.Documents)
            {
                sum -= (float)Math.Round(float.Parse(documentSnapshot.GetString("cifra")), 2);
            }

            return sum;

        }
        


    }
}

