/*************************************************************************************
 *
 * File name:   GetTokenRequest.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 14:36
 * ======================================
*************************************************************************************/
using Koknight.Basic.Structure.Models;

namespace Koknight.Feature.Oauth.Models
{
    public class GetTokenRequest : BaseRequest
    {
        public string AuthCode { get; set; }
    }
}
