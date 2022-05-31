using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Common.Interface
{
    public interface IMysqlDatabase
    {
        public void ExecuteNonQueryAsync();
        public Task<MySqlDataReader> ExecuteReaderAsync();
        public void AddMysqlParameters(string fieldName, MySqlDbType fieldType, int fieldDigit = 0);
    }
}
