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

        [HttpGet("/weather")]
        public string GetWeather()
        {
            return "Test";
        }
    }
}
