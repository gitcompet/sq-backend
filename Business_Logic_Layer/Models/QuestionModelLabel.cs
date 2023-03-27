using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionModelLabel
    {
        public QuestionModelLabel() { }
        public QuestionModelLabel(QuestionModel questionModel, string title, string label)
        {
            this.QuestionId = questionModel.QuestionId;
            this.DomainId = questionModel.DomainId;
            this.SubDomainId = questionModel.SubDomainId;
            this.Level = questionModel.Level;
            this.Weight = questionModel.Weight;
            this.Duration = questionModel.Duration;
            this.Comment = questionModel.Comment;
            this.Title = title;
            this.Label = label;
            this.isActive = questionModel.isActive;
            this.isDeleted = questionModel.isDeleted;
        }
        public string QuestionId { get; set; }
        public string DomainId { get; set; }
        public string SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }

        //label
        public string Title { get; set; }
        public string Label { get; set; }
    }
}
