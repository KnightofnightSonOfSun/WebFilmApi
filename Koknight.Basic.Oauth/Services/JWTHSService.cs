/*************************************************************************************
 *
 * File name:   JWTHSService.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/16 14:12
 * ======================================
*************************************************************************************/
using Koknight.Basic.Database.DbContexts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koknight.Feature.Oauth.Services
{
    public class JWTHSService : JWTBaseService
    {
        public JWTHSService(IOptions<AppSettingOptions> appSettingOptions, MysqlDbContext mysqlDbContext) : base(appSettingOptions, mysqlDbContext)
        {
        }

        protected override SigningCredentials GetCreds(string clientId)
        {
            var appHSSettings = this.GetAppInfoByAppKey(clientId);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appHSSettings.ClientSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return creds;
        }
    }
}
