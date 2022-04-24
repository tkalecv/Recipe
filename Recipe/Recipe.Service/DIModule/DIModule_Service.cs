using Autofac;
using Recipe.Repository.DIModule;
using Recipe.Service.Common;

namespace Recipe.Service.DIModule
{
    public class DIModule_Service : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientService>().As<IIngredientService>();

            builder.RegisterModule<DIModule_Repository>();
        }
    }
}
