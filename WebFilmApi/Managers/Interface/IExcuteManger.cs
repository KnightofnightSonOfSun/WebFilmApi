using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFilmApi.Models.Request;
using WebFilmApi.Models.Response;

namespace WebFilmApi.Managers.Interface
{
    public interface IExcuteManger<TRequest, TResponse> where TRequest : BaseRequest where TResponse : BaseResponse
    {
        TResponse Excute(TRequest request);
    }
}
