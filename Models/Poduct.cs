namespace PriceComparisiontool.Models
{
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }

        public ICollection<Price> Prices { get; set; }
    }

    public class Price
    {
        public int Id { get; set; }
        public string Store { get; set; }
        public decimal Amount { get; set; }
        public string Url { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}
