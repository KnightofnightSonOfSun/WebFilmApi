using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DB.Mysql.Account.Model.Output
{
    public class TeacherInfoOutput
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int Score { get; set; }
    }
}
