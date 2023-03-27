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

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]
    [Route("api/v1/[controller]")]
    public class SubDomainController : ControllerBase
    {
        private readonly InterfaceSubDomain _ISubDomain;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "SUBDOMAIN_TITLE";
        }
        public SubDomainController(InterfaceSubDomain interfaceSubDomain, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _ISubDomain = interfaceSubDomain;
        }

        //GET api/v1/SubDomain
        [HttpGet]
        [Route("")]
        public List<SubDomainModelLabel> GetAllSubDomain()
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var collection = _ISubDomain.GetAllSubDomain();
            List<SubDomainModelLabel> result = new List<SubDomainModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new SubDomainModelLabel(item, _IElementTranslation.GetElementLabelById(item.SubDomainId.ToString(), TYPE_LABEL.TITLE, language)));
            }
            return result;
        }


        //GET api/v1/SubDomain/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<SubDomainModelLabel> GetSubDomainById(int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var test = _ISubDomain.GetSubDomainById(id);
            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new SubDomainModelLabel(test, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language));

            return Ok(result);
        }

        //POST api/v1/SubDomain
        [HttpPost]
        [Route("")]
        public ActionResult<SubDomainModel> PostSubDomain([FromBody] SubDomainModelPostDTO subDomainModelPostDTO)
        {
            if (subDomainModelPostDTO != null)
            {
                var subDomainModel = new SubDomainModel(subDomainModelPostDTO);
                var subDomainResult = _ISubDomain.PostSubDomain(subDomainModel);
                if (subDomainResult != null)
                {
                    return Created("/api/v1/SubDomain/" + subDomainModel.SubDomainId, subDomainResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/SubDomain/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<SubDomainModel> PatchSubDomain([FromRoute] int id, [FromBody] JsonPatchDocument<SubDomain> subDomainModelJSON)
        {
            if (subDomainModelJSON != null)
            {
                var subDomain = _ISubDomain.PatchSubDomain(id, subDomainModelJSON);
                return Ok(subDomain);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/SubDomain
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<SubDomainModel> PatchSubDomain([FromRoute] int id, [FromBody] SubDomainModel subDomainModel)
        {
            if (subDomainModel.SubDomainId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (subDomainModel != null)
            {
                var subDomain = _ISubDomain.PutSubDomain(subDomainModel);
                return Ok(subDomain);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/SubDomain/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<SubDomainModel> DeleteSubDomain([FromRoute] int id)
        {
            var subDomain = _ISubDomain.GetSubDomainById(id);

            if (subDomain == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _ISubDomain.DeleteSubDomain(id);
                return Ok(subDomain);
            }

            //_ISubDomain.DeleteSubDomain(id);
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

