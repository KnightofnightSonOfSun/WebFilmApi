/*************************************************************************************
 *
 * File name:   GetTokenDto.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/14 21:40
 * ======================================
*************************************************************************************/
using Koknight.Basic.Structure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Feature.Oauth.Models
{
    /// <summary>
    /// Get token response
    /// </summary>
    public class GetTokenResponse : BaseResponse
    {
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// refresh token
        /// </summary>
        public string refreshToken { get; set; }
        /// <summary>
        /// expire time(seconds)
        /// </summary>
        public int expires { get; set; }
        /// <summary>
        /// resource scope
        /// </summary>
        public string scope { get; set; }
    }
}
