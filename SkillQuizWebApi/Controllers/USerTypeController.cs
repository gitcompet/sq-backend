using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly InterfaceUserType _IUserType;
        public UserTypeController(InterfaceUserType interfaceUserType)
        {
            _IUserType = interfaceUserType;
        }


        [HttpGet]
        [Route("getUserTypes")]


        public List<UserTypeModel> GetAllUserType()
        {
            return _IUserType.GetAllUserType();
        }



        [HttpGet]
        [Route("getUserType")]
        public ActionResult<UserTypeModel> GetUserTypeById(int id)
        {
            var usertype = _IUserType.GetUserTypeById(id);

            if (usertype == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(usertype);
        }




        [Route("postUserType")]
        [HttpPost]
        public void postUserType([FromBody] UserTypeModel usertypeModel)
        {
            _IUserType.PostUserType(usertypeModel);
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

