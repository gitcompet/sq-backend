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
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.IO;
using System.ComponentModel;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly InterfaceQuiz _IQuiz;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "QUIZ_TITLE";
            public const string LABEL = "QUIZ_LABEL";
        }
        public QuizController(InterfaceQuiz interfaceQuiz, InterfaceElementTranslation iElementTranslation)
        {
            _IQuiz = interfaceQuiz;
            _IElementTranslation = iElementTranslation;
        }

        //GET api/v1/Quiz
        [HttpGet]
        [Route("")]
        public List<QuizModelLabel> GetAllQuiz()
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var collection = _IQuiz.GetAllQuiz();
            List<QuizModelLabel> result = new List<QuizModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new QuizModelLabel(item, _IElementTranslation.GetElementLabelById(item.QuizId.ToString(), TYPE_LABEL.TITLE, language)));
            }
            return result;
        }


        //GET api/v1/Quiz/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<QuizModelLabel> GetQuizById(int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var quiz = _IQuiz.GetQuizById(id);
            if (quiz == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new QuizModelLabel(quiz, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language));

            return Ok(result);
        }

        //POST api/v1/Quiz
        [HttpPost]
        [Route("")]
        public ActionResult<QuizModel> PostQuiz([FromBody] QuizModelPostDTO quizModelPostDTO)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            if (quizModelPostDTO != null)
            {
                var quizModel = new QuizModel(quizModelPostDTO);
                var quizResult = _IQuiz.PostQuiz(quizModel);
                if (quizResult != null)
                {
                    var labels = new ElementTranslationModel();

                    labels.Description = quizModelPostDTO.Title;
                    labels.ElementId = int.Parse(quizResult.QuizId);
                    labels.ElementType = TYPE_LABEL.TITLE;
                    labels.LanguagesId = language;

                    _IElementTranslation.PostElementTranslation(labels);
                    return Created("/api/v1/Quiz/" + quizModel.QuizId, quizResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Quiz/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<QuizModel> PatchQuiz([FromRoute] int id, [FromBody] JsonPatchDocument<QuizModelLabel> quizModelLabelJSON)
        {
            JsonPatchDocument<Quiz> quizJSONTemplate = new JsonPatchDocument<Quiz>();
            JsonPatchDocument<ElementTranslation> elementTranslationJSONTemplate = new JsonPatchDocument<ElementTranslation>();

            var operations = quizModelLabelJSON.Operations;
            var labelOperationsRaw = operations.Where(x => x.path == "/title");
            bool isTitle = false;

            Operation<QuizModelLabel> labelOperations = null;
            if (labelOperationsRaw.Any())
            {
                isTitle = true;
                labelOperations = labelOperationsRaw.ToList().First();
                operations.Remove(labelOperations);
            }

            var modelOperations = quizJSONTemplate.Operations;

            foreach ( var operation in operations )
            {
                modelOperations.Add(new Operation<Quiz>(operation.op, operation.path, operation.from, operation.value));
            }

            JsonPatchDocument<Quiz> modelJSONOperations = new JsonPatchDocument<Quiz>(modelOperations, new DefaultContractResolver());

            if (modelJSONOperations != null)
            {
                var quiz = _IQuiz.PatchQuiz(id, modelJSONOperations);

                //update the title and labels
                var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);

                var modelOperationsLabel = elementTranslationJSONTemplate.Operations;

                if (isTitle)
                {
                    modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.op, "/description", labelOperations.from, labelOperations.value));
                }

                JsonPatchDocument<ElementTranslation> modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                int elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(quiz.QuizId), TYPE_LABEL.TITLE, language).ElementTranslationId);


                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);
                
                return Ok(quiz);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Quiz
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<QuizModel> PutQuiz([FromRoute] int id, [FromBody] QuizModel quizModel)
        {
            if (quizModel.QuizId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (quizModel != null)
                {
                    var quiz = _IQuiz.PutQuiz(quizModel);
                    return Ok(quiz);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/Quiz/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<QuizModel> DeleteQuiz([FromRoute] int id)
        {
            var quiz = _IQuiz.GetQuizById(id);

            if (quiz == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(quiz.QuizId), TYPE_LABEL.TITLE);
                _IQuiz.DeleteQuiz(id);
                return Ok(quiz);
            }

            //_IQuiz.DeleteQuiz(id);
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

