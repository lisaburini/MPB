using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MPB.Models;


namespace MPB
{
    //i metodi di questa interfaccia sono implementati nella classe FirestoreDroid in MPB.Android
    public interface IFirestore
    {
        Task<string[]> GetData();

        Task<string> EditData(string name, string lastName);

        Task<string> AddTransaction(string category, string money);

        Task<string> AddTransaction2(string category, string money);

        Task<ObservableCollection<Transaction>> GetTransactions();

        Task<string> TotalSum();

        Task<ObservableCollection<Transaction>> GetSomeTransactions(string category);

        Task<string> PartialSum(string category);

        Task<float> SumEarnings();

        Task<float> SumOutflows();



    }
}
