using Recipe.Repository.Common;
using System.Threading.Tasks;

namespace Recipe.Repository.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task DisposeAsync();
        Task RollbackAsync();
        IRecipeRepository RecipeRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISubcategoryRepository SubcategoryRepository { get; }
        IUserDataRepository UserDataRepository { get; }

    }
}