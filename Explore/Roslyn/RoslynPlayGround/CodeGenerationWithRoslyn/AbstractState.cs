using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    abstract class AbstractState
    {
        float salesTax;
        public float SalesTax
        {
            get { return salesTax; }
            set
            {
                salesTax = value;
            }
        }
        
    }
}
