using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
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

namespace SkillAnswerzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "USER"
     )]
    public class AnswerController : ControllerBase
    {
        private readonly InterfaceAnswer _IAnswer;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private readonly InterfaceQuizUser _IQuizUser;
        private static class TYPE_LABEL
        {
            public const string TITLE = "ANSWER_TITLE";
            public const string LABEL = "ANSWER_LABEL";
        }
        public AnswerController(InterfaceAnswer interfaceAnswer, InterfaceQuizUser interfaceQuizUser, InterfaceElementTranslation iElementTranslation)
        {
            _IAnswer = interfaceAnswer;
            _IElementTranslation = iElementTranslation;
            _IQuizUser = interfaceQuizUser;
        }
        
        //GET api/v1/Answer
        [HttpGet]
        [Route("")]
        public List<AnswerModelLabel> GetAllAnswer(int? QuizUserId)
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
                var collection = _IAnswer.GetAllAnswer();
                List<AnswerModelLabel> result = new List<AnswerModelLabel>();
                foreach (var item in collection)
                {
                    result.Add(new AnswerModelLabel(item, _IElementTranslation.GetElementLabelById(item.AnswerId.ToString(), TYPE_LABEL.TITLE, language), _IElementTranslation.GetElementLabelById(item.AnswerId.ToString(), TYPE_LABEL.LABEL, language)));
                }
                return result;
        }


        //GET api/v1/Answer/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerModelLabel> GetAnswerById(int id, int? QuizUserId)
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
            var answer = _IAnswer.GetAnswerById(id);
            if (answer == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new AnswerModelLabel(answer, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language), _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.LABEL, language));

            return Ok(result);
        }

        //POST api/v1/Answer
        [HttpPost]
        [Route("")]
        public ActionResult<AnswerModel> PostAnswer([FromBody] AnswerModelPostDTO answerModelPostDTO)
        {
            if (answerModelPostDTO != null)
            {
                var answerModel = new AnswerModel(answerModelPostDTO);
                var answerResult = _IAnswer.PostAnswer(answerModel);
                if (answerResult != null)
                {
                    return Created("/api/v1/Answer/" + answerModel.AnswerId, answerResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Answer/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<AnswerModel> PatchAnswer([FromRoute] int id, [FromBody] JsonPatchDocument<AnswerModelLabel> answerModelLabelJSON)
        {
            JsonPatchDocument<Answer> answerJSONTemplate = new JsonPatchDocument<Answer>();
            JsonPatchDocument<ElementTranslation> elementTranslationJSONTemplate = new JsonPatchDocument<ElementTranslation>();

            var operations = answerModelLabelJSON.Operations;
            var labelOperations = new List<Operation<AnswerModelLabel>>();
            labelOperations.Add(operations.Where(x => x.path == "/title").ToList().First());
            labelOperations.Add(operations.Where(x => x.path == "/label").ToList().First());

            foreach (var oper in labelOperations)
            {
                operations.Remove(oper);
            }

            var modelOperations = answerJSONTemplate.Operations;

            foreach (var operation in operations)
            {
                modelOperations.Add(new Operation<Answer>(operation.op, operation.path, operation.from, operation.value));
            }

            JsonPatchDocument<Answer> modelJSONOperations = new JsonPatchDocument<Answer>(modelOperations, new DefaultContractResolver());

            if (modelJSONOperations != null)
            {
                var answer = _IAnswer.PatchAnswer(id, modelJSONOperations);

                //update the title and labels
                var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);

                var modelOperationsLabel = elementTranslationJSONTemplate.Operations;

                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.First().op, "/description", labelOperations.First().from, labelOperations.First().value));

                JsonPatchDocument<ElementTranslation> modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                int elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(answer.AnswerId), TYPE_LABEL.TITLE, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);

                modelOperationsLabel = elementTranslationJSONTemplate.Operations;

                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.Last().op, "/description", labelOperations.Last().from, labelOperations.Last().value));

                modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(answer.AnswerId), TYPE_LABEL.LABEL, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);

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
        public ActionResult<AnswerModel> PutAnswer([FromRoute] int id, [FromBody] AnswerModel answerModel)
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
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(answer.AnswerId), TYPE_LABEL.TITLE);
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(answer.AnswerId), TYPE_LABEL.LABEL);
                _IAnswer.DeleteAnswer(id);
                return Ok(answer);
            }

            //_IAnswer.DeleteAnswer(id);
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

