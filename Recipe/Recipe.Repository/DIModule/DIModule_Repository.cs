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
            builder.RegisterType<GenericRepository<Ingredient>>().As<IGenericRepository<Ingredient>>();
            builder.RegisterType<GenericRepository<IngredientMeasuringUnit>>().As<IGenericRepository<IngredientMeasuringUnit>>();
            builder.RegisterType<GenericRepository<MeasuringUnit>>().As<IGenericRepository<MeasuringUnit>>();
            builder.RegisterType<GenericRepository<Picture>>().As<IGenericRepository<Picture>>();
            builder.RegisterType<GenericRepository<PreparationStep>>().As<IGenericRepository<PreparationStep>>();
            builder.RegisterType<GenericRepository<Models.Recipe>>().As<IGenericRepository<Models.Recipe>>();
            builder.RegisterType<GenericRepository<RecipeAttributes>>().As<IGenericRepository<RecipeAttributes>>();
            builder.RegisterType<GenericRepository<User>>().As<IGenericRepository<User>>();
            builder.RegisterType<GenericRepository<UserLikedRecipe>>().As<IGenericRepository<UserLikedRecipe>>();

            builder.RegisterType<GenericRepository<IIngredient>>().As<IGenericRepository<IIngredient>>();
            builder.RegisterType<GenericRepository<IIngredientMeasuringUnit>>().As<IGenericRepository<IIngredientMeasuringUnit>>();
            builder.RegisterType<GenericRepository<IMeasuringUnit>>().As<IGenericRepository<IMeasuringUnit>>();
            builder.RegisterType<GenericRepository<IPicture>>().As<IGenericRepository<IPicture>>();
            builder.RegisterType<GenericRepository<IPreparationStep>>().As<IGenericRepository<IPreparationStep>>();
            builder.RegisterType<GenericRepository<IRecipe>>().As<IGenericRepository<IRecipe>>();
            builder.RegisterType<GenericRepository<IRecipeAttributes>>().As<IGenericRepository<IRecipeAttributes>>();
            builder.RegisterType<GenericRepository<IUser>>().As<IGenericRepository<IUser>>();
            builder.RegisterType<GenericRepository<IUserLikedRecipe>>().As<IGenericRepository<IUserLikedRecipe>>();

            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterModule<DIModule_Models>();
            builder.RegisterModule<DIModule_DAL>();
        }

    }
}
