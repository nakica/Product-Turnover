using System;

namespace ProductTurnover.Rest
{
    public class ProductTurnover
    {
        public DateTime DateCreated { get; set; }
        public string EAN { get; set; }
        public decimal GrossTurnover { get; set; }
        public string ProductName { get; set; }
    }
}
