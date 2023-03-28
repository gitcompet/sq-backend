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
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]
    [Route("api/v1/[controller]")]
    public class SubDomainComposeController : ControllerBase
    {
        private readonly InterfaceSubDomainCompose _ISubDomainCompose;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "TEST_CATEGORY_TITLE";
        }
        public SubDomainComposeController(InterfaceSubDomainCompose interfaceSubDomainCompose, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ISubDomainCompose = interfaceSubDomainCompose;
        }

        //GET api/v1/SubDomainCompose
        [HttpGet]
        [Route("")]
        public List<SubDomainComposeModel> GetAllSubDomainCompose()
        {
            return _ISubDomainCompose.GetAllSubDomainCompose();
        }


        //GET api/v1/SubDomainCompose/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<SubDomainComposeModelLabel> GetSubDomainComposeByElementId(string type,int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var listOfCompose = _ISubDomainCompose.GetSubDomainComposeByElementId(type, id);
            var listOfCategories = new List<string>();
            var listOfLabels = new List<string>();
            foreach (SubDomainComposeModel item in listOfCompose)
            {
                listOfCategories.Add(item.SubDomainId);
                listOfLabels.Add(_IElementTranslation.GetElementLabelById(item.SubDomainId, TYPE_LABEL.TITLE, language));
            }
            SubDomainComposeModelLabel result = new SubDomainComposeModelLabel(id.ToString(), listOfCategories, listOfLabels);
            return Ok(result);
        }

        //POST api/v1/SubDomainCompose
        [HttpPost]
        [Route("")]
        public ActionResult<SubDomainComposeModel> PostSubDomainCompose([FromBody] SubDomainComposeModelPostDTO subDomainComposeModelPostDTO)
        {
            if (subDomainComposeModelPostDTO != null)
            {
                var subDomainComposeModel = new SubDomainComposeModel(subDomainComposeModelPostDTO);
                var subDomainComposeResult = _ISubDomainCompose.PostSubDomainCompose(subDomainComposeModel);
                if (subDomainComposeResult != null)
                {
                    return Created("/api/v1/SubDomainCompose/" + subDomainComposeModel.SubDomainComposeId, subDomainComposeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/SubDomainCompose/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<SubDomainComposeModel> PatchSubDomainCompose([FromRoute] int id, [FromBody] JsonPatchDocument<SubDomainCompose> subDomainComposeModelJSON)
        {
            if (subDomainComposeModelJSON != null)
            {
                var subDomainCompose = _ISubDomainCompose.PatchSubDomainCompose(id, subDomainComposeModelJSON);
                return Ok(subDomainCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/SubDomainCompose
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<SubDomainComposeModel> PutSubDomainCompose([FromRoute] int id, [FromBody] SubDomainComposeModel subDomainComposeModel)
        {
            if (subDomainComposeModel.SubDomainComposeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (subDomainComposeModel != null)
            {
                var subDomainCompose = _ISubDomainCompose.PutSubDomainCompose(subDomainComposeModel);
                return Ok(subDomainCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/SubDomainCompose/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<SubDomainComposeModel> DeleteSubDomainCompose([FromRoute] int id)
        {
            var subDomainCompose = _ISubDomainCompose.GetSubDomainComposeById(id);

            if (subDomainCompose == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ISubDomainCompose.DeleteSubDomainCompose(id);
                return Ok(subDomainCompose);
            }

            //_ISubDomainCompose.DeleteSubDomainCompose(id);
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

