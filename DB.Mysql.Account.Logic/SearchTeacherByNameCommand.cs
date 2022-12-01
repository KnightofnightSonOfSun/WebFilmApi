using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Utility.Models;
using Common.Extension;
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
                db.AddMysqlParameters("?st_teachername", MySqlDbType.String, input);
                var reader = await db.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var teacherInfoOutput = new TeacherInfoOutput
                    {
                        TeacherId = reader.GetValue<int>("TeacherId"),
                        TeacherName = reader.GetValue<string>("TeacherName"),
                        Score = reader.GetValue<int>("Score")
                    };
                    output.Add(teacherInfoOutput);
                }
                return output;
            }
        }
    }
}
