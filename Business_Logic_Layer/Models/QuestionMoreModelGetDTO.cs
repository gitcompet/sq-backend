using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionMoreModelGetDTO
    {
        public QuestionMoreModelGetDTO () { }
        public QuestionMoreModelGetDTO(QuestionModel question, IEnumerable<String> answer, Boolean[] answerQuestion)
        {
            this.QuestionId = question.QuestionId;
            this.DomainId = question.DomainId;
            this.SubDomainId = question.SubDomainId;
            this.Level = question.Level;
            this.Weight = question.Weight;
            this.Duration = question.Duration;
            this.Comment = question.Comment;
            this.isActive = question.isActive;
            this.isDeleted = question.isDeleted;
            this.AnswerTextList = answer;
            this.AnswerCheckList = answerQuestion;
        }
        public String QuestionId { get; set; }
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public String Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }

        //Answer
        public IEnumerable<String> AnswerTextList { get; set; }

        //Answer Question
        public Boolean[] AnswerCheckList { get; set; }
    }
}
