using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizModelPostDTO
    {
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public decimal Weight { get; set; }
        public String Comment { get; set; }
    }
}
