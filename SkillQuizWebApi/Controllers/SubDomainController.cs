using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubDomainController : ControllerBase
    {

        private Business_Logic_Layer.SubDomainBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceSubDomain _ISubDomain;
        public SubDomainController(Business_Logic_Layer.Interface.InterfaceSubDomain interfaceSubDomain)
        {
            _BLL = new Business_Logic_Layer.SubDomainBLL();
            _ISubDomain = interfaceSubDomain;
        }


        [HttpGet]
        [Route("getSubDomains")]


        public List<SubDomainModel> GetAllSubDomain()
        {
            return _ISubDomain.GetAllSubDomain();
        }



        [HttpGet]
        [Route("getSubDomain")]
        public ActionResult<SubDomainModel> GetSubDomainById(int id)
        {
            var subdomain = _ISubDomain.GetSubDomainById(id);

            if (subdomain == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(subdomain);
        }




        [Route("postSubDomain")]
        [HttpPost]
        public void postSubDomain([FromBody] SubDomainModel subdomainModel)
        {
            _ISubDomain.PostSubDomain(subdomainModel);
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

