﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerQuestionModelPostDTO
    {
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
        public Boolean isAnswerOk { get; set; }
    }
}
