using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class VNCVBTypedSyntaxWalkerBase : VNCVBSyntaxWalkerBase
    {
        public Boolean AllTypes = false;
        public Boolean HasAttributes = false;

        public Boolean IsBoolean = false;
        public Boolean IsDate = false;
        public Boolean IsDateTime = false;
        public Boolean IsInt16 = false;
        public Boolean IsInt32 = false;
        public Boolean IsInteger = false;
        public Boolean IsLong = false;
        public Boolean IsSingle = false;
        public Boolean IsString = false;

        public Boolean IsOtherType = false;
    }
}
