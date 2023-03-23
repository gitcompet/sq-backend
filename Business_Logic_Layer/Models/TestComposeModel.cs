using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestComposeModel
    {
        public TestComposeModel() { }
        public TestComposeModel(TestComposeModelPostDTO testComposeModelPostDTO)
        {
            this.QuizId = testComposeModelPostDTO.QuizId;
            this.TestId = testComposeModelPostDTO.TestId;
        }
        public string TestComposeId { get; set; }
        public string TestId { get; set; }
        public string QuizId { get; set; }
    }
}
