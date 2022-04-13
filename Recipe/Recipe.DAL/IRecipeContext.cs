using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.DAL
{
    public interface IRecipeContext
    {
        IDbConnection CreateConnection();
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
    }
}