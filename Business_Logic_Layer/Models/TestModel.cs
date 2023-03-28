using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestModel
    {
        public TestModel() { }
        public TestModel(TestModelPostDTO testModelDTO)
        {
            if (testModelDTO.Comment != null)
            {
                this.Comment = testModelDTO.Comment;
            }
            else
            {
                this.Comment = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public string TestId { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
