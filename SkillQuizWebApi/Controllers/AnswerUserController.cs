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

namespace SkillAnswerUserzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnswerUserController : ControllerBase
    {
        private readonly InterfaceAnswerUser _IAnswerUser;
        public AnswerUserController(InterfaceAnswerUser interfaceAnswerUser)
        {
            _IAnswerUser = interfaceAnswerUser;
        }

        //GET api/v1/AnswerUser
        [HttpGet]
        [Route("")]
        public List<AnswerUserModel> GetAllAnswerUser()
        {
            return _IAnswerUser.GetAllAnswerUser();
        }


        //GET api/v1/AnswerUser/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> GetAnswerUserById(int id)
        {
            var answerUser = _IAnswerUser.GetAnswerUserById(id);

            if (answerUser == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(answerUser);
        }

        //POST api/v1/AnswerUser
        [HttpPost]
        [Route("")]
        public ActionResult<AnswerUserModel> PostAnswerUser([FromBody] AnswerUserModelPostDTO answerUserModelPostDTO)
        {
            if (answerUserModelPostDTO != null)
            {
                var answerUserModel = new AnswerUserModel(answerUserModelPostDTO);
                var answerUserResult = _IAnswerUser.PostAnswerUser(answerUserModel);
                if (answerUserResult != null)
                {
                    return Created("/api/v1/AnswerUser/" + answerUserModel.AnswerUserId, answerUserResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/AnswerUser/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> PatchAnswerUser([FromRoute] int id, [FromBody] JsonPatchDocument<AnswerUser> answerUserModelJSON)
        {
            if (answerUserModelJSON != null)
            {
                var answerUser = _IAnswerUser.PatchAnswerUser(id, answerUserModelJSON);
                return Ok(answerUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/AnswerUser
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> PatchAnswerUser([FromRoute] int id, [FromBody] AnswerUserModel answerUserModel)
        {
            if (answerUserModel.AnswerUserId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (answerUserModel != null)
                {
                    var answerUser = _IAnswerUser.PutAnswerUser(answerUserModel);
                    return Ok(answerUser);
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }

        //DELETE api/v1/AnswerUser/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<AnswerUserModel> DeleteAnswerUser([FromRoute] int id)
        {
            var answerUser = _IAnswerUser.GetAnswerUserById(id);

            if (answerUser == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IAnswerUser.DeleteAnswerUser(id);
                return Ok(answerUser);
            }

            //_IAnswerUser.DeleteAnswerUser(id);
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

