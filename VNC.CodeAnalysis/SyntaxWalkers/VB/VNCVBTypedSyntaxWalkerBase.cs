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

            if (asClause == null || _configurationOptions.AllTypes)
            {
                addField = true;
            }
            else
            {
                switch (asClause.Type().ToString())
                {
                    case "Boolean":
                        if (_configurationOptions.Boolean) addField = true;
                        break;

                    case "Byte":
                        if (_configurationOptions.Byte) addField = true;
                        break;

                    case "DataTable":
                        if (_configurationOptions.DataTable) addField = true;
                        break;

                    case "Date":
                        if (_configurationOptions.Date) addField = true;
                        break;

                    case "DateTime":
                        if (_configurationOptions.DateTime) addField = true;
                        break;

                    case "Int16":
                        if (_configurationOptions.Int16) addField = true;
                        break;

                    case "Int32":
                        if (_configurationOptions.Int32) addField = true;
                        break;

                    case "Integer":
                        if (_configurationOptions.Integer) addField = true;
                        break;

                    case "Long":
                        if (_configurationOptions.Long) addField = true;
                        break;

                    case "Single":
                        if (_configurationOptions.Single) addField = true;
                        break;

                    case "String":
                        if (_configurationOptions.String) addField = true;
                        break;

                    default:
                        if (_configurationOptions.OtherTypes) addField = true;
                        //if (IsOtherType && !displayStructure) addField = true;

                        break;
                }
            }
            
            return addField;
        }
    }
}
