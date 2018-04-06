using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNC.Xaml.Enums;

namespace VNC.CodeAnalysis.SyntaxNode
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum WalkerDepth
    {
        [Description("StructuredTrivia")]
        StructuredTrivia,
        [Description("Node")]
        Node,
        [Description("Token")]
        Token,
        [Description("Trivia")]
        Trivia
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum FieldDeclarationLocation
    {
        [Description("Declared in Class")]
        Class,
        [Description("Declared in Module")]
        Module,
        [Description("Declared in Structure")]
        Structure
    }
}
