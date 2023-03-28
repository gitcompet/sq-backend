using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class SubDomainCompose
    {
        public int SubDomainComposeId { get; set; }
        public int ElementId { get; set; }
        public string ElementType { get; set; }
        public int SubDomainId { get; set; }
    }
}
