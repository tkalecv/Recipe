using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task DisposeAsync();
        Task RollbackAsync();
        IGenericRepository<T> Repository<T>();
        IRecipeRepository RecipeRepository { get; }
    }
}