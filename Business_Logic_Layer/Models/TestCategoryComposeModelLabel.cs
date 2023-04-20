using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestCategoryComposeModelLabel
    {
        public TestCategoryComposeModelLabel(IEnumerable<string> testCategoryComposeId, string id, IEnumerable<string> cats, IEnumerable<string> catNames)
        {
            this.TestCategoryComposeId = testCategoryComposeId;
            this.TestCategoryId = cats;
            this.TestId = id;
            this.CategoryNames = catNames;
        }
        public IEnumerable<string> TestCategoryComposeId { get; set; }
        public string TestId { get; set; }
        public IEnumerable<string> TestCategoryId { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
    }
}
