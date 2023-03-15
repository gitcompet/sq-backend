using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionModel
    {
        public String QuestionId { get; set; }
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public DateTime Time { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
