using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;

namespace Common.Attribute
{
    public class AutoWiredPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CustomAttributes.Any(s => s.AttributeType == typeof(AutoWiredPropertyAttribute));
        }
    }
}
