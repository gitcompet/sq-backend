using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestComposeController : ControllerBase
    {

        private Business_Logic_Layer.TestComposeBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestCompose _ITestCompose;
        public TestComposeController(Business_Logic_Layer.Interface.InterfaceTestCompose interfaceTestCompose)
        {
            _BLL = new Business_Logic_Layer.TestComposeBLL();
            _ITestCompose = interfaceTestCompose;
        }


        [HttpGet]
        [Route("getTestComposes")]


        public List<TestComposeModel> GetAllTestCompose()
        {
            return _ITestCompose.GetAllTestCompose();
        }



        [HttpGet]
        [Route("getTestCompose")]
        public ActionResult<TestComposeModel> GetTestComposeById(int id)
        {
            var testcompose = _ITestCompose.GetTestComposeById(id);

            if (testcompose == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testcompose);
        }




        [Route("postTestCompose")]
        [HttpPost]
        public void postTestCompose([FromBody] TestComposeModel testcomposeModel)
        {
            _ITestCompose.PostTestCompose(testcomposeModel);
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

