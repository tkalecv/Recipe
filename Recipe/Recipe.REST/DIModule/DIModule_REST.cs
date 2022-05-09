using Autofac;
using Recipe.Auth.DIModule;
using Recipe.Service.DIModule;

namespace Recipe.REST.DIModule
{
    internal class DIModule_REST : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DIModule_Service>();
        }
    }
}
