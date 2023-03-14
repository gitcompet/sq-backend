using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("hello")]
        public ActionResult<string> hello()
        {
            return Ok("Hello World!");
        }
    }
}

