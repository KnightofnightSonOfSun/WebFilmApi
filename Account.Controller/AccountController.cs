using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Attribute;
using Common.Interface;
using Microsoft.AspNetCore.Mvc;
using WebFilmApi.Account.Model.Response;

namespace WebFilmApi.Account.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [AutoWiredProperty]
        public ILogicManager<SearchTeacherResponse, string> SearchTeacherManager { get; set; }

        [Route("/teacherInfo")]
        [HttpGet]
        public async Task<IActionResult> GetTeacherInfo([FromQuery]string teacherName)
        {
            var response = await SearchTeacherManager.Execute(teacherName);
            return Ok(response);
        }

    }
}
