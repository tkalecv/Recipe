﻿using Autofac;
using Recipe.Auth.DIModule;
using Recipe.Repository.DIModule;
using Recipe.Service.Common;
using Recipe.Service.Common.Helpers;
using Recipe.Service.Helpers;

namespace Recipe.Service.DIModule
{
    public class DIModule_Service : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IngredientService>().As<IIngredientService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<UserRegistrationHelper>().As<IUserRegistrationHelper>();

            builder.RegisterModule<DIModule_Auth>();
            builder.RegisterModule<DIModule_Repository>();
        }
    }
}
