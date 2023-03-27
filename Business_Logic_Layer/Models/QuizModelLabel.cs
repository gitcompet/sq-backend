using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizModelLabel
    {
        public QuizModelLabel() { }
        public QuizModelLabel(QuizModel quizModel, string title)
        {
            this.QuizId = quizModel.QuizId;
            this.DomainId = quizModel.DomainId;
            this.SubDomainId = quizModel.SubDomainId;
            this.Weight = quizModel.Weight;
            this.Comment = quizModel.Comment;
            this.isActive = quizModel.isActive;
            this.isDeleted = quizModel.isDeleted;
            this.Title = title;
        }
        public string QuizId { get; set; }
        public string DomainId { get; set; }
        public string SubDomainId { get; set; }
        public decimal Weight { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }

        //label
        public string Title { get; set; }
    }
}
