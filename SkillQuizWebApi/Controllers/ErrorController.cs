using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        public ErrorController()
        {
            
        }

        [HttpGet]
        [Route("getError")]
        public ActionResult<string> ErrorHandler()
        {
            var errorText = "OOps something went wrong";

            return BadRequest(errorText);
        }
    }
}

