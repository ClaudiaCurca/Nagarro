using System.ComponentModel.DataAnnotations;

namespace iQuest.VendingMachine
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
