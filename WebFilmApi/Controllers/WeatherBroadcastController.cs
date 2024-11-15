using Koknight.Basic.Database.DbContexts;
using Koknight.Basic.Database.Oauth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var data = mysqlDbContext.Users.ToList();
            mysqlDbContext.Users.Add(new User
            {
                UserName = "testAdmin",
                Password = "testAdmin"
            });

            mysqlDbContext.SaveChanges();
            return "Test";
        }
    }
}
