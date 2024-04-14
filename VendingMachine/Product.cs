using System.ComponentModel.DataAnnotations;

namespace iQuest.VendingMachine
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int ColumnId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Product))
            {
                return false;
            }
            Product productToCompare = (Product)obj;
            return (this.ColumnId == productToCompare.ColumnId)
                && (this.Name == productToCompare.Name)
                && (this.Price == productToCompare.Price)
                && (this.Quantity == productToCompare.Quantity);
        }

    }
}
