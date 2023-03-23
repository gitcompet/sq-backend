using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionMoreModelPutDTO
    {
        public string QuestionId { get; set; }
        public string DomainId { get; set; }
        public string SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }

        //Answer
        public string[] AnswerTextList { get; set; }

        //Answer Question
        public string[] AnswerCheckList { get; set; }
    }
}
