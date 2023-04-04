using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerQuestionModelLabel
    {
        public AnswerQuestionModelLabel() { }
        public AnswerQuestionModelLabel(AnswerQuestionModel answerAnswerQuestionModel, string label)
        {
            this.AnswerQuestionId = answerAnswerQuestionModel.AnswerQuestionId;
            this.QuestionId = answerAnswerQuestionModel.QuestionId;
            this.AnswerId = answerAnswerQuestionModel.AnswerId;
            this.isAnswerOk = answerAnswerQuestionModel.isAnswerOk;
            this.Label = label;
        }
        public string AnswerQuestionId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public Boolean isAnswerOk { get; set; }

        //label
        public string Label { get; set; }
    }
}
