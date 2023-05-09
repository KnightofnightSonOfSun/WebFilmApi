using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Attribute;
using Common.Interface;
using DB.Mysql.Account.Logic.Interface;
using DB.Mysql.Account.Model.Input;
using DB.Mysql.Account.Model.Output;
using WebFilmApi.Account.Model.Request;
using WebFilmApi.Account.Model.Response;

namespace WebFilmApi.Account.Logic
{
    public class AddTeacherManager : ILogicManager<AddTeacherRequest>
    {
        [AutoWiredProperty]
        public IMysqlCommand<int, AddTeacherInput> AddTeacherCommand { get; set; }

        public async Task Execute(AddTeacherRequest request)
        {
            var input = new AddTeacherInput
            {
                TeacherName = request.TeacherName,
                TeacherScore = request.TeacherScore,
                TeacherDescription = request.TeacherDescription
            };
            await AddTeacherCommand.ExecuteAsync(input);
        }
    }
}