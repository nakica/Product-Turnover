using ProductTurnover.Infra.Record;

namespace ProductTurnover.Infra
{
    public interface IProductRepository
    {
        /// <summary>
        /// Reads a product with a specified EAN.
        /// </summary>
        /// <param name="EAN">European Article Number</param>
        Product Read(string EAN);
    }
}
