using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class SchoolModel 
    {     
        public string id_offline { get; set; }
        public string id_online { get; set; }
        public string EducationKey { get; set; }
        public string License { get; set; }
        public string database_id { get; set; }
        public string subdomain { get; set; }
        public string domain { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string theme { get; set; }
        public DateTime? created_on { get; set; }
    }
}
