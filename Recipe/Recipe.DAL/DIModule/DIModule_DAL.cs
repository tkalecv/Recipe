using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe.DAL.DIModule
{
    public class DIModule_DAL : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeContext>().As<IRecipeContext>();
        }
    }
}
