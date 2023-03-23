using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionController : ControllerBase
    {

        private Business_Logic_Layer.QuestionBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceQuestion _IQuestion;
        public QuestionController(Business_Logic_Layer.Interface.InterfaceQuestion interfaceQuestion)
        {
            _BLL = new Business_Logic_Layer.QuestionBLL();
            _IQuestion = interfaceQuestion;
        }

        //GET api/v1/Question
        [HttpGet]
        [Route("")]
        public List<QuestionModel> GetAllQuestion()
        {
            return _IQuestion.GetAllQuestion();
        }


        //GET api/v1/Question/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<QuestionModel> GetQuestionById(int id)
        {
            var question = _IQuestion.GetQuestionById(id);

            if (question == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(question);
        }

        //POST api/v1/Question
        [HttpPost]
        [Route("")]
        public void PostQuestion([FromBody] QuestionModel questionModel)
        {
            _IQuestion.PostQuestion(questionModel);
        }

        //PATCH api/v1/Question/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public void PatchQuestion([FromBody] QuestionModel questionModel)
        {
            //_IQuestion.PatchQuestion(questionModel);
        }

        //DELETE api/v1/Question/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public void DeleteQuestion(int id)
        {
           // _IQuestion.DeleteQuestion(id);
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

