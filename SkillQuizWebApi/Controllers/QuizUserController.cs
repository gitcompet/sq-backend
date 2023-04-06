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

namespace SkillQuizUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuizUserController : ControllerBase
    {
        private readonly InterfaceQuizUser _IQuizUser;
        public QuizUserController(InterfaceQuizUser interfaceQuizUser)
        {
            _IQuizUser = interfaceQuizUser;
        }

        //GET api/v1/QuizUser
        [HttpGet]
        [Route("")]
        public List<QuizUserModel> GetAllQuizUser()
        {
            return _IQuizUser.GetAllQuizUser();
        }


        //GET api/v1/QuizUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<IEnumerable<QuizUserModel>> GetQuizUserByLinkId(bool? isParentURL, int id)
        {
            if (isParentURL.HasValue && isParentURL.Value)
            {
                var quizUser = _IQuizUser.GetQuizUserByLinkId(id);

                if (quizUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(quizUser);
            }
            else
            {
                var quizUser = _IQuizUser.GetQuizUserById(id);

                if (quizUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(quizUser);
            }
        }

        //POST api/v1/QuizUser
        [HttpPost]
        [Route("")]
        public ActionResult<QuizUserModel> PostQuizUser([FromBody] QuizUserModelPostDTO quizUserModelPostDTO)
        {
            if (quizUserModelPostDTO != null)
            {
                var quizUserModel = new QuizUserModel(quizUserModelPostDTO);
                var quizUserResult = _IQuizUser.PostQuizUser(quizUserModel);
                if (quizUserResult != null)
                {
                    return Created("/api/v1/QuizUser/" + quizUserModel.QuizUserId, quizUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/QuizUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuizUserModel> PatchQuizUser([FromRoute] int id, [FromBody] JsonPatchDocument<QuizUser> quizUserModelJSON)
        {
            if (quizUserModelJSON != null)
            {
                var quizUser = _IQuizUser.PatchQuizUser(id, quizUserModelJSON);
                return Ok(quizUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/QuizUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuizUserModel> PutQuizUser([FromRoute] int id, [FromBody] QuizUserModel quizUserModel)
        {
            if (quizUserModel.QuizUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (quizUserModel != null)
                {
                    var quizUser = _IQuizUser.PutQuizUser(quizUserModel);
                    return Ok(quizUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/QuizUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuizUserModel> DeleteQuizUser([FromRoute] int id)
        {
            var quizUser = _IQuizUser.GetQuizUserById(id);

            if (quizUser == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IQuizUser.DeleteQuizUser(id);
                return Ok(quizUser);
            }

            //_IQuizUser.DeleteQuizUser(id);
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

