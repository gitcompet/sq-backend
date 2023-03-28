using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestCategoryComposeModelLabel
    {
        public TestCategoryComposeModelLabel(string id, IEnumerable<string> cats, IEnumerable<string> catNames)
        {
            this.TestCategoryId = cats;
            this.TestId = id;
            this.CategoryNames = catNames;
        }
        public string TestId { get; set; }
        public IEnumerable<string> TestCategoryId { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
    }
}
