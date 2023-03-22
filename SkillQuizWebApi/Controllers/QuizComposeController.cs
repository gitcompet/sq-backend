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
    public class QuizComposeController : ControllerBase
    {
        private readonly InterfaceQuizCompose _IQuizCompose;
        public QuizComposeController(InterfaceQuizCompose interfaceQuizCompose)
        {
            _IQuizCompose = interfaceQuizCompose;
        }

        //GET api/v1/QuizCompose
        [HttpGet]
        [Route("")]
        public List<QuizComposeModel> GetAllQuizCompose()
        {
            return _IQuizCompose.GetAllQuizCompose();
        }


        //GET api/v1/QuizCompose/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<QuizComposeModel> GetQuizComposeById(int id)
        {
            var quizCompose = _IQuizCompose.GetQuizComposeById(id);

            if (quizCompose == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(quizCompose);
        }

        //POST api/v1/QuizCompose
        [HttpPost]
        [Route("")]
        public ActionResult<QuizComposeModel> PostQuizCompose([FromBody] QuizComposeModelPostDTO quizComposeModelPostDTO)
        {
            if (quizComposeModelPostDTO != null)
            {
                var quizComposeModel = new QuizComposeModel(quizComposeModelPostDTO);
                var quizComposeResult = _IQuizCompose.PostQuizCompose(quizComposeModel);
                if (quizComposeResult != null)
                {
                    return Created("/api/v1/QuizCompose/" + quizComposeModel.QuizComposeId, quizComposeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/QuizCompose/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuizComposeModel> PatchQuizCompose([FromRoute] int id, [FromBody] JsonPatchDocument<QuizCompose> quizComposeModelJSON)
        {
            if (quizComposeModelJSON != null)
            {
                var quizCompose = _IQuizCompose.PatchQuizCompose(id, quizComposeModelJSON);
                return Ok(quizCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/QuizCompose
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuizComposeModel> PatchQuizCompose([FromRoute] int id, [FromBody] QuizComposeModel quizComposeModel)
        {
            if (quizComposeModel.QuizComposeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (quizComposeModel != null)
            {
                var quizCompose = _IQuizCompose.PutQuizCompose(quizComposeModel);
                return Ok(quizCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/QuizCompose/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuizComposeModel> DeleteQuizCompose([FromRoute] int id)
        {
            var quizCompose = _IQuizCompose.GetQuizComposeById(id);

            if (quizCompose == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IQuizCompose.DeleteQuizCompose(id);
                return Ok(quizCompose);
            }

            //_IQuizCompose.DeleteQuizCompose(id);
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

