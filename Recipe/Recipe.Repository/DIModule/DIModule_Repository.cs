using Autofac;
using Recipe.DAL.DIModule;
using Recipe.Models.Common;
using Recipe.Models.DIModule;
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

            //TODO: remove this
            //builder.RegisterType<Ingredient>().As<IIngredient>();
            //builder.RegisterType<IngredientMeasuringUnit>().As<IIngredientMeasuringUnit>();
            //builder.RegisterType<MeasuringUnit>().As<IMeasuringUnit>();
            //builder.RegisterType<Picture>().As<IPicture>();
            //builder.RegisterType<PreparationStep>().As<IPreparationStep>();
            //builder.RegisterType<Recipe>().As<IRecipe>();
            //builder.RegisterType<RecipeAttributes>().As<IRecipeAttributes>();
            //builder.RegisterType<User>().As<IUser>();
            //builder.RegisterType<UserLikedRecipe>().As<UserLikedRecipe>();
        }

    }
}
