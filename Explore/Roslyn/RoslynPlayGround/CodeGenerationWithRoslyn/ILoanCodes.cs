using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    interface ILoanCodes
    {
        /// <summary>
        /// Type of documentation used to verify income
        /// </summary>
        int Code1 { get; set; }
        /// <summary>
        /// Number of Months Bank Statements are available
        /// </summary>
        int Code2 { get; set; }
        /// <summary>
        /// Reason for this loan
        /// </summary>
        int Code3 { get; set; }
        /// <summary>
        /// Property Type
        /// </summary>
        int Code4 { get; set; }
        /// <summary>
        /// Appraisal Type
        /// </summary>
        int Code5 { get; set; }
        /// <summary>
        /// Number of comparable properties included in the appraisal
        /// </summary>
        int Code6 { get; set; }
        /// <summary>
        /// Location
        /// </summary>
        int Code7 { get; set; }
        /// <summary>
        /// Debt to Income Ration
        /// </summary>
        int Code8 { get; set; }
        /// <summary>
        /// Loan to Value Ratio
        /// </summary>
        int Code9 { get; set; }
        /// <summary>
        /// The middle credit score for the primary borrower
        /// </summary>
        int Code10 { get; set; }
    }
}
