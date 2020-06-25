using System;
using System.Threading.Tasks;

namespace MPB
{
    //i metodi di questa interfaccia sono implementati nella classe AuthDroid in MPB.Android
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);

        bool IsUserLoggedIn();

        bool SignOut();

        Task<string> SignUp(string email, string password, string name, string lastName);

        Task<string> EditPassword(string oldPassword, string newPassword);
    }
}
