using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class AnonQuizScore
    {
        public int AnonQuizScoreId { get; set; }
        public int QuizId { get; set; }
        public decimal Score { get; set; }
    }
}
