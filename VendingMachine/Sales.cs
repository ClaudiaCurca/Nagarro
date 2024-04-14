using System;
using System.ComponentModel.DataAnnotations;

namespace iQuest.VendingMachine
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PaymentMethod { get; set; }

    }
}
