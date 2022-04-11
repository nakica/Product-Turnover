namespace ProductTurnover.Infra.Record
{
    public class Product
    {
        public Category Category { get; set; }
        public string EAN { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
