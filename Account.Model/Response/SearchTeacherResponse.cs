using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFilmApi.Account.Model.Response
{
    public class SearchTeacherResponse : BaseResponse
    {

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int Score { get; set; }

    }
}
