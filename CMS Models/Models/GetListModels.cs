using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class GetListModel
    {
        public string tableName { get; set; }
        public string keyColumn { get; set; }
        public string valueColumn { get; set; }
        public string orderBy { get; set; }
    }

    public class ListModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
