using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestModelLabel
    {
        public TestModelLabel() { }
        public TestModelLabel(TestModel testModel, string title)
        {
            this.TestId = testModel.TestId;
            this.Comment = testModel.Comment;
            this.isActive = testModel.isActive;
            this.isDeleted = testModel.isDeleted;
            this.Title = title;
        }
        public string TestId { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
        //label
        public string Title { get; set; }
    }
}
