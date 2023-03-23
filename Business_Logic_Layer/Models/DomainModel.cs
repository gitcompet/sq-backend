using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class DomainModel
    {
        public DomainModel() { }
        public DomainModel(DomainModelPostDTO domainModelPostDTO)
        {
            if (domainModelPostDTO.Name != null)
            {
                this.Name = domainModelPostDTO.Name;
            }
            else
            {
                this.Name = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public string DomainId { get; set; }
        public string Name { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
