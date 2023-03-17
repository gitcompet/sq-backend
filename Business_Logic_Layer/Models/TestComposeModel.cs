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
        public String TestComposeId { get; set; }
        public String TestId { get; set; }
        public String QuizId { get; set; }
    }
}
