using Autofac;
using Recipe.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Recipe.Models.DIModule
{
    public class DIModule_Models : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Ingredient>().As<IIngredient>();
            builder.RegisterType<IngredientMeasuringUnit>().As<IIngredientMeasuringUnit>();
            builder.RegisterType<MeasuringUnit>().As<IMeasuringUnit>();
            builder.RegisterType<Picture>().As<IPicture>();
            builder.RegisterType<PreparationStep>().As<IPreparationStep>();
            builder.RegisterType<Recipe>().As<IRecipe>();
            builder.RegisterType<RecipeAttributes>().As<IRecipeAttributes>();
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<UserLikedRecipe>().As<IUserLikedRecipe>();
        }
    }
}
