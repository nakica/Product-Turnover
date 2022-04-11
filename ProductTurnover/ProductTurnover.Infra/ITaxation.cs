namespace ProductTurnover.Infra
{
    public interface ITaxation
    {
        /// <summary>
        /// Calculates net turnover by subtracking the VAT from the gross turnover.
        /// </summary>
        /// <param name="grossTurnover">Turnover without taxes.</param>
        /// <param name="vat">Value added tax. Can be between 0 and 1.</param>
        /// <returns>Turnover with applied vat.</returns>
        decimal CalculateNetTurnover(decimal grossTurnover, decimal vat);
    }
}
