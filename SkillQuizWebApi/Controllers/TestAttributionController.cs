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
    public class TestAttributionController : ControllerBase
    {

        private Business_Logic_Layer.TestAttributionBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestAttribution _ITestAttribution;
        public TestAttributionController(Business_Logic_Layer.Interface.InterfaceTestAttribution interfaceTestAttribution)
        {
            _BLL = new Business_Logic_Layer.TestAttributionBLL();
            _ITestAttribution = interfaceTestAttribution;
        }

        //GET api/v1/TestAttribution
        [HttpGet]
        [Route("")]
        public List<TestAttributionModel> GetAllTestAttribution()
        {
            return _ITestAttribution.GetAllTestAttribution();
        }


        //GET api/v1/TestAttribution/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestAttributionModel> GetTestAttributionById(int id)
        {
            var testAttribution = _ITestAttribution.GetTestAttributionById(id);

            if (testAttribution == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testAttribution);
        }

        //POST api/v1/TestAttribution
        [HttpPost]
        [Route("")]
        public ActionResult<TestAttributionModel> PostTestAttribution([FromBody] TestAttributionModelPostDTO testAttributionModelPostDTO)
        {
            if (testAttributionModelPostDTO != null)
            {
                var testAttributionModel = new TestAttributionModel(testAttributionModelPostDTO);
                var testAttributionResult = _ITestAttribution.PostTestAttribution(testAttributionModel);
                if (testAttributionResult != null)
                {
                    return Created("/api/v1/TestAttribution/" + testAttributionModel.TestAttributionId, testAttributionResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestAttribution/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestAttributionModel> PatchTestAttribution([FromRoute] int id, [FromBody] JsonPatchDocument<TestAttribution> testAttributionModelJSON)
        {
            if (testAttributionModelJSON != null)
            {
                var testAttribution = _ITestAttribution.PatchTestAttribution(id, testAttributionModelJSON);
                return Ok(testAttribution);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestAttribution
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestAttributionModel> PatchTestAttribution([FromRoute] int id, [FromBody] TestAttributionModel testAttributionModel)
        {
            if (testAttributionModel.TestAttributionId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testAttributionModel != null)
            {
                var testAttribution = _ITestAttribution.PutTestAttribution(testAttributionModel);
                return Ok(testAttribution);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/TestAttribution/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestAttributionModel> DeleteTestAttribution([FromRoute] int id)
        {
            var testAttribution = _ITestAttribution.GetTestAttributionById(id);

            if (testAttribution == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ITestAttribution.DeleteTestAttribution(id);
                return Ok(testAttribution);
            }

            //_ITestAttribution.DeleteTestAttribution(id);
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

