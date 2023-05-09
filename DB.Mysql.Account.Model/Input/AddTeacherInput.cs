using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Mysql.Account.Model.Input
{
    public class AddTeacherInput
    {
        public string TeacherName { get; set; }
        public int TeacherScore { get; set; }
        public string TeacherDescription { get; set; }

    }
}
