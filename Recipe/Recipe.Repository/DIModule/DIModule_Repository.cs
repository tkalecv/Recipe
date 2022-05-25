using Autofac;
using Recipe.DAL.DIModule;
using Recipe.Models.DIModule;
using Recipe.Repository.Common;
using Recipe.Repository.UnitOfWork;

namespace Recipe.Repository.DIModule
{
    public class DIModule_Repository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<RecipeRepository>().As<IRecipeRepository>();
            builder.RegisterType<SubcategoryRepository>().As<ISubcategoryRepository>();
            builder.RegisterType<UserDataRepository>().As<IUserDataRepository>();

            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterModule<DIModule_Models>();
            builder.RegisterModule<DIModule_DAL>();
        }

    }
}
