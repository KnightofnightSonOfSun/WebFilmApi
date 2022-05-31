using Common.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Data
{
    public class MysqlDatabase :IMysqlDatabase, IDisposable
    {
        private int? _commandTimeout { get; set; }
        private readonly string _commandText;
        private readonly string _connectionStr;
        private List<MySqlParameter> _parameters;

        private static MySqlConnection _connection;

        public MysqlDatabase(string connectionStr, string commandText)
        {
            _connectionStr = connectionStr;
            _commandText = commandText;
            GetMySqlConnection();
        }

        private void GetMySqlConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionStr);
            }
        }

        public async void ExecuteNonQueryAsync()
        {
            var responseCode = await MySqlHelper.ExecuteNonQueryAsync(_connection, _commandText, _parameters.ToArray());

        }

        public async Task<MySqlDataReader> ExecuteReaderAsync()
        {
            var response = await MySqlHelper.ExecuteReaderAsync(_connection, _commandText, _parameters.ToArray());
            return response;
        }

        public void AddMysqlParameters(string fieldName, MySqlDbType fieldType, int fieldDigit = 0)
        {
            if (fieldDigit == 0)
            {
                _parameters.Add(new MySqlParameter(fieldName, fieldType));
                return;
            }
            _parameters.Add(new MySqlParameter(fieldName, fieldType, fieldDigit));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
