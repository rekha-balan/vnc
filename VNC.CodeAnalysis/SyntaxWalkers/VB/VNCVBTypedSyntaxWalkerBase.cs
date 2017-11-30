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
        //public Boolean AllTypes = false;
        public Boolean HasAttributes = false;

        //public Boolean IsBoolean = false;
        //public Boolean IsByte = false;
        //public Boolean IsDate = false;
        //public Boolean IsDataTable = false;
        //public Boolean IsDateTime = false;
        //public Boolean IsInt16 = false;
        //public Boolean IsInt32 = false;
        //public Boolean IsInteger = false;
        //public Boolean IsLong = false;
        //public Boolean IsSingle = false;
        //public Boolean IsString = false;

        //public Boolean IsOtherType = false;

        internal bool FilterByType(AsClauseSyntax asClause)
        {
            Boolean addField = false;

            if (asClause == null || Display.AllTypes)
            {
                addField = true;
            }
            else
            {
                switch (asClause.Type().ToString())
                {
                    case "Boolean":
                        if (Display.Boolean) addField = true;
                        break;

                    case "Byte":
                        if (Display.Byte) addField = true;
                        break;

                    case "DataTable":
                        if (Display.DataTable) addField = true;
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
                        if (Display.OtherTypes) addField = true;
                        //if (IsOtherType && !displayStructure) addField = true;

                        break;
                }
            }
            
            return addField;
        }
    }
}
