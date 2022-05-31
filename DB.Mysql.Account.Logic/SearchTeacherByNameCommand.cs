using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Utility.Models;
using DB.Mysql.Account.Logic.Interface;
using DB.Mysql.Account.Model.Output;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DB.Mysql.Account.Logic
{
    public class SearchTeacherByNameCommand : IMysqlCommand<List<TeacherInfoOutput>, string>
    {
        private const string _commandText = "SearchTeacher";
        private readonly string _connectionString;

        public SearchTeacherByNameCommand(IOptions<DatabaseConnection> options)
        {
            _connectionString = options.Value.MySqlConnectionStr;
        }

        public async Task<List<TeacherInfoOutput>> ExecuteAsync(string input)
        {
            var output = new List<TeacherInfoOutput>();
            using (var db = new MysqlDatabase(_connectionString, _commandText))
            {
                db.AddMysqlParameters("st_teachername", MySqlDbType.VarChar, 20);
                var reader = await db.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var teacherInfoOutput = new TeacherInfoOutput
                    {
                        TeacherId = reader.GetInt32("TeacherId"),
                        TeacherName = reader.GetString("TeacherName"),
                        Score = reader.GetInt16("Score")
                    };
                    output.Add(teacherInfoOutput);
                }
                return output;
            }
        }
    }
}
