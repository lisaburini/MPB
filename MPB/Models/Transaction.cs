using System;
namespace progetto_lisa.Models
{
    public class Transaction
    {

        public string Date { get; set; } //data di inserimento
        public string Type { get; set; } //uscita o entrata
        public string Category { get; set; }
        public float Money { get; set; }

    }
}

