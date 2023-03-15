using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerCandidateTestController : ControllerBase
    {

        private Business_Logic_Layer.AnswerCandidateTestBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceAnswerCandidateTest _IAnswerCandidateTest;
        public AnswerCandidateTestController(Business_Logic_Layer.Interface.InterfaceAnswerCandidateTest interfaceAnswerCandidateTest)
        {
            _BLL = new Business_Logic_Layer.AnswerCandidateTestBLL();
            _IAnswerCandidateTest = interfaceAnswerCandidateTest;
        }


        [HttpGet]
        [Route("getAnswerCandidateTests")]


        public List<AnswerCandidateTestModel> GetAllAnswerCandidateTest()
        {
            return _IAnswerCandidateTest.GetAllAnswerCandidateTest();
        }



        [HttpGet]
        [Route("getAnswerCandidateTest")]
        public ActionResult<AnswerCandidateTestModel> GetAnswerCandidateTestById(int id)
        {
            var answercandidatetest = _IAnswerCandidateTest.GetAnswerCandidateTestById(id);

            if (answercandidatetest == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answercandidatetest);
        }




        [Route("postAnswerCandidateTest")]
        [HttpPost]
        public void postAnswerCandidateTest([FromBody] AnswerCandidateTestModel answercandidatetestModel)
        {
            _IAnswerCandidateTest.PostAnswerCandidateTest(answercandidatetestModel);
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

