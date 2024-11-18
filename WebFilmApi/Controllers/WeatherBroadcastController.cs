using Koknight.Basic.Common.Exceptions;
using Koknight.Basic.Database.DbContexts;
using Koknight.Basic.Database.Oauth;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebFilmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherBroadcastController : ControllerBase
    {
        private readonly MysqlDbContext mysqlDbContext;
        public WeatherBroadcastController(MysqlDbContext dbContext)
        {
            mysqlDbContext = dbContext;
        }

        [HttpGet("/weather")]
        public string GetWeather()
        {
            var data = mysqlDbContext.Users.FirstOrDefault(x => x.Id ==1);
            var info = new UserInfo
            {
                UserId = 1,
                Account = "test",
                Name = "test",
                Email = "test@koknight.com",
                Mobile = "1888888888",
                Role = "admin",
            };

            mysqlDbContext.UserInfos.Add(info);

            mysqlDbContext.SaveChanges();
            return "Test";
        }

        [HttpGet("/testException")]
        public string TestException()
        {
            throw new System.Exception();
        }

    }
}
