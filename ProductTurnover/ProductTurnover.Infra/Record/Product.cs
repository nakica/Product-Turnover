using System;

namespace ProductTurnover.Infra.Record
{
    public class Product
    {
        public Guid Category { get; set; }
        public int EAN { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
