using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainController : ControllerBase
    {

        private Business_Logic_Layer.DomainBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceDomain _IDomain;
        public DomainController(Business_Logic_Layer.Interface.InterfaceDomain interfaceDomain)
        {
            _BLL = new Business_Logic_Layer.DomainBLL();
            _IDomain = interfaceDomain;
        }


        [HttpGet]
        [Route("getDomains")]
        public List<DomainModel> GetAllDomain()
        {
            return _IDomain.GetAllDomain();
        }

        [HttpGet]
        [Route("getDomain")]
        public ActionResult<DomainModel> GetDomainById(int id)
        {
            var domain = _IDomain.GetDomainById(id);

            if (domain == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(domain);
        }

        [Route("postDomain")]
        [HttpPost]
        public void postDomain([FromBody] DomainModel domainModel)
        {
            _IDomain.PostDomain(domainModel);
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

