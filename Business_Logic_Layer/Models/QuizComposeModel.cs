using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizComposeModel
    {
        public QuizComposeModel() { }
        public QuizComposeModel(QuizComposeModelPostDTO quizComposeModelPostDTO)
        {
            this.QuizId = quizComposeModelPostDTO.QuizId;
            this.QuestionId = quizComposeModelPostDTO.QuestionId;
            this.QuestionLevel = quizComposeModelPostDTO.QuestionLevel;
            this.QuestionWeight = quizComposeModelPostDTO.QuestionWeight;
        }
        public String QuizComposeId { get; set; }
        public string QuizId { get; set; }
        public string QuestionId { get; set; }
        public int QuestionLevel { get; set; }
        public decimal QuestionWeight { get; set; }
    }
}
