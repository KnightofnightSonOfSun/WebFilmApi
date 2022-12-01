using Common.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Common.Data
{
    public class MysqlDatabase :IMysqlDatabase, IDisposable
    {
        private int? _commandTimeout { get; set; }
        private readonly string _commandText;
        private readonly string _connectionStr;

        private static MySqlConnection _connection;

        private MySqlCommand _cmd;

        public MySqlCommand DbCommand
        {
            get
            {
                if (_cmd == null)
                {
                    _cmd = new MySqlCommand(_commandText, _connection);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    if (_commandTimeout.HasValue)
                    {
                        _cmd.CommandTimeout = _commandTimeout.Value;
                    }
                    _cmd.Connection = new MySqlConnection(_connectionStr);
                    _cmd.Connection.Open();
                }

                return _cmd;
            }
        }

        public MysqlDatabase(string connectionStr, string commandText)
        {
            _connectionStr = connectionStr;
            _commandText = commandText;
        }


        public async void ExecuteNonQueryAsync()
        {
            try
            {
                await DbCommand.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<DbDataReader> ExecuteReaderAsync()
        {
            try
            {
                return await DbCommand.ExecuteReaderAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddMysqlParameters(string fieldName, MySqlDbType fieldType, object fieldValue, int fieldDigit = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = new MySqlParameter(fieldName, fieldType);
            param.Direction = direction;
            param.Value = fieldValue;
            if (fieldDigit == 0)
            {
                DbCommand.Parameters.Add(param);
                return;
            }

            param.Size = fieldDigit;
            DbCommand.Parameters.Add(param);
        }

        public void Dispose()
        {
            _cmd.Connection?.Close();
        }
    }
}
