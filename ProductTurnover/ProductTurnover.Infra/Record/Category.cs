using System;

namespace ProductTurnover.Infra.Record
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal VAT { get; set; }
    }
}
