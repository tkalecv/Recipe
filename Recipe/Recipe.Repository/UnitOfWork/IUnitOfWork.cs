using Recipe.Repository.Common.Generic;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task DisposeAsync();      
        Task RollbackAsync();
        IGenericRepository<T> Repository<T>();
    }
}