/*************************************************************************************
 *
 * File name:   IExcuteManger.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/13 22:41
 * ======================================
*************************************************************************************/
using Koknight.Basic.Structure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Structure
{
    public interface IExcuteManger<TRequest, TResponse> where TRequest : BaseRequest where TResponse : BaseResponse
    {
        TResponse Excute(TRequest request);
    }
}
