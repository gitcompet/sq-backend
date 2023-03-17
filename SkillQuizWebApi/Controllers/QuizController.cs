using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Nodes;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuizController : ControllerBase
    {

        private Business_Logic_Layer.QuizBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceQuiz _IQuiz;
        public QuizController(Business_Logic_Layer.Interface.InterfaceQuiz interfaceQuiz)
        {
            _BLL = new Business_Logic_Layer.QuizBLL();
            _IQuiz = interfaceQuiz;
        }

        //GET api/v1/Quiz
        [HttpGet]
        [Route("")]
        public List<QuizModel> GetAllQuiz()
        {
            return _IQuiz.GetAllQuiz();
        }


        //GET api/v1/Quiz/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<QuizModel> GetQuizById(int id)
        {
            var quiz = _IQuiz.GetQuizById(id);

            if (quiz == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(quiz);
        }

        //POST api/v1/Quiz
        [HttpPost]
        [Route("")]
        public ActionResult<QuizModel> PostQuiz([FromBody] QuizModelPostDTO quizModelPostDTO)
        {
            if (quizModelPostDTO != null)
            {
                var quizModel = new QuizModel(quizModelPostDTO);
                var quizResult = _IQuiz.PostQuiz(quizModel);
                if (quizResult != null)
                {
                    return Created("/api/v1/Quiz/" + quizModel.QuizId, quizResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Quiz/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuizModel> PatchQuiz([FromRoute] int id, [FromBody] JsonPatchDocument<Quiz> quizModelJSON)
        {
            if (quizModelJSON != null)
            {
                var quiz = _IQuiz.PatchQuiz(id, quizModelJSON);
                return Ok(quiz);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Quiz
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuizModel> PatchQuiz([FromRoute] int id, [FromBody] QuizModel quizModel)
        {
            if (quizModel.QuizId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (quizModel != null)
                {
                    var quiz = _IQuiz.PutQuiz(quizModel);
                    return Ok(quiz);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/Quiz/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuizModel> DeleteQuiz([FromRoute] int id)
        {
            var quiz = _IQuiz.GetQuizById(id);

            if (quiz == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IQuiz.DeleteQuiz(id);
                return Ok(quiz);
            }

            //_IQuiz.DeleteQuiz(id);
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

