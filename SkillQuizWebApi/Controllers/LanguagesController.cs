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

        [Route("removeLanguage")]
        [HttpPost]
        public ActionResult<int> RemoveLanguage([FromBody] ResponseIdDTO responseIdDTO)
        {
            var result = _ILanguages.RemoveLanguage(Int32.Parse(responseIdDTO.GenericId));

            if (result == 0)
            {
                return NotFound("Invalid ID");
            }
            return Ok(result);
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

