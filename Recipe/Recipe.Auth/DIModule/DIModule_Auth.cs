using Autofac;
using Recipe.Auth.Models;
using Recipe.Auth.ModelsCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.Auth.DIModule
{
    public class DIModule_Auth : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FirebaseClient>().As<IFirebaseClient>();
            builder.RegisterType<AuthUser>().As<IAuthUser>();
        }
    }
}
