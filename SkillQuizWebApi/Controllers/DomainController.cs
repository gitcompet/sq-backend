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
using System.Data;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(
        AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
        Roles = "ADMIN"
     )]
    public class DomainController : ControllerBase
    {
        private readonly InterfaceDomain _IDomain;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "DOMAIN_TITLE";
        }
        public DomainController(InterfaceDomain interfaceDomain, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation; 
            _IDomain = interfaceDomain;
        }

        //GET api/v1/Domain
        [HttpGet]
        [Route("")]
        public List<DomainModelLabel> GetAllDomain()
        {
            var language = 2;
            var collection = _IDomain.GetAllDomain();
            List<DomainModelLabel> result = new List<DomainModelLabel>();
            foreach (var item in collection)
            {
                result.Add(new DomainModelLabel(item, _IElementTranslation.GetElementLabelById(item.DomainId.ToString(), TYPE_LABEL.TITLE, language)));
            }
            return result;
        }


        //GET api/v1/Domain/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<DomainModelLabel> GetDomainById(int id)
        {
            var language = 2;
            var test = _IDomain.GetDomainById(id);
            if (test == null)
            {
                return NotFound("Invalid ID");
            }
            var result = new DomainModelLabel(test, _IElementTranslation.GetElementLabelById(id.ToString(), TYPE_LABEL.TITLE, language));

            return Ok(result);
        }

        //POST api/v1/Domain
        [HttpPost]
        [Route("")]
        public ActionResult<DomainModel> PostDomain([FromBody] DomainModelPostDTO domainModelPostDTO)
        {
            if (domainModelPostDTO != null)
            {
                var domainModel = new DomainModel(domainModelPostDTO);
                var domainResult = _IDomain.PostDomain(domainModel);
                if (domainResult != null)
                {
                    return Created("/api/v1/Domain/" + domainModel.DomainId, domainResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/Domain/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<DomainModel> PatchDomain([FromRoute] int id, [FromBody] JsonPatchDocument<Domain> domainModelJSON)
        {
            if (domainModelJSON != null)
            {
                var domain = _IDomain.PatchDomain(id, domainModelJSON);
                return Ok(domain);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/Domain
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<DomainModel> PatchDomain([FromRoute] int id, [FromBody] DomainModel domainModel)
        {
            if (domainModel.DomainId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (domainModel != null)
            {
                var domain = _IDomain.PutDomain(domainModel);
                return Ok(domain);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/Domain/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<DomainModel> DeleteDomain([FromRoute] int id)
        {
            var domain = _IDomain.GetDomainById(id);

            if (domain == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IDomain.DeleteDomain(id);
                return Ok(domain);
            }

            //_IDomain.DeleteDomain(id);
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

