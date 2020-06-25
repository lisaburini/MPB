
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



[assembly: Dependency(typeof(AuthDroid))]
namespace MPB.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {

            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }

            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

           
        }

        public bool IsUserLoggedIn()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return true; //se utente è già loggato ritorna true
            }
            else return false;
        }

        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SignUp(string email, string password, string name, string lastName)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                //per salvare dati nella collection users
                if (token.Token != null)
                {
                    FirebaseFirestore database;
                    string uid = FirebaseAuth.Instance.CurrentUser.Uid;
                    
                    var app = FirebaseApp.Instance;

                    database = FirebaseFirestore.GetInstance(app);

                    HashMap map = new HashMap();
                    map.Put("email", email);
                    map.Put("nome", name);
                    map.Put("cognome", lastName);

                    DocumentReference docRef = database.Collection("utenti").Document(uid); 
                    await docRef.Set(map);
                }

                return token.Token;


            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }

        public async Task<string> EditPassword(string oldPassword, string newPassword)
        {
            try
            {
                string current_email = FirebaseAuth.Instance.CurrentUser.Email;
                AuthCredential credential = EmailAuthProvider.GetCredential(current_email, oldPassword);
                await FirebaseAuth.Instance.CurrentUser.Reauthenticate(credential); //per cambiare password bisogna riautenticare
                await FirebaseAuth.Instance.CurrentUser.UpdatePasswordAsync(newPassword);
                return "ok";

            }
            catch
            {
                return null;
            }


        }


    }
}


