using System.Data;
using System.Data.Common;

namespace Recipe.DAL
{
    public interface IRecipeContext
    {
        DbConnection CreateConnection();
    }
}