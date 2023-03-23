using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {

        private Business_Logic_Layer.LanguagesBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceLanguages _ILanguages;
        public LanguagesController(Business_Logic_Layer.Interface.InterfaceLanguages interfaceLanguages)
        {
            _BLL = new Business_Logic_Layer.LanguagesBLL();
            _ILanguages = interfaceLanguages;
        }

        //DEBUT DE TON CODE//

        //GET ONE
        //GET api/Languages/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<LanguagesModel> GetLanguagesById(int id)
        {
            //VOTRE CODE A METTRE
            return Ok(ModelState);
        }
        
        //GET ALL
        //GET api/Languages
        

    }
}

