using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Recipe.DAL
{
    public interface IRecipeContext
    {
        IDbConnection CreateConnection();
    }
}