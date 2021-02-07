namespace FleaMarket
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepository { get; }

        int Complete();
    }
}
