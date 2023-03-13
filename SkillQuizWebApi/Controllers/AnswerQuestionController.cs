using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerQuestionController : ControllerBase
    {

        private Business_Logic_Layer.AnswerQuestionBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceAnswerQuestion _IAnswerQuestion;
        public AnswerQuestionController(Business_Logic_Layer.Interface.InterfaceAnswerQuestion interfaceAnswerQuestion)
        {
            _BLL = new Business_Logic_Layer.AnswerQuestionBLL();
            _IAnswerQuestion = interfaceAnswerQuestion;
        }


        [HttpGet]
        [Route("getAnswerQuestions")]


        public List<AnswerQuestionModel> GetAllAnswerQuestion()
        {
            return _IAnswerQuestion.GetAllAnswerQuestion();
        }



        [HttpGet]
        [Route("getAnswerQuestion")]
        public ActionResult<AnswerQuestionModel> GetAnswerQuestionById(int id)
        {
            var answerquestion = _IAnswerQuestion.GetAnswerQuestionById(id);

            if (answerquestion == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answerquestion);
        }




        [Route("postAnswerQuestion")]
        [HttpPost]
        public void postAnswerQuestion([FromBody] AnswerQuestionModel answerquestionModel)
        {
            _IAnswerQuestion.PostAnswerQuestion(answerquestionModel);
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

