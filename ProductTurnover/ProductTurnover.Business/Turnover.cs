using ProductTurnover.Infra;

namespace ProductTurnover.Business
{
    public class Turnover : ITurnover
    {
        public decimal CalculateNetTurnover(decimal grossTurnover, decimal vat)
        {
            var netTurnover = grossTurnover - (grossTurnover * vat);
            return netTurnover;
        }
    }
}
