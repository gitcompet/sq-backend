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

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestCategoryController : ControllerBase
    {

        private Business_Logic_Layer.TestCategoryBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestCategory _ITestCategory;
        public TestCategoryController(Business_Logic_Layer.Interface.InterfaceTestCategory interfaceTestCategory)
        {
            _BLL = new Business_Logic_Layer.TestCategoryBLL();
            _ITestCategory = interfaceTestCategory;
        }

        //GET api/v1/TestCategory
        [HttpGet]
        [Route("")]
        public List<TestCategoryModel> GetAllTestCategory()
        {
            return _ITestCategory.GetAllTestCategory();
        }


        //GET api/v1/TestCategory/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestCategoryModel> GetTestCategoryById(int id)
        {
            var testCategory = _ITestCategory.GetTestCategoryById(id);

            if (testCategory == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testCategory);
        }

        //POST api/v1/TestCategory
        [HttpPost]
        [Route("")]
        public ActionResult<TestCategoryModel> PostTestCategory([FromBody] TestCategoryModelPostDTO testCategoryModelPostDTO)
        {
            if (testCategoryModelPostDTO != null)
            {
                var testCategoryModel = new TestCategoryModel(testCategoryModelPostDTO);
                var testCategoryResult = _ITestCategory.PostTestCategory(testCategoryModel);
                if (testCategoryResult != null)
                {
                    return Created("/api/v1/TestCategory/" + testCategoryModel.TestCategoryId, testCategoryResult);
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
        public ActionResult<TestCategoryModel> PatchTestCategory([FromRoute] int id, [FromBody] TestCategoryModel testCategoryModel)
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

