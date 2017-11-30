﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Text.RegularExpressions;

namespace VNC.CodeAnalysis.SyntaxWalkers.VB
{
    public class StructureBlock : VNCVBTypedSyntaxWalkerBase
    {
        /// <summary>
        /// Regular Expression
        /// </summary>
        public string StructureNames;
        /// <summary>
        /// Regular Expression
        /// </summary>
        public string FieldNames;

        public Boolean ShowFields = false;

        public Boolean AllFieldTypes = false;

        private Regex structureNameRegEx;
        private Regex fieldNameRegEx;


        public override void InitializeRegEx()
        {
            try
            {
                structureNameRegEx = new Regex(StructureNames, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                Messages.AppendLine(string.Format("Error in StructureNames RegEx >{0}< Error:({1}), using >.*<", 
                    StructureNames, ex.Message));
                structureNameRegEx = new Regex(".*", RegexOptions.IgnoreCase);
            }

            try
            {
                fieldNameRegEx = new Regex(FieldNames, RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                Messages.AppendLine(string.Format("Error in StructureNames RegEx >{0}< Error:({1}), using >.*<",
                    FieldNames, ex.Message));
                fieldNameRegEx = new Regex(".*", RegexOptions.IgnoreCase);
            }
        }

        public override void VisitStructureBlock(StructureBlockSyntax node)
        {
            //var nodeInitializer = node.Initializer;
            //var nodeAsClause = node.AsClause;
            //var nodeNames = node.Names;
            //var asType = node.AsClause.Type().ToString();

            if (structureNameRegEx.Match(node.StructureStatement.Identifier.Text).Success)
            {
                StringBuilder messageFields = new StringBuilder();
                Boolean displayStructure = AllFieldTypes;

                //Messages.AppendLine(node.StructureStatement.Identifier.Text);
                //message.AppendLine(node.StructureStatement.ToString());

                if (ShowFields)
                {
                    foreach (var field in node.DescendantNodes().OfType<FieldDeclarationSyntax>())
                    {
                        if (! fieldNameRegEx.Match(field.ToString()).Success)
                        {
                            continue;
                        }
                        Boolean addField = false;

                        var ac = field.ChildNodes().OfType<VariableDeclaratorSyntax>();

                        var ac2 = field.ChildNodes().OfType<VariableDeclaratorSyntax>().FirstOrDefault().AsClause;
                        var asClauseType = ac2.Type();
                        var ac4 = asClauseType.GetType() == typeof(Int32);
                        var ac5 = typeof(Int32);
                        var ac6 = ac5.ToString();

                        switch (asClauseType.ToString())
                        {
                            case "Boolean":
                                if (Display.Boolean) addField = true;
                                break;

                            case "Date":
                                if (Display.Date) addField = true;
                                break;

                            case "DateTime":
                                if (Display.DateTime) addField = true;
                                break;

                            case "Int16":
                                if (Display.Int16) addField = true;
                                break;

                            case "Int32":
                                if (Display.Int32) addField = true;
                                break;

                            case "Integer":
                                if (Display.Integer) addField = true;
                                break;

                            case "Long":
                                if (Display.Long) addField = true;
                                break;

                            case "Single":
                                if (Display.Single) addField = true;
                                break;

                            case "String":
                                if (Display.String) addField = true;
                                break;

                            default:
                                    if (Display.OtherTypes && !displayStructure) addField = true;

                                    break;
                        }                            


                        if (HasAttributes)
                        {
                            if (field.AttributeLists.Count > 0)
                            {
                                addField = true;
                            }
                        }

                        ////if (IsOtherType && ! displayField)
                        ////{
                        ////    displayField = true;
                        ////}

                        if (addField)
                        {
                            messageFields.AppendLine(string.Format("  {0}", field.ToString()));
                            displayStructure = true;
                        }
                    }
                }

                if (displayStructure)
                {
                    string messageContext = "";

                    if (Display.ClassOrModuleName)
                    {
                        messageContext = Helpers.VB.GetContainingContext(node, Display);
                    }

                    if (Display.MethodName)
                    {
                        messageContext += string.Format(" Method:({0, -35})", Helpers.VB.GetContainingMethod(node));
                    }

                    Messages.AppendLine(String.Format("{0} {1}",
                        messageContext,
                        node.StructureStatement.ToString()));

                    //Messages.AppendLine(node.StructureStatement.ToString());

                    if (ShowFields)
                    {
                        Messages.AppendLine(messageFields.ToString());
                    }
                }
            }

            // Call base to visit children

            base.VisitStructureBlock(node);
        }
    }
}
