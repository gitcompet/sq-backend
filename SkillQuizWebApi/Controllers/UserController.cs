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

namespace SkillUserzWebApi.Controllers
{
    [ApiController]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles ="USER,ADMIN"
     )]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly InterfaceUser _IUser;
        public UserController(InterfaceUser interfaceUser)
        {
            _IUser = interfaceUser;
        }

        //GET api/v1/User
        [HttpGet]
        [Route("")]
        public List<UserModel> GetAllUser()
        {
            return _IUser.GetAllUser();
        }


        //GET api/v1/User/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<UserModelNoPassword> GetUserById(int id)
        {
            var rawuser = _IUser.GetUserById(id);

            if (rawuser == null)
            {
                return NotFound("Invalid ID");
            }

            UserModelNoPassword user = new UserModelNoPassword(rawuser);

            return Ok(user);
        }

        //POST api/v1/User
        [HttpPost]
        [Route("")]
        public ActionResult<UserModel> PostUser([FromBody] UserModelPostDTO userModelPostDTO)
        {
            if (userModelPostDTO != null)
            {
                var userModel = new UserModel(userModelPostDTO);
                var userResult = _IUser.PostUser(userModel);
                if (userResult != null)
                {
                    return Created("/api/v1/User/" + userModel.LoginId, userResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/User/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<UserModel> PatchUser([FromRoute] int id, [FromBody] JsonPatchDocument<User> userModelJSON)
        {
            if (userModelJSON != null)
            {
                var user = _IUser.PatchUser(id, userModelJSON);
                return Ok(user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/User
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<UserModel> PutUser([FromRoute] int id, [FromBody] UserModel userModel)
        {
            if (userModel.LoginId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (userModel != null)
            {
                var user = _IUser.PutUser(userModel);
                return Ok(user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/User/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<UserModel> DeleteUser([FromRoute] int id)
        {
            var user = _IUser.GetUserById(id);

            if (user == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IUser.DeleteUser(id);
                return Ok(user);
            }

            //_IUser.DeleteUser(id);
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

