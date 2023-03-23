using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestCategoryModel
    {
        public TestCategoryModel () { }
        public TestCategoryModel (TestCategoryModelPostDTO testCategoryModelPostDTO)
        {

            if (testCategoryModelPostDTO.Description != null)
            {
                this.Description = testCategoryModelPostDTO.Description;
            }
            else
            {
                this.Description = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public string TestCategoryId { get; set; }
        public string Description { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
