namespace ProductTurnover.Infra
{
    public interface ITurnover
    {
        decimal CalculateNetTurnover(decimal grossTurnover, decimal vat);
    }
}
