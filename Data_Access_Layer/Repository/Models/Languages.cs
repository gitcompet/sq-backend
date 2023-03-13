using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class Languages
    {
        public int LanguagesID { get; set; }
        public string title { get; set; }
        public string shortCode { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isAppRelated { get; set; }
        public Boolean isTestRelated { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
