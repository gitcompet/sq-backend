using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerQuestionModel
    {
        public AnswerQuestionModel() { }
        public AnswerQuestionModel(AnswerQuestionModelPostDTO answerQuestionModelPostDTO)
        {
            this.QuestionId = answerQuestionModelPostDTO.QuestionId;
            this.AnswerId = answerQuestionModelPostDTO.AnswerId;
            this.isAnswerOk = answerQuestionModelPostDTO.isAnswerOk;
        }
        public string AnswerQuestionId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public Boolean isAnswerOk { get; set; }
    }
}
