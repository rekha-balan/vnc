using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    public class EnumTypeItem
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsRange { get; set; }
        public int ID { get; set; }
        public List<EnumTypeDetailItem> Details { get; set; }
    }
}
