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
    public class VariableDeclarator : VisualBasicSyntaxWalker
    {
        public StringBuilder Messages;
        public Boolean DisplayClassOrModuleName;
        public Boolean DisplayMethodName;

        public string IdentifierNames;

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

        private Regex identifierNameRegEx;

        public VariableDeclarator() : base(SyntaxWalkerDepth.StructuredTrivia)
        {

        }

        public void InitializeRegEx()
        {
            try
            {
                identifierNameRegEx = new Regex(IdentifierNames, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                Messages.AppendLine(string.Format("Error in IdentifierNames RegEx >{0}< Error:({1}), using >.*<",
                    IdentifierNames, ex.Message));
                identifierNameRegEx = new Regex(".*", RegexOptions.IgnoreCase);
            }
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var nodeInitializer = node.Initializer;
            //var asClause = node.AsClause;
            var nodeNames = node.Names;
            var nodeNames2 = nodeNames.First().ToString();

            var asType = node.AsClause.Type().ToString();

            if (identifierNameRegEx.Match(nodeNames.First().ToString()).Success)
            {
                var asClause = node.AsClause;
                var asClauseType = asClause.Type();
                Boolean addField = false;

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

                if (addField)
                {
                    string messageContext = "";

                    if (DisplayClassOrModuleName)
                    {
                        // HACK(crhodes)
                        // Figure out how to get Helpers to work

                        //messageContext = Helpers.VB.GetContainingType(node);
                    }

                    if (DisplayMethodName)
                    {
                        // HACK(crhodes)
                        // Figure out how to get Helpers to work

                        //messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
                    }

                    Messages.AppendLine(String.Format("{0} {1}",
                        messageContext,
                        node.ToString()));

                    //Messages.AppendLine(string.Format("  {0}", node.ToString()));
                    //displayStructure = true;
                }

                //if (HasAttributes)
                //{
                //    if (node.AttributeLists.Count > 0)
                //    {
                //        addField = true;
                //    }
                //}
            }

            //if (node.Expression.ToString() == _pattern)
            //{
            //    Messages.AppendLine(node.ToString());
            //}

            // Call base to visit children

            base.VisitVariableDeclarator(node);
        }
    }
}
