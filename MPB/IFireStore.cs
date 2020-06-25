using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MPB.Models;

namespace MPB
{
    public interface IFirestore
    {
        Task<string[]> GetData();

        Task<string> EditData(string name, string lastName);

        Task<string> AddTransaction(string category, float money);

        Task<string> AddTransaction2(string category, float money);

        Task<ObservableCollection<Transaction>> GetTransactions();

        Task<string> TotalSum();

        Task<ObservableCollection<Transaction>> GetSomeTransactions(string category);

        Task<string> PartialSum(string category);

        Task<float> SumEarnings();

        Task<float> SumOutflows();



    }
}
