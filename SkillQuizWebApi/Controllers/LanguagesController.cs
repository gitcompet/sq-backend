using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguagesController : ControllerBase
    {

        private Business_Logic_Layer.LanguagesBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceLanguages _ILanguages;
        public LanguagesController(Business_Logic_Layer.Interface.InterfaceLanguages interfaceLanguages)
        {
            _BLL = new Business_Logic_Layer.LanguagesBLL();
            _ILanguages = interfaceLanguages;
        }


        [HttpGet]
        [Route("getLanguagess")]


        public List<LanguagesModel> GetAllLanguages()
        {
            return _ILanguages.GetAllLanguages();
        }



        [HttpGet]
        [Route("getLanguages")]
        public ActionResult<LanguagesModel> GetLanguagesById(int id)
        {
            var languages = _ILanguages.GetLanguagesById(id);

            if (languages == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(languages);
        }




        [Route("postLanguages")]
        [HttpPost]
        public int postLanguages([FromBody] LanguagesModelDTO languagesModelDTO)
        {
            return _ILanguages.PostLanguages(languagesModelDTO);
        }



        //(This is the bad practise!) = > this should instead also call the BLL 
        //[Route("deletePerson")]
        //[HttpDelete]
        //public void deletePerson(int id)
        //{
        //    var db = new PersonDbContext();
        //    Person p = new Person();
        //    p = db.Person.FirstOrDefault(x => x.Id == id);

        //    if (p == null)
        //        throw new Exception("Not found");

        //    db.Person.Remove(p);
        //    db.SaveChanges();
        //}


    }
}

