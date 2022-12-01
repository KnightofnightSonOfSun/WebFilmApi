using WebFilmApi.Account.Model.Response;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Attribute;
using DB.Mysql.Account.Logic.Interface;
using DB.Mysql.Account.Model.Output;

namespace WebFilmApi.Account.Logic
{
    public class SearchTeacherManager : ILogicManager<SearchTeacherResponse, string>
    {
        [AutoWiredProperty]
        public IMysqlCommand<List<TeacherInfoOutput>, string> SearchTeacherCommand { get; set; }

        public async Task<SearchTeacherResponse> Execute(string request)
        {
            var result = new SearchTeacherResponse();
            var response = await SearchTeacherCommand.ExecuteAsync(request);
            if (response.Count > 0)
            {
                result.TeacherName = response[0].TeacherName;
                result.TeacherId = response[0].TeacherId;
                result.Score = response[0].Score;
            }

            return result;
        }
    }
}
