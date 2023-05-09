using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Extension;
using Common.Utility.Models;
using DB.Mysql.Account.Logic.Interface;
using DB.Mysql.Account.Model.Input;
using DB.Mysql.Account.Model.Output;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DB.Mysql.Account.Logic
{
    public class AddTeacherCommand : IMysqlCommand<int, AddTeacherInput>
    {
        private const string _commandText = "AddNewTeacher";
        private readonly string _connectionString;

        public AddTeacherCommand(IOptions<DatabaseConnection> options)
        {
            _connectionString = options.Value.MySqlConnectionStr;
        }

        public async Task<int> ExecuteAsync(AddTeacherInput input)
        {
            using (var db = new MysqlDatabase(_connectionString, _commandText))
            {
                db.AddMysqlParameters("?st_teachername", MySqlDbType.String, input.TeacherName);
                db.AddMysqlParameters("?st_teacherscore", MySqlDbType.Int32, input.TeacherScore);
                db.AddMysqlParameters("?st_description", MySqlDbType.String, input.TeacherDescription);
                await db.ExecuteReaderAsync();
                return 1;
            }
        }
    }
}
