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
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;

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
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
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
            var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);
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
        public ActionResult<DomainModel> PatchDomain([FromRoute] int id, [FromBody] JsonPatchDocument<DomainModelLabel> domainModelLabelJSON)
        {
            JsonPatchDocument<Domain> domainJSONTemplate = new JsonPatchDocument<Domain>();
            JsonPatchDocument<ElementTranslation> elementTranslationJSONTemplate = new JsonPatchDocument<ElementTranslation>();

            var operations = domainModelLabelJSON.Operations;
            var labelOperations = operations.Where(x => x.path == "/title").ToList().First();
            operations.Remove(labelOperations);

            var modelOperations = domainJSONTemplate.Operations;

            foreach (var operation in operations)
            {
                modelOperations.Add(new Operation<Domain>(operation.op, operation.path, operation.from, operation.value));
            }

            JsonPatchDocument<Domain> modelJSONOperations = new JsonPatchDocument<Domain>(modelOperations, new DefaultContractResolver());

            if (modelJSONOperations != null)
            {
                var domain = _IDomain.PatchDomain(id, modelJSONOperations);

                //update the title and labels
                var language = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value);

                var modelOperationsLabel = elementTranslationJSONTemplate.Operations;
                modelOperationsLabel.Add(new Operation<ElementTranslation>(labelOperations.op, "/description", labelOperations.from, labelOperations.value));

                JsonPatchDocument<ElementTranslation> modelJSONOperationsLabel = new JsonPatchDocument<ElementTranslation>(modelOperationsLabel, new DefaultContractResolver());

                int elementTranslationId = int.Parse(_IElementTranslation.GetElementTranslationByKey(int.Parse(domain.DomainId), TYPE_LABEL.TITLE, language).ElementTranslationId);

                _IElementTranslation.PatchElementTranslation(elementTranslationId, modelJSONOperationsLabel);

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
        public ActionResult<DomainModel> PutDomain([FromRoute] int id, [FromBody] DomainModel domainModel)
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
                _IElementTranslation.DeleteElementTranslationByItem(int.Parse(domain.DomainId), TYPE_LABEL.TITLE);
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

