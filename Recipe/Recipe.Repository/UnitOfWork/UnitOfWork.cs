using Recipe.DAL;
using Recipe.Repository.Common;
using Recipe.Repository.Common.UnitOfWork;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Recipe.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection { get; set; } = null;
        private DbTransaction _transaction { get; set; } = null;

        #region Repositories
        private IRecipeRepository _recipeRepository;
        public IRecipeRepository RecipeRepository
        {
            get
            {
                if (_recipeRepository == null)
                    _recipeRepository = new RecipeRepository(_transaction);

                return _recipeRepository;
            }
        }

        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_transaction);

                return _categoryRepository;
            }
        }

        private ISubcategoryRepository _subcategoryRepository;
        public ISubcategoryRepository SubcategoryRepository
        {
            get
            {
                if (_subcategoryRepository == null)
                    _subcategoryRepository = new SubcategoryRepository(_transaction);

                return _subcategoryRepository;
            }
        }

        private IUserDataRepository _userDataRepository;
        public IUserDataRepository UserDataRepository
        {
            get
            {
                if (_userDataRepository == null)
                    _userDataRepository = new UserDataRepository(_transaction);

                return _userDataRepository;
            }
        }
        #endregion

        public UnitOfWork(IRecipeContext recipeContext)
        {
            _connection = recipeContext.CreateConnection();
        }

        /// <summary>
        /// Method asynchronously opens a database connection and begins a database transaction
        /// </summary>
        /// <returns>Task</returns>
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            if (_transaction == null)
                _transaction = await _connection.BeginTransactionAsync();
        }

        /// <summary>
        /// Method asynchronously commits the database transaction and disposes the transaction and connection objects
        /// </summary>
        /// <returns>Task</returns>
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await DisposeAsync();
        }

        /// <summary>
        /// Method asynchronously rolls back a transaction from a pending state
        /// </summary>
        /// <returns>Task</returns>
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await DisposeAsync();
        }

        /// <summary>
        /// Method asynchronously disposes the transaction and connection objects
        /// </summary>
        /// <returns>Task</returns>
        public async Task DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _transaction = null;

            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}
