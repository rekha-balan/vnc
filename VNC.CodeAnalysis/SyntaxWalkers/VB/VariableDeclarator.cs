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
    public class VariableDeclarator : VNCVBTypedSyntaxWalkerBase
    {

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var nodeInitializer = node.Initializer;
            //var asClause = node.AsClause;
            var nodeNames = node.Names;
            var nodeNames2 = nodeNames.First().ToString();

            var asType = node.AsClause.Type().ToString();

            if (identifierNameRegEx.Match(nodeNames.First().ToString()).Success)
            {
                Boolean addField = false;

                var asClause = node.AsClause;
 
                // {Private Shared bCheckedCatSupport = False}
                // TODO(crhodes)
                // Need to handle null asClause

                if (asClause == null)
                {
                    addField = true;
                }
                else
                {
                    var asClauseType = asClause.Type();

                    switch (asClauseType.ToString())
                    {
                        case "Boolean":
                            if (IsBoolean) addField = true;
                            break;

                        case "Date":
                            if (IsDate) addField = true;
                            break;

                        case "DateTime":
                            if (IsDateTime) addField = true;
                            break;

                        case "Int16":
                            if (IsInt16) addField = true;
                            break;

                        case "Int32":
                            if (IsInt32) addField = true;
                            break;

                        case "Integer":
                            if (IsInteger) addField = true;
                            break;

                        case "Long":
                            if (IsLong) addField = true;
                            break;

                        case "Single":
                            if (IsSingle) addField = true;
                            break;

                        case "String":
                            if (IsString) addField = true;
                            break;

                        default:
                            if (IsOtherType) addField = true;
                            //if (IsOtherType && !displayStructure) addField = true;

                            break;
                    }
                }

                if (addField)
                {
                    string messageContext = "";

                    if (DisplayClassOrModuleName)
                    {
                        messageContext = Helpers.VB.GetContainingType(node);
                    }

                    if (DisplayMethodName)
                    {
                        messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
                    }

                    Messages.AppendLine(String.Format("{0} {1}",
                        messageContext,
                        node.ToString()));
                }
            }

            base.VisitVariableDeclarator(node);
        }
    }
}
