using FleaMarket.Data;

namespace FleaMarket
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DatabaseContext context;

        public IItemRepository ItemRepository { get; private set; }

        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
            ItemRepository = new ItemRepository(context);
        }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
