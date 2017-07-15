using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationWithRoslyn
{
    interface IGreetingProfile
    {
        int Hour { get; set; }
        int Gender { get; set; }
        int MaritalStatus { get; set; }
        int FirstName { get; set; }
        int  LastName { get; set; }
    }
}
