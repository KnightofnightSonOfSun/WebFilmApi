/*************************************************************************************
 *
 * File name:   JWTBaseService.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/14 21:43
 * ======================================
*************************************************************************************/
using Koknight.Basic.Common.Helpers;
using Koknight.Basic.Structure.Models;
using Koknight.Feature.Oauth.Interface;
using Koknight.Feature.Oauth.Models.Response;
using Microsoft.Extensions.Options;

namespace Koknight.Feature.Oauth.Services
{
    public class JWTBaseService : IJWTService
    {
        protected readonly IOptions<AppSettingOptions> _appSettingOptions;
        public JWTBaseService(IOptions<AppSettingOptions> appSettingOptions)
        {
            _appSettingOptions = appSettingOptions;
        }

        public BaseResponse<string> GetCode(string clientId, string userNmae, string password)
        {
            var result = new BaseResponse<string>();
            var appHSSetting = _appSettingOptions.Value.appHSSettings.Where(o => o.clientId == clientId).FirstOrDefault();
            if (appHSSetting != null)
            {
                result.SetFail("There is no application here.");
            }

            return result;
        }

        public BaseResponse<string> GetCodeBySessionCode(string clientId, string sessionCode)
        {
            throw new NotImplementedException();
        }

        public string GetTokenByRefresh(string refreshToken, string clientId)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetTokenDto> GetTokenWithRefresh(string authCode)
        {
            throw new NotImplementedException();
        }
    }
}
