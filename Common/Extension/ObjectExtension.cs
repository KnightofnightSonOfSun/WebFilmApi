using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension
{
    public static class ObjectExtension
    {
        public static T ConvertTo<T>(this object value)
        {
            var valueType = Nullable.GetUnderlyingType(typeof(T));
            if (valueType != null)
            {
                if (value == null)
                {
                    return default(T);
                }

                var result = Convert.ChangeType(value, valueType);
                return (T) result;
            }

            return (T) Convert.ChangeType(value, typeof(T));
        }
    }
}
