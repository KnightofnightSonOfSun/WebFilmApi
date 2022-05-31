using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Mysql.Account.Logic.Interface
{
    public interface IMysqlCommand<TOutput, in TInput>
    {
        Task<TOutput> ExecuteAsync(TInput input);
    }
}
