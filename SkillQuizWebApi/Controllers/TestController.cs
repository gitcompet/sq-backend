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

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly InterfaceTest _ITest;
        public TestController(InterfaceTest interfaceTest)
        {
            _ITest = interfaceTest;
        }

        //GET api/v1/Test
        [HttpGet]
        [Route("")]
        public List<TestModel> GetAllTest()
        {
            return _ITest.GetAllTest();
        }


        //GET api/v1/Test/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestModel> GetTestById(int id)
        {
            var test = _ITest.GetTestById(id);

            if (test == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(test);
        }

        //POST api/v1/Test
        [HttpPost]
        [Route("")]
        public ActionResult<TestModel> PostTest([FromBody] TestModelPostDTO testModelPostDTO)
        {
            if (testModelPostDTO != null)
            {
                var testModel = new TestModel(testModelPostDTO);
                var testResult = _ITest.PostTest(testModel);
                if (testResult != null)
                {
                    return Created("/api/v1/Test/" + testModel.TestId, testResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Test/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestModel> PatchTest([FromRoute] int id, [FromBody] JsonPatchDocument<Test> testModelJSON)
        {
            if (testModelJSON != null)
            {
                var test = _ITest.PatchTest(id, testModelJSON);
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
        public ActionResult<TestModel> PatchTest([FromRoute] int id, [FromBody] TestModel testModel)
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
