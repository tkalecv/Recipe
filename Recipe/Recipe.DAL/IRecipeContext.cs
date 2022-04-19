using System.Data;

namespace Recipe.DAL
{
    public interface IRecipeContext
    {
        IDbConnection CreateConnection();
    }
}