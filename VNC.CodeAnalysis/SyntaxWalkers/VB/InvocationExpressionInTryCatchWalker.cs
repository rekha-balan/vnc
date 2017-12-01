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
    public class InvocationExpressionInTryCatch : VNCVBTypedSyntaxWalkerBase
    {
        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (identifierNameRegEx.Match(node.Expression.ToString()).Success)
            {
                RecordMatchAndContext(node, node.ToString());

                //string nodeValue = node.ToString();

                //Messages.AppendLine(String.Format("{0} {1}",
                //    GetContext(node),
                //    nodeValue));

                //if (Matches.ContainsKey(nodeValue))
                //{
                //    Matches[nodeValue] += 1;
                //}
                //else
                //{
                //    Matches.Add(nodeValue, 1);
                //}
            }

            // Call base to visit children

            base.VisitInvocationExpression(node);
        }

        //public string GetContext(VisualBasicSyntaxNode node)
        //{
        //    string messageContext = "";

        //    //if (Display.ClassOrModuleName)
        //    //{
        //        messageContext = ContainingType(node);
        //    //}

        //    //if (Display.MethodName)
        //    //{
        //    //    messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
        //    //}

        //    return messageContext;
        //}

    }
}
