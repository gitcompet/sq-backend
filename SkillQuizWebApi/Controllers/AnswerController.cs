using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {

        private Business_Logic_Layer.AnswerBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceAnswer _IAnswer;
        public AnswerController(Business_Logic_Layer.Interface.InterfaceAnswer interfaceAnswer)
        {
            _BLL = new Business_Logic_Layer.AnswerBLL();
            _IAnswer = interfaceAnswer;
        }


        [HttpGet]
        [Route("getAnswers")]


        public List<AnswerModel> GetAllAnswer()
        {
            return _IAnswer.GetAllAnswer();
        }



        [HttpGet]
        [Route("getAnswer")]
        public ActionResult<AnswerModel> GetAnswerById(int id)
        {
            var answer = _IAnswer.GetAnswerById(id);

            if (answer == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answer);
        }




        [Route("postAnswer")]
        [HttpPost]
        public void postAnswer([FromBody] AnswerModel answerModel)
        {
            _IAnswer.PostAnswer(answerModel);
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

