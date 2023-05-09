using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFilmApi.Account.Model.Request
{
    public class AddTeacherRequest
    {
        public string TeacherName { get; set; }
        public int TeacherScore { get; set; }
        public string TeacherDescription { get; set; }
    }
}
