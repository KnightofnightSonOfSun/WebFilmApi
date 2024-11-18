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
using Koknight.Feature.Oauth.Models;

namespace Koknight.Feature.Oauth.Interface
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
        BaseResponse<string> GetCode(string clientId, string userName, string password);

        /// <summary>
        /// Get auth code by Session code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        BaseResponse<string> GetCodeBySessionCode(string clientId, string sessionCode);

        /// <summary>
        /// Refresh token by auth code.
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        GetTokenResponse GetTokenWithRefresh(string authCode);

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        BaseResponse<string> GetTokenByRefresh(string refreshToken, string clientId);
    }
}
