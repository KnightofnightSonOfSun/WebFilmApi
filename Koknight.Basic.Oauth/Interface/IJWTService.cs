/*************************************************************************************
 *
 * File name:   IJWTService.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/13 22:37
 * ======================================
*************************************************************************************/
using Koknight.Basic.Structure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Basic.Oauth.Interface
{
    public interface IJWTService
    {
        /// <summary>
        /// Get auth code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userNmae"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BaseResponse<string> GetCode(string clientId, string userNmae, string password);

        /// <summary>
        /// Get auth code by Session code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        BaseResponse<string> GetCodeBySessionCode(string clientId, string sessionCode);

    }
}
