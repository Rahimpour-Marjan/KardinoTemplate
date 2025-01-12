using Domain;

namespace Infrastructure.Persistance.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        IUserLogRepository UserLogRepository { get; }
        Task<bool> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly KardinoTemplateDbContext _db;
        public UnitOfWork(KardinoTemplateDbContext db)
        {
            _db = db;
            UserRepository = new UserRepository(_db);
            AccountRepository = new AccountRepository(_db);
            UserLogRepository = new UserLogRepository(_db);
        }

        public IUserRepository UserRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public IUserLogRepository UserLogRepository { get; }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
