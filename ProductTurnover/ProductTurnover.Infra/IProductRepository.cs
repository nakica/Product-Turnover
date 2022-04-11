namespace ProductTurnover.Infra
{
    public interface IProductRepository
    {
        decimal ReadTaxation(int EAN);
    }
}
