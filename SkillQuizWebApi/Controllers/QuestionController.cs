using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {

        private Business_Logic_Layer.QuestionBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceQuestion _IQuestion;
        public QuestionController(Business_Logic_Layer.Interface.InterfaceQuestion interfaceQuestion)
        {
            _BLL = new Business_Logic_Layer.QuestionBLL();
            _IQuestion = interfaceQuestion;
        }


        [HttpGet]
        [Route("getQuestions")]


        public List<QuestionModel> GetAllQuestion()
        {
            return _IQuestion.GetAllQuestion();
        }



        [HttpGet]
        [Route("getQuestion")]
        public ActionResult<QuestionModel> GetQuestionById(int id)
        {
            var question = _IQuestion.GetQuestionById(id);

            if (question == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(question);
        }




        [Route("postQuestion")]
        [HttpPost]
        public void postQuestion([FromBody] QuestionModel questionModel)
        {
            _IQuestion.PostQuestion(questionModel);
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

