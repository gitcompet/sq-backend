using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestCategoryComposeModel
    {
        public TestCategoryComposeModel() { }
        public TestCategoryComposeModel(TestCategoryComposeModelPostDTO testCategoryComposeModelPostDTO)
        {
            this.TestCategoryId = testCategoryComposeModelPostDTO.TestCategoryId;
            this.TestId = testCategoryComposeModelPostDTO.TestId;
        }
        public string TestCategoryComposeId { get; set; }
        public string TestId { get; set; }
        public string TestCategoryId { get; set; }
    }
}
