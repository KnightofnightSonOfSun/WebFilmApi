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
using Koknight.Basic.Common.Exceptions;
using Koknight.Basic.Common.Helpers;
using Koknight.Basic.Database.DbContexts;
using Koknight.Basic.Database.Oauth;
using Koknight.Basic.Structure.Models;
using Koknight.Feature.Oauth.Interface;
using Koknight.Feature.Oauth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Koknight.Feature.Oauth.Services
{
    public abstract class JWTBaseService : IJWTService
    {
        protected readonly MysqlDbContext mysqlDbContext;
        protected readonly IOptions<AppSettingOptions> _appSettingOptions;

        public JWTBaseService(IOptions<AppSettingOptions> appSettingOptions, MysqlDbContext mysqlDbContext)
        {
            _appSettingOptions = appSettingOptions;
            this.mysqlDbContext = mysqlDbContext;
        }

        public BaseResponse<string> GetCode(string clientId, string userName, string password)
        {
            var result = new BaseResponse<string>();
            string code = string.Empty;
            var appHSSetting = this.GetAppInfoByAppKey(clientId);
            if (appHSSetting != null)
            {
                throw new NotFoundException("There is no application here.");
            }

            var user = mysqlDbContext.Users.Where(o => o.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                throw new NotFoundException("There is no user here.");
            }

            if (password != user.Password)
            {
                throw new AuthenticationException("Password error!");
            } 

            var userInfo = mysqlDbContext.UserInfos.FirstOrDefault(x => x.Id == user.Id);

            //The authcode, can be replaced by other generating ways.
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCacheKey = $"AuthCodeClientId:{code}";
            Cachehelper.StringSet<UserInfo>(key, userInfo, TimeSpan.FromMinutes(10));
            Cachehelper.StringSet<string>(appCacheKey, appHSSetting.ClientId, TimeSpan.FromMinutes(10));

            string sessionCode = $"SessionCode:{code}";
            SessionCodeUser sessionCodeUser = new SessionCodeUser
            {
                ExpiresTime = DateTime.Now.AddHours(1),
                UserInfo = userInfo,
            };

            Cachehelper.StringSet<UserInfo>(sessionCode, userInfo, TimeSpan.FromDays(1));
            string sessionExpiryKey = $"SessionExpiryKey:{code}";
            DateTime sessionExpirTime = DateTime.Now.AddDays(1);
            Cachehelper.StringSet<DateTime>(sessionExpiryKey, sessionExpirTime, TimeSpan.FromDays(1));
            Console.WriteLine($"Login successfully, session code:{code}");
            Cachehelper.StringSet<DateTime>($"AuthCodeSessionTime:{code}", sessionExpirTime, TimeSpan.FromDays(1));
            result.SetSuccess(code);
            return result;
        }

        public BaseResponse<string> GetCodeBySessionCode(string clientId, string sessionCode)
        {
            var result = new BaseResponse<string>();
            string code = string.Empty;
            AppHSSetting appHSSetting = this.GetAppInfoByAppKey(clientId);
            if (appHSSetting == null)
            {
                throw new NotFoundException("There is no application here.");
            }
            string codeKey = $"SessionCode:{sessionCode}";
            UserInfo currentUserModel = Cachehelper.StringGet<UserInfo>(codeKey);
            if (currentUserModel == null)
            {
                throw new AuthenticationException("Sesson not exists or expired.");
            }

            //generate auth code.
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //Cache auth code.
            Cachehelper.StringSet<UserInfo>(key, currentUserModel, TimeSpan.FromMinutes(10));
            //Cache auth code by app id.
            Cachehelper.StringSet<string>(appCachekey, appHSSetting.ClientId, TimeSpan.FromMinutes(10));

            //Get the valid time for the session code.
            DateTime expirTime = Cachehelper.StringGet<DateTime>($"SessionExpiryKey:{sessionCode}");
            Cachehelper.StringSet<DateTime>($"AuthCodeSessionTime:{code}", expirTime, expirTime - DateTime.Now);

            result.SetSuccess(code);
            return result;
        }

        public BaseResponse<string> GetTokenByRefresh(string refreshToken, string clientId)
        {
            var result = new BaseResponse<string>();

            //Refresh token in cache.
            UserInfo currentUserModel = Cachehelper.StringGet<UserInfo>($"RefreshToken:{refreshToken}");
            if (currentUserModel == null)
            {
                throw new AuthenticationException("No token in cache.");
            }
            //刷新token过期时间
            DateTime refreshTokenExpiry = Cachehelper.StringGet<DateTime>($"RefreshTokenExpiry:{refreshToken}");
            //token默认时间为600s
            double tokenExpiry = 600;
            //如果刷新token的过期时间不到600s了，token过期时间为刷新token的过期时间
            if (refreshTokenExpiry > DateTime.Now && refreshTokenExpiry < DateTime.Now.AddSeconds(600))
            {
                tokenExpiry = (refreshTokenExpiry - DateTime.Now).TotalSeconds;
            }

            //从新生成Token
            string token = IssueToken(currentUserModel, clientId, tokenExpiry);
            return result.SetSuccess(token, "");
        }

        public GetTokenResponse GetTokenWithRefresh(string authCode)
        {
            var result = new GetTokenResponse();

            string key = $"AuthCode:{authCode}";
            string clientIdCachekey = $"AuthCodeClientId:{authCode}";
            string AuthCodeSessionTimeKey = $"AuthCodeSessionTime:{authCode}";

            //Get user info according to authCode
            UserInfo currentUserModel = Cachehelper.StringGet<UserInfo>(key);
            if (currentUserModel == null)
            {
                throw new AuthenticationException("code invalid.");
            }
            //clear auth code.
            Cachehelper.DeleteKey(key);

            //Get application settings.
            string clientId = Cachehelper.StringGet<string>(clientIdCachekey);
            //Refresh token expire time.
            DateTime sessionExpiryTime = Cachehelper.StringGet<DateTime>(AuthCodeSessionTimeKey);
            DateTime tokenExpiryTime = DateTime.Now.AddMinutes(10);//refresh to 10 minutes.
            if (sessionExpiryTime > DateTime.Now && sessionExpiryTime < tokenExpiryTime)
            {
                tokenExpiryTime = sessionExpiryTime;
            }
            //get access token.
            string token = this.IssueToken(currentUserModel, clientId, (tokenExpiryTime - DateTime.Now).TotalSeconds);


            TimeSpan refreshTokenExpiry;
            if (sessionExpiryTime != default(DateTime))
            {
                refreshTokenExpiry = sessionExpiryTime - DateTime.Now;
            }
            else
            {
                refreshTokenExpiry = TimeSpan.FromSeconds(60 * 60 * 24);//默认24小时
            }
            string refreshToken = this.IssueToken(currentUserModel, clientId, refreshTokenExpiry.TotalSeconds);
            Cachehelper.StringSet($"RefreshToken:{refreshToken}", currentUserModel, refreshTokenExpiry);
            Cachehelper.StringSet($"RefreshTokenExpiry:{refreshToken}", DateTime.Now.AddSeconds(refreshTokenExpiry.TotalSeconds), refreshTokenExpiry);
            
            result.token = token;
            result.refreshToken = refreshToken;
            result.expires = 60 * 10;
            result.SetSuccess("Success!");
            Console.WriteLine($"client_id:{clientId}Get token,valid time:{sessionExpiryTime.ToString("yyyy-MM-dd HH:mm:ss")},token:{token}");
            return result;
        }

        protected virtual AppHSSetting GetAppInfoByAppKey(string clientId)
        {
            AppHSSetting appHSSetting = _appSettingOptions.Value.AppHSSettings.Where(s => s.ClientId == clientId).FirstOrDefault();
            return appHSSetting;
        }

        protected abstract SigningCredentials GetCreds(string clientId);

        private string IssueToken(UserInfo userModel, string clientId, double tokenExpiry = 600)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userModel.Name),
                   new Claim("Account", userModel.Account),
                   new Claim("Id", userModel.UserId.ToString()),
                   new Claim("Mobile", userModel.Mobile),
                   new Claim("Email", userModel.Email),
                   new Claim(ClaimTypes.Role,userModel.Role),
            };

            var creds = this.GetCreds(clientId);
            var token = new JwtSecurityToken(
                issuer: "KoknightSSOCenter",
                audience: clientId,
                claims: claims,
                expires: DateTime.Now.AddSeconds(tokenExpiry),
                notBefore: null,
                signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}
