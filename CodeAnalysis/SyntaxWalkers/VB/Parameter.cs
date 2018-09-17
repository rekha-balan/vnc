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
    public class Parameter : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitParameter(ParameterSyntax node)
        {
            if (_targetPatternRegEx.Match(node.Identifier.ToString()).Success)
            {
                if (FilterByType(node.AsClause))
                {
                    string messageContext = "";

                    if (_configurationOptions.ClassOrModuleName)
                    {
                        messageContext = Helpers.VB.GetContainingContext(node, _configurationOptions);
                    }

                    if (_configurationOptions.MethodName)
                    {
                        messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethodName(node));
                    }

                    // TODO(crhodes)
                    // Think through if it would make sense to have parameters as a key

                    Messages.AppendLine(String.Format("{0} {1}",
                        messageContext,
                        node.ToString()));
                }
            }

            base.VisitParameter(node);
        }
    }
}
