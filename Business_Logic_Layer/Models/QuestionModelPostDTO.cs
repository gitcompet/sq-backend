using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionModelPostDTO
    {
        public string DomainId { get; set; }
        public string SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
    }
}
