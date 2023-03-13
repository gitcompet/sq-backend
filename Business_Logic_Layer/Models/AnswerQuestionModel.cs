using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerQuestionModel
    {
        public String AnswerQuestionId { get; set; }
        public String QuestionId { get; set; }
        public String AnswerId { get; set; }
        public Boolean isAnswerOk { get; set; }
    }
}
