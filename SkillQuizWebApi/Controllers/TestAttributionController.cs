using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAttributionController : ControllerBase
    {

        private Business_Logic_Layer.TestAttributionBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestAttribution _ITestAttribution;
        public TestAttributionController(Business_Logic_Layer.Interface.InterfaceTestAttribution interfaceTestAttribution)
        {
            _BLL = new Business_Logic_Layer.TestAttributionBLL();
            _ITestAttribution = interfaceTestAttribution;
        }


        [HttpGet]
        [Route("getTestAttributions")]


        public List<TestAttributionModel> GetAllTestAttribution()
        {
            return _ITestAttribution.GetAllTestAttribution();
        }



        [HttpGet]
        [Route("getTestAttribution")]
        public ActionResult<TestAttributionModel> GetTestAttributionById(int id)
        {
            var testattribution = _ITestAttribution.GetTestAttributionById(id);

            if (testattribution == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testattribution);
        }




        [Route("postTestAttribution")]
        [HttpPost]
        public void postTestAttribution([FromBody] TestAttributionModel testattributionModel)
        {
            _ITestAttribution.PostTestAttribution(testattributionModel);
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

