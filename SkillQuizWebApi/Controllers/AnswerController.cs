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
    public class AnswerController : ControllerBase
    {
        private readonly InterfaceAnswer _IAnswer;
        public AnswerController(InterfaceAnswer interfaceAnswer)
        {
            _IAnswer = interfaceAnswer;
        }
        /*
        //GET api/v1/Answer
        [HttpGet]
        [Route("")]
        public List<AnswerModel> GetAllAnswer()
        {
            return _IAnswer.GetAllAnswer();
        }


        //GET api/v1/Answer/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerModel> GetAnswerById(int id)
        {
            var answer = _IAnswer.GetAnswerById(id);

            if (answer == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answer);
        }
        
        //PATCH api/v1/Answer/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<AnswerModel> PatchAnswer([FromRoute] int id, [FromBody] JsonPatchDocument<Answer> answerModelJSON)
        {
            if (answerModelJSON != null)
            {
                var answer = _IAnswer.PatchAnswer(id, answerModelJSON);
                return Ok(answer);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Answer
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<AnswerModel> PatchAnswer([FromRoute] int id, [FromBody] AnswerModel answerModel)
        {
            if (answerModel.AnswerId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (answerModel != null)
            {
                var answer = _IAnswer.PutAnswer(answerModel);
                return Ok(answer);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/Answer/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<AnswerModel> DeleteAnswer([FromRoute] int id)
        {
            var answer = _IAnswer.GetAnswerById(id);

            if (answer == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IAnswer.DeleteAnswer(id);
                return Ok(answer);
            }

            //_IAnswer.DeleteAnswer(id);
        }
        */

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

