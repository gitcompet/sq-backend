using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionUserModelHidden
    {
        public QuestionUserModelHidden() { }
        public QuestionUserModelHidden(QuestionUserModel questionUserModel)
        {
            this.QuestionId = questionUserModel.QuestionId;
            this.QuizUserId = questionUserModel.QuizUserId;
            this.QuestionUserId = questionUserModel.QuestionUserId;
        }
        public string QuestionUserId { get; set; }
        public string QuestionId { get; set; }
        public string QuizUserId { get; set; }
        public DateTime MaxValidationDate { get; set; }
    }
}
