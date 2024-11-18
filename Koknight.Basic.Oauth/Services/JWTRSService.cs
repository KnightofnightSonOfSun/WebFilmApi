/*************************************************************************************
 *
 * File name:   JWTRSService.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 14:16
 * ======================================
*************************************************************************************/
using Koknight.Basic.Database.DbContexts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Koknight.Feature.Oauth.Services
{
    public class JWTRSService : JWTBaseService
    {
        public JWTRSService(IOptions<AppSettingOptions> appSettingOptions, MysqlDbContext mysqlDbContext) : base(appSettingOptions, mysqlDbContext)
        {
        }

        protected override SigningCredentials GetCreds(string clientId)
        {
            var appRSSetting = this.GetAppInfoByAppKey(clientId);
            var rsa = RSA.Create();
            byte[] privateKey = Convert.FromBase64String(appRSSetting.PrivateKey);//这里只需要私钥，不要begin,不要end
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            var key = new RsaSecurityKey(rsa);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            return creds;
        }

        protected new AppRSSetting GetAppInfoByAppKey(string clientId)
        {
            AppRSSetting appRSSetting = _appSettingOptions.Value.AppRSSettings.Where(s => s.ClientId == clientId).FirstOrDefault();
            return appRSSetting;
        }
    }
}
