using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Common.Interface
{
    public interface IMysqlDatabase
    {
        public void ExecuteNonQueryAsync();
        public Task<DbDataReader> ExecuteReaderAsync();
        public void AddMysqlParameters(string fieldName, MySqlDbType fieldType, object fieldValue, int fieldDigit = 0, ParameterDirection direction = ParameterDirection.Input);
    }
}
