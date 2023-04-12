using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace SkillAnswerUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnswerUserController : ControllerBase
    {
        private readonly InterfaceAnswerUser _IAnswerUser;
        private readonly InterfaceQuestionUser _IQuestionUser;
        private readonly InterfaceQuizUser _IQuizUser;
        private readonly InterfaceTestUser _ITestUser;
        public AnswerUserController(InterfaceAnswerUser interfaceAnswerUser, InterfaceQuizUser interfaceQuizUser, InterfaceTestUser interfaceTestUser, InterfaceQuestionUser interfaceQuestionUser)
        {
            _IAnswerUser = interfaceAnswerUser;
            _IQuestionUser = interfaceQuestionUser;
            _ITestUser = interfaceTestUser;
            _IQuizUser = interfaceQuizUser;
        }

        //GET api/v1/AnswerUser
        [HttpGet]
        [Route("")]
        public List<AnswerUserModel> GetAllAnswerUser()
        {
            return _IAnswerUser.GetAllAnswerUser();
        }
        //GET api/v1/AnswerUser/Empty{quizId}
        [HttpGet]
        [Route("Remaining/{quizId:int}")]
        public List<int> GetAllAnswerUser(int quizId)
        {
            var result = new List<int>();
            var questions = _IQuestionUser.GetQuestionUserByLinkId(quizId).Value.Select(x => x.QuestionUserId).ToList();
            
            foreach (var item in questions)
            {
                var temp = _IAnswerUser.GetAnswerUserByLinkId(int.Parse(item)).Value.ToList();
                if (!temp.Any())
                {
                    //IF overdue don't add it
                    var dueDate = _IQuestionUser.GetQuestionUserById(int.Parse(item)).MaxValidationDate;
                    if (!dueDate.HasValue || dueDate.Value > DateTime.Now)
                    {
                        result.Add(int.Parse(item));
                    }
                }
            }
            return result;
        }

        //GET api/v1/AnswerUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> GetAnswerUserByLinkId(bool? isParentURL, int id)
        {
            if (isParentURL.HasValue && isParentURL.Value)
            {
                var answerUser = _IAnswerUser.GetAnswerUserByLinkId(id);

                if (answerUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(answerUser);
            }
            else
            {
                var answerUser = _IAnswerUser.GetAnswerUserById(id);

                if (answerUser == null)
                {
                    return NotFound("Invalid ID");
                }

                return Ok(answerUser);
            }
        }

        //POST api/v1/AnswerUser
        [HttpPost]
        [Route("")]
        public ActionResult<AnswerUserModel> PostAnswerUser([FromBody] AnswerUserModelPostDTO answerUserModelPostDTO)
        {
            if (answerUserModelPostDTO != null)
            {
                //check first if the user accessing it is legitimate
                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var quizUserId = _IQuestionUser.GetQuestionUserById(int.Parse(answerUserModelPostDTO.QuestionUserId)).QuizUserId;
                var testUserId = _IQuizUser.GetQuizUserById(int.Parse(quizUserId)).TestUserId;
                var legitimateUser = _ITestUser.GetTestUserById(int.Parse(testUserId)).LoginId;

                if (legitimateUser != userId)
                {
                    return StatusCode(403, "You aren't the user allowed to access this Quiz");
                }

                var answerUserModel = new AnswerUserModel(answerUserModelPostDTO);
                //Déjà répondu?
                var isEmpty = _IAnswerUser.GetAnswerUserByLinkId(int.Parse(answerUserModel.QuestionUserId)).Value.ToList();
                if (isEmpty.Any())
                {
                    return StatusCode(403, "You already answered this Question");
                }
                //Timer dépassé
                if (_IQuestionUser.GetQuestionUserById(int.Parse(answerUserModel.QuestionUserId)).MaxValidationDate < DateTime.Now)
                {
                    return StatusCode(403, "You were out of time for this submition");
                }
                var answerUserResult = _IAnswerUser.PostAnswerUser(answerUserModel);
                if (answerUserResult != null)
                {
                    return Created("/api/v1/AnswerUser/" + answerUserModel.AnswerUserId, answerUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/AnswerUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> PatchAnswerUser([FromRoute] int id, [FromBody] JsonPatchDocument<AnswerUser> answerUserModelJSON)
        {
            if (answerUserModelJSON != null)
            {
                var answerUser = _IAnswerUser.PatchAnswerUser(id, answerUserModelJSON);
                return Ok(answerUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/AnswerUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> PutAnswerUser([FromRoute] int id, [FromBody] AnswerUserModel answerUserModel)
        {
            if (answerUserModel.AnswerUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (answerUserModel != null)
                {
                    var answerUser = _IAnswerUser.PutAnswerUser(answerUserModel);
                    return Ok(answerUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/AnswerUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> DeleteAnswerUser([FromRoute] int id)
        {
            var answerUser = _IAnswerUser.GetAnswerUserById(id);

            if (answerUser == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IAnswerUser.DeleteAnswerUser(id);
                return Ok(answerUser);
            }

            //_IAnswerUser.DeleteAnswerUser(id);
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

