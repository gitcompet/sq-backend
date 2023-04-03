using Business_Logic_Layer.Interface;
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
    public class AnswerQuestionController : ControllerBase
    {
        private readonly InterfaceAnswerQuestion _IAnswerQuestion;
        public AnswerQuestionController(InterfaceAnswerQuestion interfaceAnswerQuestion)
        {
            _IAnswerQuestion = interfaceAnswerQuestion;
        }

        //GET api/v1/AnswerQuestion
        /*[HttpGet]
        [Route("")]
        public List<AnswerQuestionModel> GetAllAnswerQuestion()
        {
            return _IAnswerQuestion.GetAllAnswerQuestion();
        }
        */

        //GET api/v1/AnswerQuestion/{id}
        /*[HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerQuestionModel> GetAnswerQuestionById(int id)
        {
            var answerQuestion = _IAnswerQuestion.GetAnswerQuestionById(id);

            if (answerQuestion == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answerQuestion);
        }*/
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerQuestionModel> GetAnswerQuestionByQuestionId(int id)
        {
            var answerQuestion = _IAnswerQuestion.GetAnswerQuestionByQuestionId(id);

            if (answerQuestion == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answerQuestion);
        }

        //POST api/v1/AnswerQuestion
        [HttpPost]
        [Route("")]
        public ActionResult<AnswerQuestionModel> PostAnswerQuestion([FromBody] AnswerQuestionModelPostDTO answerQuestionModelPostDTO)
        {
            if (answerQuestionModelPostDTO != null)
            {
                var answerQuestionModel = new AnswerQuestionModel(answerQuestionModelPostDTO);
                var answerQuestionResult = _IAnswerQuestion.PostAnswerQuestion(answerQuestionModel);
                if (answerQuestionResult != null)
                {
                    return Created("/api/v1/AnswerQuestion/" + answerQuestionModel.AnswerQuestionId, answerQuestionResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/AnswerQuestion/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<AnswerQuestionModel> PatchAnswerQuestion([FromRoute] int id, [FromBody] JsonPatchDocument<AnswerQuestion> answerQuestionModelJSON)
        {
            if (answerQuestionModelJSON != null)
            {
                var answerQuestion = _IAnswerQuestion.PatchAnswerQuestion(id, answerQuestionModelJSON);
                return Ok(answerQuestion);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/AnswerQuestion
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<AnswerQuestionModel> PutAnswerQuestion([FromRoute] int id, [FromBody] AnswerQuestionModel answerQuestionModel)
        {
            if (answerQuestionModel.AnswerQuestionId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (answerQuestionModel != null)
            {
                var answerQuestion = _IAnswerQuestion.PutAnswerQuestion(answerQuestionModel);
                return Ok(answerQuestion);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/AnswerQuestion/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<AnswerQuestionModel> DeleteAnswerQuestion([FromRoute] int id)
        {
            var answerQuestion = _IAnswerQuestion.GetAnswerQuestionById(id);

            if (answerQuestion == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IAnswerQuestion.DeleteAnswerQuestion(id);
                return Ok(answerQuestion);
            }

            //_IAnswerQuestion.DeleteAnswerQuestion(id);
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

