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

namespace SkillUserTypezWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Route("[controller]")]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]
    public class UserTypeController : ControllerBase
    {
        private readonly InterfaceUserType _IUserType;
        public UserTypeController(InterfaceUserType interfaceUserType)
        {
            _IUserType = interfaceUserType;
        }

        //GET api/v1/UserType
        [HttpGet]
        [Route("")]
        public List<UserTypeModel> GetAllUserType()
        {
            return _IUserType.GetAllUserType();
        }


        //GET api/v1/UserType/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<UserTypeModel> GetUserTypeById(int id)
        {
            var userType = _IUserType.GetUserTypeById(id);

            if (userType == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(userType);
        }

        //POST api/v1/UserType
        [HttpPost]
        [Route("")]
        public ActionResult<UserTypeModel> PostUserType([FromBody] UserTypeModelPostDTO userTypeModelPostDTO)
        {
            if (userTypeModelPostDTO != null)
            {
                var userTypeModel = new UserTypeModel(userTypeModelPostDTO);
                var userTypeResult = _IUserType.PostUserType(userTypeModel);
                if (userTypeResult != null)
                {
                    return Created("/api/v1/UserType/" + userTypeModel.UserTypeId, userTypeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/UserType/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<UserTypeModel> PatchUserType([FromRoute] int id, [FromBody] JsonPatchDocument<UserType> userTypeModelJSON)
        {
            if (userTypeModelJSON != null)
            {
                var userType = _IUserType.PatchUserType(id, userTypeModelJSON);
                return Ok(userType);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/UserType
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<UserTypeModel> PatchUserType([FromRoute] int id, [FromBody] UserTypeModel userTypeModel)
        {
            if (userTypeModel.UserTypeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (userTypeModel != null)
            {
                var userType = _IUserType.PutUserType(userTypeModel);
                return Ok(userType);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/UserType/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<UserTypeModel> DeleteUserType([FromRoute] int id)
        {
            var userType = _IUserType.GetUserTypeById(id);

            if (userType == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IUserType.DeleteUserType(id);
                return Ok(userType);
            }
        }
    }
}

