using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension
{
    public static class DbDataReaderExtension
    {
        public static T GetValue<T>(this DbDataReader dbDataReader, string fieldName)
        {
            try
            {
                if (string.IsNullOrEmpty(fieldName))
                {
                    throw new ArgumentException(nameof(fieldName), "fieldName cannot be null or empty");
                }

                if (dbDataReader[fieldName] == DBNull.Value)
                    return default(T);
                return dbDataReader[fieldName].ConvertTo<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting value from FieldName = {fieldName}", ex);
            }
        }
    }
}
