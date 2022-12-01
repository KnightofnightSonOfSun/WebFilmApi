using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Attribute;
using Common.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebFilmApi
{
    public class BootStrap : Module
    {

        public static void InitAutofacInstance(ContainerBuilder builder)
        {
            RegisterAllBusinessProject(builder);
        }

        protected static void RegisterAllBusinessProject(ContainerBuilder builder)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var controllersTypesInAssembly = assembly.GetExportedTypes()
                        .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type)
                        && type.Name != "ControllerBase").ToArray();
                    if (controllersTypesInAssembly.Length > 0)
                    {
                        builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired(new AutoWiredPropertySelector());
                    }

                    if (assembly.FullName.StartsWith("WebFilmApi") && assembly.FullName.Contains("Controller"))
                    {
                        var registers = (from t in assembly.GetTypes()
                            where t.IsClass && typeof(IRegister).IsAssignableFrom(t) select t).ToList();
                        foreach (var register in registers)
                        {
                            var provider = Activator.CreateInstance(register) as IRegister;
                            if (provider != null)
                            {
                                provider.Register(builder);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
