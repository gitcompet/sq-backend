using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestCategoryModelLabel
    {
        public TestCategoryModelLabel () { }
        public TestCategoryModelLabel (TestCategoryModel testCategoryModel, string title)
        {
            this.TestCategoryId = testCategoryModel.TestCategoryId;
            this.Description = testCategoryModel.Description;
            this.isActive = testCategoryModel.isActive;
            this.isDeleted = testCategoryModel.isDeleted;
            this.Title = title;
        }
        public string TestCategoryId { get; set; }
        public string Description { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
        public string Title { get; set; }
    }
}
