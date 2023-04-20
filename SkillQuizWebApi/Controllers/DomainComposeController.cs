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
        Roles = "USER,RH,ADMIN"
     )]
    [Route("api/v1/[controller]")]
    public class DomainComposeController : ControllerBase
    {
        private readonly InterfaceDomainCompose _IDomainCompose;
        private readonly InterfaceElementTranslation _IElementTranslation;
        private static class TYPE_LABEL
        {
            public const string TITLE = "TEST_CATEGORY_TITLE";
        }
        public DomainComposeController(InterfaceDomainCompose interfaceDomainCompose, InterfaceElementTranslation interfaceElementTranslation)
        {
            _IElementTranslation = interfaceElementTranslation;
            _IDomainCompose = interfaceDomainCompose;
        }

        //GET api/v1/DomainCompose
        [HttpGet]
        [Route("")]
        public List<DomainComposeModel> GetAllDomainCompose()
        {
            return _IDomainCompose.GetAllDomainCompose();
        }


        //GET api/v1/DomainCompose/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<DomainComposeModelLabel> GetDomainComposeByElementId(string type,int id)
        {
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
            var listOfCompose = _IDomainCompose.GetDomainComposeByElementId(type, id);
            var listOfCategories = new List<string>();
            var listOfLabels = new List<string>();
            var listOfIds = new List<string>();
            foreach (DomainComposeModel item in listOfCompose)
            {
                listOfIds.Add(item.DomainComposeId);
                listOfCategories.Add(item.DomainId);
                listOfLabels.Add(_IElementTranslation.GetElementLabelById(item.DomainId, TYPE_LABEL.TITLE, language));
            }
            DomainComposeModelLabel result = new DomainComposeModelLabel(listOfIds, id.ToString(), listOfCategories, listOfLabels);
            return Ok(result);
        }

        //POST api/v1/DomainCompose
        [HttpPost]
        [Route("")]
        public ActionResult<DomainComposeModel> PostDomainCompose([FromBody] DomainComposeModelPostDTO domainComposeModelPostDTO)
        {
            if (domainComposeModelPostDTO != null)
            {
                var domainComposeModel = new DomainComposeModel(domainComposeModelPostDTO);
                var domainComposeResult = _IDomainCompose.PostDomainCompose(domainComposeModel);
                if (domainComposeResult != null)
                {
                    return Created("/api/v1/DomainCompose/" + domainComposeModel.DomainComposeId, domainComposeResult);
                }
            }
            return BadRequest(ModelState);
        }

        //PATCH api/v1/DomainCompose/{id}
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult<DomainComposeModel> PatchDomainCompose([FromRoute] int id, [FromBody] JsonPatchDocument<DomainCompose> domainComposeModelJSON)
        {
            if (domainComposeModelJSON != null)
            {
                var domainCompose = _IDomainCompose.PatchDomainCompose(id, domainComposeModelJSON);
                return Ok(domainCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/v1/DomainCompose
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<DomainComposeModel> PutDomainCompose([FromRoute] int id, [FromBody] DomainComposeModel domainComposeModel)
        {
            if (domainComposeModel.DomainComposeId != id.ToString())
            {
                return BadRequest("ID mismatch");
            }
            else
                if (domainComposeModel != null)
            {
                var domainCompose = _IDomainCompose.PutDomainCompose(domainComposeModel);
                return Ok(domainCompose);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE api/v1/DomainCompose/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<DomainComposeModel> DeleteDomainCompose([FromRoute] int id)
        {
            var domainCompose = _IDomainCompose.GetDomainComposeById(id);

            if (domainCompose == null)
            {
                return NotFound("Invalid ID");
            }
            else
            {
                _IDomainCompose.DeleteDomainCompose(id);
                return Ok(domainCompose);
            }

            //_IDomainCompose.DeleteDomainCompose(id);
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

