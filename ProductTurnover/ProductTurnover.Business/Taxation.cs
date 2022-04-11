using ProductTurnover.Infra;

namespace ProductTurnover.Business
{
    public class Taxation : ITaxation
    {
        public decimal CalculateNetTurnover(decimal grossTurnover, decimal vat)
        {
            var netTurnover = grossTurnover - (grossTurnover * vat);
            return netTurnover;
        }
    }
}
