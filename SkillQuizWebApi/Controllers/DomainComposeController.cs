using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainComposeController : ControllerBase
    {

        private Business_Logic_Layer.DomainComposeBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceDomainCompose _IDomainCompose;
        public DomainComposeController(Business_Logic_Layer.Interface.InterfaceDomainCompose interfaceDomainCompose)
        {
            _BLL = new Business_Logic_Layer.DomainComposeBLL();
            _IDomainCompose = interfaceDomainCompose;
        }


        [HttpGet]
        [Route("getDomainsCompose")]


        public List<DomainComposeModel> GetAllDomainCompose()
        {
            return _IDomainCompose.GetAllDomainCompose();
        }



        [HttpGet]
        [Route("getDomainCompose")]
        public ActionResult<DomainComposeModel> GetDomainComposeById(int id)
        {
            var domaincompose = _IDomainCompose.GetDomainComposeById(id);

            if (domaincompose == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(domaincompose);
        }




        [Route("postDomainCompose")]
        [HttpPost]
        public void postDomainCompose([FromBody] DomainComposeModel domaincomposeModel)
        {
            _IDomainCompose.PostDomainCompose(domaincomposeModel);
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

