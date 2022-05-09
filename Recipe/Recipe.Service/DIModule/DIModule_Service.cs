using Autofac;
using Recipe.Auth.DIModule;
using Recipe.Repository.DIModule;
using Recipe.Service.Common;

namespace Recipe.Service.DIModule
{
    public class DIModule_Service : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientService>().As<IIngredientService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterModule<DIModule_Auth>();
            builder.RegisterModule<DIModule_Repository>();
        }
    }
}
