﻿using Business_Logic_Layer.Models;
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
    public class QuestionController : ControllerBase
    {

        private Business_Logic_Layer.QuestionBLL _BLL;
        private Business_Logic_Layer.AnswerBLL _BLL2;
        private readonly Business_Logic_Layer.Interface.InterfaceQuestion _IQuestion;
        public QuestionController(Business_Logic_Layer.Interface.InterfaceQuestion interfaceQuestion)
        {
            _BLL = new Business_Logic_Layer.QuestionBLL();
            _BLL2 = new Business_Logic_Layer.AnswerBLL();
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

            _BLL2.PostAnswer(id);
            //private readonly Business_Logic_Layer.Interface.InterfaceAnswer _IAnswer;

            return Ok(question);
        }

        //POST api/v1/Question
        [HttpPost]
        [Route("")]
        public ActionResult<QuestionModel> PostQuestion([FromBody] QuestionModelPostDTO questionModelPostDTO)
        {
            if (questionModelPostDTO != null)
            {
                var questionModel = new QuestionModel(questionModelPostDTO);
                var questionResult = _IQuestion.PostQuestion(questionModel);
                if (questionResult != null)
                {
                    return Created("/api/v1/Question/" + questionModel.QuestionId, questionResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Question/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuestionModel> PatchQuestion([FromRoute] int id, [FromBody] JsonPatchDocument<Question> questionModelJSON)
        {
            if (questionModelJSON != null)
            {
                var question = _IQuestion.PatchQuestion(id, questionModelJSON);
                return Ok(question);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Question
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuestionModel> PatchQuestion([FromRoute] int id, [FromBody] QuestionModel questionModel)
        {
            if (questionModel.QuestionId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (questionModel != null)
                {
                    var question = _IQuestion.PutQuestion(questionModel);
                    return Ok(question);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/Question/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuestionModel> DeleteQuestion([FromRoute] int id)
        {
            var question = _IQuestion.GetQuestionById(id);

            if (question == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IQuestion.DeleteQuestion(id);
                return Ok(question);
            }

            //_IQuestion.DeleteQuestion(id);
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

