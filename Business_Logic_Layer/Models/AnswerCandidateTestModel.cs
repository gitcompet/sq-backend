using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerCandidateTestModel
    {
        public String AssignmentTestId { get; set; }
        public String TestId { get; set; }
        public String QuizId { get; set; }
        public String QuestionId { get; set; }
        public String AnswerId { get; set; }

    }
}
