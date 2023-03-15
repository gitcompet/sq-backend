using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestCategory
    {

        List<TestCategoryModel> GetAllTestCategory();


        TestCategoryModel GetTestCategoryById(int id);
        void PostTestCategory(TestCategoryModel testcategoryModel);
    }
}
