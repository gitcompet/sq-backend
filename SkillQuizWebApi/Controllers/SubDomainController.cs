﻿using Business_Logic_Layer.Interface;
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
    public class SubDomainController : ControllerBase
    {
        private readonly InterfaceSubDomain _ISubDomain;
        public SubDomainController(InterfaceSubDomain interfaceSubDomain)
        {
            _ISubDomain = interfaceSubDomain;
        }

        //GET api/v1/SubDomain
        [HttpGet]
        [Route("")]
        public List<SubDomainModel> GetAllSubDomain()
        {
            return _ISubDomain.GetAllSubDomain();
        }


        //GET api/v1/SubDomain/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<SubDomainModel> GetSubDomainById(int id)
        {
            var subDomain = _ISubDomain.GetSubDomainById(id);

            if (subDomain == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(subDomain);
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

