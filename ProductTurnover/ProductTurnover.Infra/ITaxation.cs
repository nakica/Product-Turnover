namespace ProductTurnover.Infra
{
    public interface ITaxation
    {
        decimal CalculateNetTurnover(decimal grossTurnover, decimal vat);
    }
}
