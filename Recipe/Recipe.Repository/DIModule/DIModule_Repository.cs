using Autofac;
using Recipe.DAL.DIModule;
using Recipe.Models;
using Recipe.Models.Common;
using Recipe.Models.DIModule;
using Recipe.Repository.Common;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.Generic;
using Recipe.Repository.UnitOfWork;

namespace Recipe.Repository.DIModule
{
    public class DIModule_Repository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO: should we put here interfaces or classes?
            builder.RegisterType<GenericRepository<Ingredient>>().As<IGenericRepository<IIngredient>>();
            builder.RegisterType<GenericRepository<IngredientMeasuringUnit>>().As<IGenericRepository<IIngredientMeasuringUnit>>();
            builder.RegisterType<GenericRepository<MeasuringUnit>>().As<IGenericRepository<IMeasuringUnit>>();
            builder.RegisterType<GenericRepository<Picture>>().As<IGenericRepository<IPicture>>();
            builder.RegisterType<GenericRepository<PreparationStep>>().As<IGenericRepository<IPreparationStep>>();
            builder.RegisterType<GenericRepository<Models.Recipe>>().As<IGenericRepository<IRecipe>>();
            builder.RegisterType<GenericRepository<RecipeAttributes>>().As<IGenericRepository<IRecipeAttributes>>();
            builder.RegisterType<GenericRepository<User>>().As<IGenericRepository<IUser>>();
            builder.RegisterType<GenericRepository<UserLikedRecipe>>().As<IGenericRepository<IUserLikedRecipe>>();

            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();

            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterModule<DIModule_Models>();
            builder.RegisterModule<DIModule_DAL>();
        }

    }
}
