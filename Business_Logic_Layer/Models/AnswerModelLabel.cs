using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerModelLabel
    {
        public AnswerModelLabel() { }
        public AnswerModelLabel(AnswerModel answerModel, string title, string label)
        {
            this.AnswerId = answerModel.AnswerId;
            this.Comment = answerModel.Comment;
            this.isActive = answerModel.isActive;
            this.isDeleted = answerModel.isDeleted;
            this.Title = title;
            this.Label = label;
        }
        public string AnswerId { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
        //label
        public string Title { get; set; }
        public string Label { get; set; }
    }
}
