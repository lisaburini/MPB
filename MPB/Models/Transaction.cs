using System;
namespace MPB.Models
{
    public class Transaction
    {

        public string Date { get; set; } //data di inserimento
        public string Type { get; set; } //uscita o entrata
        public string Category { get; set; } //categoria transazione (salary, pension, gitf...)
        public float Money { get; set; } //valore transazione

    }
}

