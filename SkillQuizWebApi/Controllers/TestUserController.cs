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

namespace SkillTestUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestUserController : ControllerBase
    {
        private readonly InterfaceTestUser _ITestUser;
        public TestUserController(InterfaceTestUser interfaceTestUser)
        {
            _ITestUser = interfaceTestUser;
        }

        //GET api/v1/TestUser
        [HttpGet]
        [Route("")]
        public List<TestUserModel> GetAllTestUser()
        {
            return _ITestUser.GetAllTestUser();
        }


        //GET api/v1/TestUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> GetTestUserById(int id)
        {
            var testUser = _ITestUser.GetTestUserById(id);

            if (testUser == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testUser);
        }

        //POST api/v1/TestUser
        [HttpPost]
        [Route("")]
        public ActionResult<TestUserModel> PostTestUser([FromBody] TestUserModelPostDTO testUserModelPostDTO)
        {
            if (testUserModelPostDTO != null)
            {
                var testUserModel = new TestUserModel(testUserModelPostDTO);
                var testUserResult = _ITestUser.PostTestUser(testUserModel);
                if (testUserResult != null)
                {
                    return Created("/api/v1/TestUser/" + testUserModel.TestUserId, testUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/TestUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> PatchTestUser([FromRoute] int id, [FromBody] JsonPatchDocument<TestUser> testUserModelJSON)
        {
            if (testUserModelJSON != null)
            {
                var testUser = _ITestUser.PatchTestUser(id, testUserModelJSON);
                return Ok(testUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/TestUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> PatchTestUser([FromRoute] int id, [FromBody] TestUserModel testUserModel)
        {
            if (testUserModel.TestUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (testUserModel != null)
                {
                    var testUser = _ITestUser.PutTestUser(testUserModel);
                    return Ok(testUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/TestUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<TestUserModel> DeleteTestUser([FromRoute] int id)
        {
            var testUser = _ITestUser.GetTestUserById(id);

            if (testUser == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ITestUser.DeleteTestUser(id);
                return Ok(testUser);
            }

            //_ITestUser.DeleteTestUser(id);
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

