using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private Business_Logic_Layer.PersonBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfacePerson _IPerson;
        public PersonController(Business_Logic_Layer.Interface.InterfacePerson interfacePerson)
        {
            _BLL = new Business_Logic_Layer.PersonBLL();
            _IPerson = interfacePerson;
        }
     
       
        [HttpGet]
        [Route("getPersons")]
      
       
        public List<PersonModel> GetAllPersons()
        {
            return _BLL.GetAllPersons();
        }



        [HttpGet]
        [Route("getPerson")]
        public ActionResult<PersonModel> GetPersonById(int id)
        {
            var person= _IPerson.GetPersonById(id);

            if (person == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(person);
        }


 
    
        [Route("postPerson")]
        [HttpPost]
        public void postPerson([FromBody] PersonModel personModel)
        {
            _IPerson.PostPerson(personModel);
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
