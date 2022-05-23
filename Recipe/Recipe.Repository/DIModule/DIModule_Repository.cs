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
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();

            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterModule<DIModule_Models>();
            builder.RegisterModule<DIModule_DAL>();
        }

    }
}
