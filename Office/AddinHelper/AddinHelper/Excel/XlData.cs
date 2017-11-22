﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using XLDataReader = ExcelDataReader;    // DataReaderExcel
using ExcelDataReader;
using ExcelDataReader.Core;
using ExcelDataReader.Exceptions;

using XL = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Windows;

namespace VNC.AddinHelper
{
    public partial class Excel
    {
        public class XlData
        {
            // Fields...
            #region Fields and Properties

            private string _Path;

            public string Path
            {
                get { return _Path; }
                set
                {
                    _Path = value;
                }
            }

            #endregion

            #region Constructors and Load
            
            /// <summary>
            /// Initializes a new instance of the <see cref="XlData"/> class.
            /// </summary>
            /// <param name="path"></param>
            public XlData(string path)
            {
                _Path = path;
            }

            #endregion

            #region Main Function Routines

            private DataTable GetExcelTable(string sheetName, string tableName)
            {
                DataTable dt = null;

                XL.Application xlApp = new XL.Application();

                XL.Workbook wb = xlApp.Workbooks.Open(Path);
                XL.Worksheet ws = wb.Sheets[sheetName];
                XL.ListObject lo = ws.ListObjects[tableName];
                XL.ListColumns listColumns = lo.ListColumns;
                XL.ListRows listRows = lo.ListRows;

                wb.Close();

                return dt;
            }

            public IExcelDataReader GetExcelReader()
            {
                IExcelDataReader reader = null;

                using (FileStream stream = File.Open(Path, FileMode.Open, FileAccess.Read))
                {

                    try
                    {
                        if (Path.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        return reader;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            public IEnumerable<string> GetWorkSheetNames()
            {
                using (var reader = this.GetExcelReader())
                {
                    var workbook = reader.AsDataSet();
                    var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;

                    return sheets;
                }
            }

            public IEnumerable<DataRow> GetData(string sheetName, bool firstRowIsColumnNames = true)
            {
                using (var reader = this.GetExcelReader())
                {
                    //reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
                    var workSheet = reader.AsDataSet().Tables[sheetName];
                    var rows = from DataRow row in workSheet.Rows select row;

                    return rows;
                }
            }

            #endregion

        }
    }
}
