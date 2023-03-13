using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestStatusController : ControllerBase
    {

        private Business_Logic_Layer.TestStatusBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestStatus _ITestStatus;
        public TestStatusController(Business_Logic_Layer.Interface.InterfaceTestStatus interfaceTestStatus)
        {
            _BLL = new Business_Logic_Layer.TestStatusBLL();
            _ITestStatus = interfaceTestStatus;
        }


        [HttpGet]
        [Route("getTestStatuss")]


        public List<TestStatusModel> GetAllTestStatus()
        {
            return _ITestStatus.GetAllTestStatus();
        }



        [HttpGet]
        [Route("getTestStatus")]
        public ActionResult<TestStatusModel> GetTestStatusById(int id)
        {
            var teststatus = _ITestStatus.GetTestStatusById(id);

            if (teststatus == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(teststatus);
        }




        [Route("postTestStatus")]
        [HttpPost]
        public void postTestStatus([FromBody] TestStatusModel teststatusModel)
        {
            _ITestStatus.PostTestStatus(teststatusModel);
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

