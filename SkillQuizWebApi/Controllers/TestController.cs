using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private Business_Logic_Layer.TestBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTest _ITest;
        public TestController(Business_Logic_Layer.Interface.InterfaceTest interfaceTest)
        {
            _BLL = new Business_Logic_Layer.TestBLL();
            _ITest = interfaceTest;
        }


        [HttpGet]
        [Route("getTests")]


        public List<TestModel> GetAllTest()
        {
            return _ITest.GetAllTest();
        }



        [HttpGet]
        [Route("getTest")]
        public ActionResult<TestModel> GetTestById(int id)
        {
            var test = _ITest.GetTestById(id);

            if (test == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(test);
        }




        [Route("postTest")]
        [HttpPost]
        public void postTest([FromBody] TestModel testModel)
        {
            _ITest.PostTest(testModel);
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