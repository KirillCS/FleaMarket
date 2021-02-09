namespace FleaMarket.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepository { get; }

        int Complete();
    }
}
