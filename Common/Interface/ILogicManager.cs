using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface ILogicManager<TResponse, in TRequest>
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface ILogicManager<in TRequest>
    {
        Task Execute(TRequest request);
    }
}
