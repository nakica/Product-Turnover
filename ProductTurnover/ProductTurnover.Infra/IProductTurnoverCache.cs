namespace ProductTurnover.Infra
{
    public interface IProductTurnoverCache
    {
        void Add<T>(T item, string key);
        bool TryGetValue<T>(string key, out T item);
    }
}
