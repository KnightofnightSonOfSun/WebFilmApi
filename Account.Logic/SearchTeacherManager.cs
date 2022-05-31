using WebFilmApi.Account.Model.Response;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFilmApi.Account.Logic
{
    public class SearchTeacherManager : ILogicManager<SearchTeacherResponse, string>
    {
        public Task<SearchTeacherResponse> Execute(string request)
        {
            throw new NotImplementedException();
        }
    }
}
