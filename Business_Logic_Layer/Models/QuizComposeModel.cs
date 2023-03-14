using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizComposeModel
    {
        public String TestComposeId { get; set; }
        public string QuizId { get; set; }
        public string QuestionId { get; set; }
        public int QuestionLevel { get; set; }
        public decimal QuestionWeight { get; set; }
    }
}
