using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipe.DAL
{
    public interface IRecipeContext
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
    }
}