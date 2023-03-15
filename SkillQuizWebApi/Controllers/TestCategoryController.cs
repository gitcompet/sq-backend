using Business_Logic_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace SkillQuizzWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestCategoryController : ControllerBase
    {

        private Business_Logic_Layer.TestCategoryBLL _BLL;
        private readonly Business_Logic_Layer.Interface.InterfaceTestCategory _ITestCategory;
        public TestCategoryController(Business_Logic_Layer.Interface.InterfaceTestCategory interfaceTestCategory)
        {
            _BLL = new Business_Logic_Layer.TestCategoryBLL();
            _ITestCategory = interfaceTestCategory;
        }


        [HttpGet]
        [Route("getTestCategorys")]


        public List<TestCategoryModel> GetAllTestCategory()
        {
            return _ITestCategory.GetAllTestCategory();
        }



        [HttpGet]
        [Route("getTestCategory")]
        public ActionResult<TestCategoryModel> GetTestCategoryById(int id)
        {
            var testcategory = _ITestCategory.GetTestCategoryById(id);

            if (testcategory == null)
            {
                return NotFound("Invalid ID");
            }

            return Ok(testcategory);
        }




        [Route("postTestCategory")]
        [HttpPost]
        public void postTestCategory([FromBody] TestCategoryModel testcategoryModel)
        {
            _ITestCategory.PostTestCategory(testcategoryModel);
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

