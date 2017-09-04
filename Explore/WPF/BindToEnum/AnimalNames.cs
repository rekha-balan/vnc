using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNC.Xaml.Enums;
using System.ComponentModel;

namespace BindToEnum
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum AnimalNames
    {
        [Description("This is a bird")]
        Bird,
        [Description("This is a cat")]
        Cat,
        [Description("This is a Dog")]
        Dog
    }
}
