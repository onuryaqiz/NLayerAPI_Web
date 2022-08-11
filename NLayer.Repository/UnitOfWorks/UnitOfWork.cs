using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync() //Yukarıdaki Commit metodu olmasının sebebi SaveChangesAsync().Result yazmak yerine ayrı method olarak yazdık.Thread'i blokladığı için.
        {
            await _context.SaveChangesAsync();
        }
    }
}
