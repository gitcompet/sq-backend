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

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]
    public class TestCategoryController : ControllerBase
    {
        private readonly InterfaceTestCategory _ITestCategory;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "TEST_CATEGORY_TITLE";
        }
        public TestCategoryController(InterfaceTestCategory interfaceTestCategory, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ITestCategory = interfaceTestCategory;
        }

        //GET api/v1/TestCategory
        [HttpGet]
        [Route("")]
        public List<TestCategoryModelLabel> GetAllTestCategory()
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var collection = _ITestCategory.GetAllTestCategory();
            List<TestCategoryModelLabel> result = new List<TestCategoryModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new TestCategoryModelLabel(item, _IElementTranslation.GetElementLabelById(item.TestCategoryId.ToString(), TYPE_LABEL.TITLE, language)));
            }
            return result;
        }


        //GET api/v1/TestCategory/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestCategoryModelLabel> GetTestCategoryById(int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var test = _ITestCategory.GetTestCategoryById(id);
            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new TestCategoryModelLabel(test, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language));

            return Ok(result);
        }

        //POST api/v1/TestCategory
        [HttpPost]
        [Route("")]
        public ActionResult<TestCategoryModel> PostTestCategory([FromBody] TestCategoryModelPostDTO testCategoryModelPostDTO)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            if (testCategoryModelPostDTO != null)
            {
                var questionModel = new TestCategoryModel(testCategoryModelPostDTO);
                var questionResult = _ITestCategory.PostTestCategory(questionModel);
                if (questionResult != null)
                {
                    var labels = new ElementTranslationModel();

                    labels.Description = testCategoryModelPostDTO.Description;
                    labels.ElementId = int.Parse(questionResult.TestCategoryId);
                    labels.ElementType = TYPE_LABEL.TITLE;
                    labels.LanguagesId = language;

                    _IElementTranslation.PostElementTranslation(labels);
                    return Created("/api/v1/TestCategory/" + questionModel.TestCategoryId, questionResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestCategory/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestCategoryModel> PatchTestCategory([FromRoute] int id, [FromBody] JsonPatchDocument<TestCategory> testCategoryModelJSON)
        {
            if (testCategoryModelJSON != null)
            {
                var testCategory = _ITestCategory.PatchTestCategory(id, testCategoryModelJSON);
                return Ok(testCategory);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestCategory
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestCategoryModel> PutTestCategory([FromRoute] int id, [FromBody] TestCategoryModel testCategoryModel)
        {
            if (testCategoryModel.TestCategoryId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testCategoryModel != null)
            {
                var testCategory = _ITestCategory.PutTestCategory(testCategoryModel);
                return Ok(testCategory);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/TestCategory/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestCategoryModel> DeleteTestCategory([FromRoute] int id)
        {
            var testCategory = _ITestCategory.GetTestCategoryById(id);

            if (testCategory == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(testCategory.TestCategoryId), TYPE_LABEL.TITLE);
                _ITestCategory.DeleteTestCategory(id);
                return Ok(testCategory);
            }

            //_ITestCategory.DeleteTestCategory(id);
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

