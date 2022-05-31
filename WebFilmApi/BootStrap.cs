using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilmApi
{
    public class BootStrap : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        }

        public static void InitAutofacInstance(ContainerBuilder builder)
        {

        }

    }
}
