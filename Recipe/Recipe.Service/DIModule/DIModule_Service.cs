using Autofac;
using Recipe.Repository.DIModule;

namespace Recipe.Service.DIModule
{
    public class DIModule_Service : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterModule<DIModule_Repository>();
        }
    }
}
