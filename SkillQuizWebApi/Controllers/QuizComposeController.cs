using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;





namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizComposeController : ControllerBase
    {

        private Business_Logic_Layer.QuizComposeBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceQuizCompose _IQuizCompose;
        public QuizComposeController(Business_Logic_Layer.Interface.InterfaceQuizCompose interfaceQuizCompose)
        {
            _BLL = new Business_Logic_Layer.QuizComposeBLL();
            _IQuizCompose = interfaceQuizCompose;
        }


        [HttpGet]
        [Route("getQuizComposes")]


        public List<QuizComposeModel> GetAllQuizCompose()
        {
            return _IQuizCompose.GetAllQuizCompose();
        }



        [HttpGet]
        [Route("getQuizCompose")]
        public ActionResult<QuizComposeModel> GetQuizComposeById(int id)
        {
            var quizcompose = _IQuizCompose.GetQuizComposeById(id);

            if (quizcompose == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(quizcompose);
        }




        [Route("postQuizCompose")]
        [HttpPost]
        public void postQuizCompose([FromBody] QuizComposeModel quizcomposeModel)
        {
            _IQuizCompose.PostQuizCompose(quizcomposeModel);
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

