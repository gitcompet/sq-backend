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
    public class TestComposeController : ControllerBase
    {

        private Business_Logic_Layer.TestComposeBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestCompose _ITestCompose;
        public TestComposeController(Business_Logic_Layer.Interface.InterfaceTestCompose interfaceTestCompose)
        {
            _BLL = new Business_Logic_Layer.TestComposeBLL();
            _ITestCompose = interfaceTestCompose;
        }

        //GET api/v1/TestCompose
        [HttpGet]
        [Route("")]
        public List<TestComposeModel> GetAllTestCompose()
        {
            return _ITestCompose.GetAllTestCompose();
        }


        //GET api/v1/TestCompose/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestComposeModel> GetTestComposeById(int id)
        {
            var testCompose = _ITestCompose.GetTestComposeById(id);

            if (testCompose == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testCompose);
        }

        //POST api/v1/TestCompose
        [HttpPost]
        [Route("")]
        public ActionResult<TestComposeModel> PostTestCompose([FromBody] TestComposeModelPostDTO testComposeModelPostDTO)
        {
            if (testComposeModelPostDTO != null)
            {
                var testComposeModel = new TestComposeModel(testComposeModelPostDTO);
                var testComposeResult = _ITestCompose.PostTestCompose(testComposeModel);
                if (testComposeResult != null)
                {
                    return Created("/api/v1/TestCompose/" + testComposeModel.TestComposeId, testComposeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestCompose/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestComposeModel> PatchTestCompose([FromRoute] int id, [FromBody] JsonPatchDocument<TestCompose> testComposeModelJSON)
        {
            if (testComposeModelJSON != null)
            {
                var testCompose = _ITestCompose.PatchTestCompose(id, testComposeModelJSON);
                return Ok(testCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestCompose
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestComposeModel> PatchTestCompose([FromRoute] int id, [FromBody] TestComposeModel testComposeModel)
        {
            if (testComposeModel.TestComposeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testComposeModel != null)
            {
                var testCompose = _ITestCompose.PutTestCompose(testComposeModel);
                return Ok(testCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/TestCompose/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestComposeModel> DeleteTestCompose([FromRoute] int id)
        {
            var testCompose = _ITestCompose.GetTestComposeById(id);

            if (testCompose == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ITestCompose.DeleteTestCompose(id);
                return Ok(testCompose);
            }

            //_ITestCompose.DeleteTestCompose(id);
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

