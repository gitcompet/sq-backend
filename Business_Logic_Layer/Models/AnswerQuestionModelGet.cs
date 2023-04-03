using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerQuestionModelGet
    {
        public AnswerQuestionModelGet(AnswerQuestionModel answerQuestionModel)
        {
            this.QuestionId = answerQuestionModel.QuestionId;
            this.AnswerQuestionId = answerQuestionModel.AnswerQuestionId;
            this.AnswerId = answerQuestionModel.AnswerId;
        }
        public string AnswerQuestionId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
    }
}
