using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private Business_Logic_Layer.UserBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceUser _IUser;
        public UserController(Business_Logic_Layer.Interface.InterfaceUser interfaceUser)
        {
            _BLL = new Business_Logic_Layer.UserBLL();
            _IUser = interfaceUser;
        }


        [HttpGet]
        [Route("getUsers")]


        public List<UserModel> GetAllUser()
        {
            return _IUser.GetAllUser();
        }



        [HttpGet]
        [Route("getUser")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            var User = _IUser.GetUserById(id);

            if (User == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(User);
        }




        [Route("postUser")]
        [HttpPost]
        public int postUser([FromBody] UserModelDTO userModelDTO)
        {
            return _IUser.PostUser(userModelDTO);
        }


    }
}

