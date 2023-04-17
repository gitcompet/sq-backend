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
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "USER"
     )]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly InterfaceTest _ITest;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "TEST_TITLE";
            public const string LABEL = "TEST_LABEL";
        }
        public TestController(InterfaceTest interfaceTest, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ITest = interfaceTest;
        }

        //GET api/v1/Test
        [HttpGet]
        [Route("")]
        public List<TestModelLabel> GetAllTest()
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var collection = _ITest.GetAllTest();
            List<TestModelLabel> result = new List<TestModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new TestModelLabel(item, _IElementTranslation.GetElementLabelById(item.TestId.ToString(), TYPE_LABEL.TITLE, language)));
            }
            return result;
        }


        //GET api/v1/Test/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestModelLabel> GetTestById(int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var test = _ITest.GetTestById(id);
            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new TestModelLabel(test, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language));

            return Ok(result);
        }

        //POST api/v1/Test
        [HttpPost]
        [Route("")]
        public ActionResult<TestModel> PostTest([FromBody] TestModelPostDTO testModelPostDTO)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            if (testModelPostDTO != null)
            {
                var testModel = new TestModel(testModelPostDTO);
                var testResult = _ITest.PostTest(testModel);
                if (testResult != null)
                {
                    var labels = new ElementTranslationModel();

                    labels.Description = testModelPostDTO.Title;
                    labels.ElementId = int.Parse(testResult.TestId);
                    labels.ElementType = TYPE_LABEL.TITLE;
                    labels.LanguagesId = language;

                    _IElementTranslation.PostElementTranslation(labels);
                    return Created("/api/v1/Test/" + testModel.TestId, testResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Test/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestModel> PatchTest([FromRoute] int id, [FromBody] JsonPatchDocument<TestModelLabel> testModelLabelJSON)
        {
            JsonPatchDocument<Test> testJSONTemplate = new JsonPatchDocument<Test>();
            JsonPatchDocument<ElementTranslation> elementTranslationJSONTemplate = new JsonPatchDocument<ElementTranslation>();

            var operations = testModelLabelJSON.Operations;
            var labelOperations = operations.Where(x => x.path == "/title").ToList().First();
            operations.Remove(labelOperations);

            var modelOperations = testJSONTemplate.Operations;

            foreach (var operation in operations)
            {
                modelOperations.Add(new Operation<Test>(operation.op, operation.path, operation.from, operation.value));
            }

            JsonPatchDocument<Test> modelJSONOperations = new JsonPatchDocument<Test>(modelOperations, new DefaultContractResolver());

            if (modelJSONOperations != null)
            {
                var test = _ITest.PatchTest(id, modelJSONOperations);

                //update the title and labels
                var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);

                var modelOperationsLabel = elementTranslationJSONTemplate.Operations;
                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.op, "/description", labelOperations.from, labelOperations.value));

                JsonPatchDocument<ElementTranslation> modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                int elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(test.TestId), TYPE_LABEL.TITLE, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);

                return Ok(test);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //PUT api/v1/Test
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestModel> PutTest([FromRoute] int id, [FromBody] TestModel testModel)
        {
            if (testModel.TestId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testModel != null)
            {
                var test = _ITest.PutTest(testModel);
                return Ok(test);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/Test/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestModel> DeleteTest([FromRoute] int id)
        {
            var test = _ITest.GetTestById(id);

            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ITest.DeleteTest(id);
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(test.TestId), TYPE_LABEL.TITLE);
                return Ok(test);
            }

            //_ITest.DeleteTest(id);
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
