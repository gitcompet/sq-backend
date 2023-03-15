using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class AnswerQuestion
    {
        public int AnswerQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public Boolean isAnswerOK { get; set; }
    }
}