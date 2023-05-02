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
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;

namespace SkillTestUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestUserController : ControllerBase
    {
        private readonly InterfaceTestUser _ITestUser;
        private readonly InterfaceQuizUser _IQuizUser;
        private readonly InterfaceQuestionUser _IQuestionUser;
        private readonly InterfaceQuizCompose _IQuizCompose;
        private readonly InterfaceTestCompose _ITestCompose;
        public TestUserController(InterfaceTestUser interfaceTestUser, InterfaceQuizUser interfaceQuizUser, InterfaceQuestionUser interfaceQuestionUser, InterfaceQuizCompose interfaceQuizCompose, InterfaceTestCompose interfaceTestCompose)
        {
            _ITestUser = interfaceTestUser;
            _IQuestionUser = interfaceQuestionUser;
            _ITestCompose = interfaceTestCompose;
            _IQuizCompose = interfaceQuizCompose;
            _IQuizUser = interfaceQuizUser;
        }

        //GET api/v1/TestUser
        [HttpGet]
        [Route("")]
        public List<TestUserModel> GetAllTestUser()
        {
            return _ITestUser.GetAllTestUser();
        }


        //GET api/v1/TestUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<IEnumerable<TestUserModel>> GetTestUserByUserId(bool? isParentURL, bool? isPending, int id)
        {
            if (isParentURL.HasValue && isParentURL.Value)
            {
                var testUser = _ITestUser.GetTestUserByUserId(id, isPending.Value);

                if (testUser == null)
                {
                    return NotFound("Invalid ID or no results");
                }

                return Ok(testUser);
            }
            else
            {
                var testUser = _ITestUser.GetTestUserById(id, isPending.Value);

                if (testUser == null)
                {
                    return NotFound("Invalid ID or no results");
                }

                return Ok(testUser);
            }
        }

        //POST api/v1/TestUser
        [HttpPost]
        [Route("")]
        public ActionResult<TestUserModel> PostTestUser([FromBody] TestUserModelPostDTO testUserModelPostDTO)
        {
            if (testUserModelPostDTO != null)
            {
                var testUserModel = new TestUserModel(testUserModelPostDTO);
                var testUserResult = _ITestUser.PostTestUser(testUserModel);
                if (testUserResult != null)
                {
                    //Trouver les Quizs concernés
                    var testList = _ITestCompose.GetTestComposeByTestId(int.Parse(testUserModelPostDTO.TestId)).ToList();
                    //ajouter les liens dans QuizUser

                    foreach (var quiz in testList)
                    {
                        QuizUserModel quizUserModel = new QuizUserModel();
                        quizUserModel.QuizId = quiz.QuizId;
                        quizUserModel.TestUserId = testUserResult.TestUserId;
                        quizUserModel.IsClosed = false;
                        var quizUserResult = _IQuizUser.PostQuizUser(quizUserModel);
                        //Trouver les Questions concernées
                        var quizList = _IQuizCompose.GetQuizComposeByQuizId(int.Parse(quiz.QuizId));
                        foreach (var question in quizList)
                        {
                            QuestionUserModel questionUserModel = new QuestionUserModel();
                            questionUserModel.QuestionId = question.QuestionId;
                            questionUserModel.QuizUserId = quizUserResult.QuizUserId;
                            _IQuestionUser.PostQuestionUser(questionUserModel);
                            //ajouter les liens dans QuestionUser
                        }
                    }
                        return Created("/api/v1/TestUser/" + testUserModel.TestUserId, testUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> PatchTestUser([FromRoute] int id, [FromBody] JsonPatchDocument<TestUser> testUserModelJSON)
        {
            if (testUserModelJSON != null)
            {
                var testUser = _ITestUser.PatchTestUser(id, testUserModelJSON);
                return Ok(testUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> PutTestUser([FromRoute] int id, [FromBody] TestUserModel testUserModel)
        {
            if (testUserModel.TestUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testUserModel != null)
                {
                    var testUser = _ITestUser.PutTestUser(testUserModel);
                    return Ok(testUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/TestUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> DeleteTestUser([FromRoute] int id)
        {
            var testUser = _ITestUser.GetTestUserById(id, false);

            if (testUser == null)
            {
                return NotFound("Invalid ID or no results");
            }
            else
            {
                var toBeDeletedTestUser = _ITestUser.GetTestUserById(id, false);
                //Trouver les QuizsUser concernés
                var testList = _IQuizUser.GetQuizUserByLinkId(int.Parse(toBeDeletedTestUser.TestUserId)).Value.ToList();
                //supprimer les liens dans QuizUser

                foreach (var quiz in testList)
                {
                    //Trouver les Questions concernées
                    var quizList = _IQuestionUser.GetQuestionUserByLinkId(int.Parse(quiz.QuizUserId)).Value.ToList();
                    //supprimer les liens dans QuestionUser
                    foreach (var question in quizList)
                    {
                        _IQuestionUser.DeleteQuestionUser(int.Parse(question.QuestionUserId));
                    }
                    _IQuizUser.DeleteQuizUser(int.Parse(quiz.QuizUserId));
                }
                _ITestUser.DeleteTestUser(id);
                return Ok(testUser);
            }

            //_ITestUser.DeleteTestUser(id);
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

