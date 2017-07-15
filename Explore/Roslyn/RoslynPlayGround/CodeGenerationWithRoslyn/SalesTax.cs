using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    class SalesTax
    {
        float CalculateSalesTax(string stateAbbrev)
        {
            float salesTax = 0.0F;

            switch (stateAbbrev)
            {
                case "AL":
                    // Sales tax for Alabama
                    salesTax = 0.01F;
                    break;

                case "FL":
                    salesTax = 0.02F;
                    break;

                case "GA":
                    salesTax = 0.015F;
                    break;

                default:
                    break;
            }

            return salesTax;
        }
    }
}
