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
    /*[Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]*/
    public class TestCategoryComposeController : ControllerBase
    {
        private readonly InterfaceTestCategoryCompose _ITestCategoryCompose;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "TEST_CATEGORY_TITLE";
        }
        public TestCategoryComposeController(InterfaceTestCategoryCompose interfaceTestCategoryCompose, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ITestCategoryCompose = interfaceTestCategoryCompose;
        }

        //GET api/v1/TestCategoryCompose
        [HttpGet]
        [Route("")]
        public List<TestCategoryComposeModel> GetAllTestCategoryCompose()
        {
            return _ITestCategoryCompose.GetAllTestCategoryCompose();
        }


        //GET api/v1/TestCategoryCompose/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestCategoryComposeModelLabel> GetTestCategoryComposeByTestId(int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var listOfCompose = _ITestCategoryCompose.GetTestCategoryComposeByTestId(id);
            var listOfCategories = new List<string>();
            var listOfLabels = new List<string>();
            foreach(TestCategoryComposeModel item in listOfCompose)
            {
                listOfCategories.Add(item.TestCategoryId);
                listOfLabels.Add(_IElementTranslation.GetElementLabelById(item.TestCategoryId, TYPE_LABEL.TITLE, language));
            }
            TestCategoryComposeModelLabel result = new TestCategoryComposeModelLabel(id.ToString(), listOfCategories, listOfLabels);
            return Ok(result);
        }

        //POST api/v1/TestCategoryCompose
        [HttpPost]
        [Route("")]
        public ActionResult<TestCategoryComposeModel> PostTestCategoryCompose([FromBody] TestCategoryComposeModelPostDTO testCategoryComposeModelPostDTO)
        {
            if (testCategoryComposeModelPostDTO != null)
            {
                var testCategoryComposeModel = new TestCategoryComposeModel(testCategoryComposeModelPostDTO);
                var testCategoryComposeResult = _ITestCategoryCompose.PostTestCategoryCompose(testCategoryComposeModel);
                if (testCategoryComposeResult != null)
                {
                    return Created("/api/v1/TestCategoryCompose/" + testCategoryComposeModel.TestCategoryComposeId, testCategoryComposeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestCategoryCompose/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestCategoryComposeModel> PatchTestCategoryCompose([FromRoute] int id, [FromBody] JsonPatchDocument<TestCategoryCompose> testCategoryComposeModelJSON)
        {
            if (testCategoryComposeModelJSON != null)
            {
                var testCategoryCompose = _ITestCategoryCompose.PatchTestCategoryCompose(id, testCategoryComposeModelJSON);
                return Ok(testCategoryCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestCategoryCompose
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestCategoryComposeModel> PutTestCategoryCompose([FromRoute] int id, [FromBody] TestCategoryComposeModel testCategoryComposeModel)
        {
            if (testCategoryComposeModel.TestCategoryComposeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testCategoryComposeModel != null)
            {
                var testCategoryCompose = _ITestCategoryCompose.PutTestCategoryCompose(testCategoryComposeModel);
                return Ok(testCategoryCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/TestCategoryCompose/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestCategoryComposeModel> DeleteTestCategoryCompose([FromRoute] int id)
        {
            var testCategoryCompose = _ITestCategoryCompose.GetTestCategoryComposeById(id);

            if (testCategoryCompose == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ITestCategoryCompose.DeleteTestCategoryCompose(id);
                return Ok(testCategoryCompose);
            }

            //_ITestCategoryCompose.DeleteTestCategoryCompose(id);
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

