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

namespace SkillQuestionUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuestionUserController : ControllerBase
    {
        private readonly InterfaceQuestionUser _IQuestionUser;
        public QuestionUserController(InterfaceQuestionUser interfaceQuestionUser)
        {
            _IQuestionUser = interfaceQuestionUser;
        }

        //GET api/v1/QuestionUser
        [HttpGet]
        [Route("")]
        public List<QuestionUserModel> GetAllQuestionUser()
        {
            return _IQuestionUser.GetAllQuestionUser();
        }


        //GET api/v1/QuestionUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<IEnumerable<QuestionUserModel>> GetQuestionUserByLinkId(bool? isParentURL, int id)
        {
            if (isParentURL.HasValue && isParentURL.Value)
            {
                var questionUser = _IQuestionUser.GetQuestionUserByLinkId(id);

                if (questionUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(questionUser);
            }
            else
            {
                var questionUser = _IQuestionUser.GetQuestionUserById(id);

                if (questionUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(questionUser);
            }
        }

        //POST api/v1/QuestionUser
        [HttpPost]
        [Route("")]
        public ActionResult<QuestionUserModel> PostQuestionUser([FromBody] QuestionUserModelPostDTO questionUserModelPostDTO)
        {
            if (questionUserModelPostDTO != null)
            {
                var questionUserModel = new QuestionUserModel(questionUserModelPostDTO);
                var questionUserResult = _IQuestionUser.PostQuestionUser(questionUserModel);
                if (questionUserResult != null)
                {
                    return Created("/api/v1/QuestionUser/" + questionUserModel.QuestionUserId, questionUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/QuestionUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuestionUserModel> PatchQuestionUser([FromRoute] int id, [FromBody] JsonPatchDocument<QuestionUser> questionUserModelJSON)
        {
            if (questionUserModelJSON != null)
            {
                var questionUser = _IQuestionUser.PatchQuestionUser(id, questionUserModelJSON);
                return Ok(questionUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/QuestionUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuestionUserModel> PutQuestionUser([FromRoute] int id, [FromBody] QuestionUserModel questionUserModel)
        {
            if (questionUserModel.QuestionUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (questionUserModel != null)
                {
                    var questionUser = _IQuestionUser.PutQuestionUser(questionUserModel);
                    return Ok(questionUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/QuestionUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuestionUserModel> DeleteQuestionUser([FromRoute] int id)
        {
            var questionUser = _IQuestionUser.GetQuestionUserById(id);

            if (questionUser == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IQuestionUser.DeleteQuestionUser(id);
                return Ok(questionUser);
            }

            //_IQuestionUser.DeleteQuestionUser(id);
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

