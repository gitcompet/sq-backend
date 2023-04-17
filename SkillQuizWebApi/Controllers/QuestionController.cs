using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Authorization;
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
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    /*[Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "USER"
     )]*/
    [Route("api/v1/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly InterfaceQuestion _IQuestion;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private readonly InterfaceQuizUser _IQuizUser;
        private readonly InterfaceTestUser _ITestUser;
        private readonly InterfaceQuestionUser _IQuestionUser;
        private readonly InterfaceAnswerUser _IAnswerUser;
        private static class TYPE_LABEL
        {
            public const string TITLE = "QUESTION_TITLE";
            public const string LABEL = "QUESTION_LABEL";
        }
        public QuestionController(InterfaceQuestion interfaceQuestion, InterfaceTestUser interfaceTestUser, InterfaceAnswerUser interfaceAnswerUser, InterfaceQuestionUser interfaceQuestionUser, InterfaceQuizUser interfaceQuizUser, InterfaceElementTranslation iElementTranslation)
        {
            _IQuestion = interfaceQuestion;
            _IElementTranslation = iElementTranslation;
            _ITestUser = interfaceTestUser;
            _IQuizUser = interfaceQuizUser;
            _IQuestionUser = interfaceQuestionUser;
            _IAnswerUser = interfaceAnswerUser;
        }

        //GET api/v1/Question
        [HttpGet]
        [Route("")]
        public List<QuestionModelLabel> GetAllQuestion(int? QuizUserId)
        {
            int language = 2;
            if (QuizUserId.HasValue)
            {
                language = _IQuizUser.GetQuizUserById(QuizUserId.Value).LanguageId;
            }
            else
            {
                language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            }
            var collection = _IQuestion.GetAllQuestion();
            List<QuestionModelLabel> result = new List<QuestionModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new QuestionModelLabel(item, _IElementTranslation.GetElementLabelById(item.QuestionId.ToString(), TYPE_LABEL.TITLE, language), _IElementTranslation.GetElementLabelById(item.QuestionId.ToString(), TYPE_LABEL.LABEL, language)));
            }
            return result;
        }


        //GET api/v1/Question/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<QuestionModelLabel> GetQuestionById(int id, int? quizUserId)
        {
            var role = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            if (role.Count() == 1 && role.First().Value == "USER")
            {
                if (quizUserId.HasValue)
                {
                    //check first if the user accessing it is legitimate
                    var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    var testUserId = _IQuizUser.GetQuizUserById(quizUserId.Value).TestUserId; 
                    var legitimateUser = _ITestUser.GetTestUserById(int.Parse(testUserId)).LoginId;

                    if (legitimateUser != userId)
                    {
                        return StatusCode(403, "You aren't the user allowed to access this Quiz");
                    }
                    //User is accessing, we need to check if quiz is open
                    var isClosed = _IQuizUser.GetQuizUserById(quizUserId.Value).IsClosed;
                    if (isClosed)
                    {
                        return StatusCode(403, "The Quiz is closed");
                    }
                    //need to check if answer has already been submited
                    var questionUser = _IQuestionUser.GetQuestionUserByLinkId(quizUserId.Value).Value.FirstOrDefault(q => q.QuestionId == id.ToString());
                    var questionUserId = questionUser.QuestionUserId;
                    var answersUser = _IAnswerUser.GetAnswerUserByLinkId(int.Parse(questionUserId)).Value.ToList();
                    if (answersUser.Any())
                    {
                        return StatusCode(403, "You already answered this Question");
                    }
                    //must start the "timmer" thing IF it is on;
                    var questionDatas = _IQuestion.GetQuestionById(int.Parse(questionUser.QuestionId));
                    if (_IQuizUser.GetQuizUserById(quizUserId.Value).Timer && _IQuestionUser.GetQuestionUserById(int.Parse(questionUserId)).MaxValidationDate == null)
                    {
                        int span = questionDatas.Duration + 1;
                        TimeSpan timmer = new TimeSpan(0, span, 0);
                        DateTime endTimmer = DateTime.Now + timmer;
                        _IQuestionUser.PatchQuestionUserHidden(int.Parse(questionUserId), endTimmer);
                    }
                    //refuse access if overdue;
                    if (_IQuestionUser.GetQuestionUserById(int.Parse(questionUserId)).MaxValidationDate != null && _IQuestionUser.GetQuestionUserById(int.Parse(questionUserId)).MaxValidationDate < DateTime.Now)
                    {
                        return StatusCode(403, "You were out of time for this question");
                    }
                }
                else
                {
                    return StatusCode(403, "You aren't allowed to perform this action");
                }
            }
            int language = 2;
            if (quizUserId.HasValue)
            {
                language = _IQuizUser.GetQuizUserById(quizUserId.Value).LanguageId;
            }
            else
            {
                language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            }
            var question = _IQuestion.GetQuestionById(id);
            if (question == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new QuestionModelLabel(question, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language), _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.LABEL, language));

            return Ok(result);
        }

        //POST api/v1/Question
        [HttpPost]
        [Route("")]
        public ActionResult<QuestionModel> PostQuestion([FromBody] QuestionModelPostDTO questionModelPostDTO)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            if (questionModelPostDTO != null)
            {
                var questionModel = new QuestionModel(questionModelPostDTO);
                var questionResult = _IQuestion.PostQuestion(questionModel);
                if (questionResult != null)
                {
                    var labels = new ElementTranslationModel();

                    labels.Description = questionModelPostDTO.Title;
                    labels.ElementId = int.Parse(questionResult.QuestionId);
                    labels.ElementType = TYPE_LABEL.TITLE;
                    labels.LanguagesId = language;

                    _IElementTranslation.PostElementTranslation(labels);

                    labels.Description = questionModelPostDTO.Label;
                    labels.ElementType = TYPE_LABEL.LABEL;

                    _IElementTranslation.PostElementTranslation(labels);
                    return Created("/api/v1/Question/" + questionModel.QuestionId, questionResult);
                }
            }
            return BadRequest(ModelState);
        }
        
        //PATCH api/v1/Question/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuestionModel> PatchQuestion([FromRoute] int id, [FromBody] JsonPatchDocument<QuestionModelLabel> questionModelLabelJSON)
        {
            JsonPatchDocument<Question> questionJSONTemplate = new JsonPatchDocument<Question>();
            JsonPatchDocument<ElementTranslation> elementTranslationJSONTemplate = new JsonPatchDocument<ElementTranslation>();

            var operations = questionModelLabelJSON.Operations;
            var labelOperations = new List<Operation<QuestionModelLabel>>();
            labelOperations.Add(operations.Where(x => x.path == "/title").ToList().First());
            labelOperations.Add(operations.Where(x => x.path == "/label").ToList().First());

            foreach (var oper in labelOperations)
            {
                operations.Remove(oper);
            }

            var modelOperations = questionJSONTemplate.Operations;

            foreach ( var operation in operations )
            {
                modelOperations.Add(new Operation<Question>(operation.op, operation.path, operation.from, operation.value));
            }

            JsonPatchDocument<Question> modelJSONOperations = new JsonPatchDocument<Question>(modelOperations, new DefaultContractResolver());
            
            if (modelJSONOperations != null)
            {
                var question = _IQuestion.PatchQuestion(id, modelJSONOperations);

                //update the title and labels
                var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);

                var modelOperationsLabel = elementTranslationJSONTemplate.Operations;

                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.First().op, "/description", labelOperations.First().from, labelOperations.First().value));
                
                JsonPatchDocument<ElementTranslation> modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                int elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(question.QuestionId), TYPE_LABEL.TITLE, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);
                
                modelOperationsLabel = elementTranslationJSONTemplate.Operations;

                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.Last().op, "/description", labelOperations.Last().from, labelOperations.Last().value));
                
                modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(question.QuestionId), TYPE_LABEL.LABEL, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);
                
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
        public ActionResult<QuestionModel> PutQuestion([FromRoute] int id, [FromBody] QuestionModel questionModel)
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
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(question.QuestionId), TYPE_LABEL.TITLE);
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(question.QuestionId), TYPE_LABEL.LABEL);
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

