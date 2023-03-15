using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElementTranslationController : ControllerBase
    {

        private Business_Logic_Layer.ElementTranslationBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceElementTranslation _IElementTranslation;
        public ElementTranslationController(Business_Logic_Layer.Interface.InterfaceElementTranslation interfaceElementTranslation)
        {
            _BLL = new Business_Logic_Layer.ElementTranslationBLL();
            _IElementTranslation = interfaceElementTranslation;
        }


        [HttpGet]
        [Route("getElementTranslations")]


        public List<ElementTranslationModel> GetAllElementTranslation()
        {
            return _IElementTranslation.GetAllElementTranslation();
        }



        [HttpGet]
        [Route("getElementTranslation")]
        public ActionResult<ElementTranslationModel> GetElementTranslationById(int id)
        {
            var elementtranslation = _IElementTranslation.GetElementTranslationById(id);

            if (elementtranslation == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(elementtranslation);
        }




        [Route("postElementTranslation")]
        [HttpPost]
        public void postElementTranslation([FromBody] ElementTranslationModel elementtranslationModel)
        {
            _IElementTranslation.PostElementTranslation(elementtranslationModel);
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

