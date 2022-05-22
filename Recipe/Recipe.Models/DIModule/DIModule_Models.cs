using Autofac;
using Recipe.Models.Common;

namespace Recipe.Models.DIModule
{
    public class DIModule_Models : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Ingredient>().As<IIngredient>();
            builder.RegisterType<MeasuringUnit>().As<IMeasuringUnit>();
            builder.RegisterType<Picture>().As<IPicture>();
            builder.RegisterType<PreparationStep>().As<IPreparationStep>();
            builder.RegisterType<Recipe>().As<IRecipe>();
            builder.RegisterType<RecipeAttributes>().As<IRecipeAttributes>();
            builder.RegisterType<UserData>().As<IUserData>();
            builder.RegisterType<Category>().As<ICategory>();
            builder.RegisterType<Subcategory>().As<ISubcategory>();
        }
    }
}
