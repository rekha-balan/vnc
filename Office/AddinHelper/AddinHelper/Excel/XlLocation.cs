using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace VNC.AddinHelper
{
    public partial class Excel
    {
        /// <summary>
        /// Cursor based support for adding information to a worksheet
        /// </summary>
        public class XlLocation
        {
            // TODO(crhodes):
            //	Need to revisit all the offset stuff. It is not clean.  Lots of conditional
            // -1 stuff.  Ugh

            //public int RowStart;
            public int RowOffset = 0;
            public int RowOffsetMax= 0;

            //public int ColumnStart;
            public int ColumnOffset = 0;
            public int ColumnOffsetMax = 0;

            public int RowsAdded = 0;
            public int ColumnsAdded = 0;

            private Range startRange = null;
            private Range currentRange = null;

            public int GroupStartRow = 0;
            public int GroupStartColumn = 0;
            public int GroupEndRow = 0;
            public int GroupEndColumn = 0;

            public bool OrientVertical = true;

            public int MarkStartRow = 0;
            public int MarkStartColumn = 0;
            public int MarkEndRow = 0;
            public int MarkEndColumn = 0;

            public int TableStartRow = 0;
            public int TableStartColumn = 0;
            public int TableEndRow = 0;
            public int TableEndColumn = 0;

            public Worksheet workSheet = null;

            #region Constructors

            public XlLocation(int row, int column)
            {
                // TODO(crhodes):
                //	Not sure this makes sense.  May need to always specify a Worksheet
                //RowStart = row;
                //ColumnStart = column;
            }

            public XlLocation(Worksheet ws, int row, int column)
                : this(row, column)
            {
                workSheet = ws;
                startRange = ws.Cells[row, column];
                currentRange = startRange;
            }

            public XlLocation(Worksheet ws, int row, int column, bool orientVertical)
                : this(row, column)
            {
                workSheet = ws;
                startRange = ws.Cells[row, column];
                currentRange = startRange;
                OrientVertical = orientVertical;
            }

            #endregion

            #region Main Methods

            public Range AddColumn(int columns = 1)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // currentRange is currently where it should be.
                Range rngOutput = currentRange;

                // Update to reflect new row.
                ColumnsAdded += columns;
                currentRange = currentRange.Offset[0, columns];

                ColumnOffset = columns;
                //ColumnOffsetMax = ColumnOffset;

                if (columns > ColumnsAdded)
                {
                    ColumnsAdded = columns;
                }

                return rngOutput;
            }

            public Range AddOffsetColumn()
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                Range rngOutput = currentRange.Offset[0, ColumnOffset++];

                if (ColumnOffsetMax < ColumnOffset)
                {
                    ColumnOffsetMax = ColumnOffset;
                }
                
                if (ColumnOffset > ColumnsAdded)
                {
                    ColumnsAdded = ColumnOffset;
                }

                return rngOutput;
            }

            public Range AddOffsetRow()
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                Range rngOutput = currentRange.Offset[RowOffset++, 0];

                if (RowOffset > RowsAdded)
                {
                    RowsAdded = RowOffset;
                }

                return rngOutput;
            }

            // Should this be AddColumnsToRow?
            // Should column offset be 0 or reflect how many columns have been added?

            // TODO(crhodes):
            //	Maybe we should just use ColumnOffset

            public Range AddRow(int columns = 0)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // currentRange is currently where it should be.
                Range rngOutput = currentRange;

                // Update to reflect new row.
                RowsAdded++;
                currentRange = currentRange.Offset[1, 0];

                ColumnOffset = columns;

                if (columns > ColumnsAdded)
                {
                    ColumnsAdded = columns;
                }

                if (ColumnOffsetMax < ColumnOffset)
                {
                    ColumnOffsetMax = ColumnOffset;
                }

                return rngOutput;
            }

            public void ClearOffsets(Boolean clearOffsetMax = false)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // HACK(crhodes)
                // 
                RowOffset = 0;
                ColumnOffset = 0;

                if (clearOffsetMax)
                {
                    RowOffsetMax = 0;
                    ColumnOffsetMax = 0;
                }

            }

            public void CreateTable(string tableName)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                Worksheet ws = currentRange.Worksheet;

                if (IsValidTableRange(TableStartRow, TableStartColumn, TableEndRow, TableEndColumn))
                {
                    ws.ListObjects.Add(XlListObjectSourceType.xlSrcRange,
                        ws.Range[
                            ws.Cells[TableStartRow, TableStartColumn],
                            ws.Cells[TableEndRow, TableEndColumn]],
                        Type.Missing,
                        XlYesNoGuess.xlYes).Name = tableName;
                }
            }

            public int ColumnCurrent()
            {
                return currentRange.Column;
            }


            public int ColumnStart()
            {
                return startRange.Column;
            }

            public void DecrementColumns(int count = 1)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // TODO(crhodes):
                ////	Decide if need to add to ColumnsAdded or just adjust range.

                //ColumnsAdded -= count;

                currentRange = currentRange.Offset[0, -count];
            }

            public void DecrementRows(int count = 1)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // TODO(crhodes):
                //	Decide if need to add to RowsAdded or just adjust range.

                //RowsAdded -= count;
                currentRange = currentRange.Offset[-count, 0];
            }

            public void EndSectionAndSetNextLocation(bool orientVertical)
            {
                if (!orientVertical)
                {
                    // Skip past the info just added.
                    SetLocation(RowStart(), TableEndColumn + 1);
                }
            }

            public Range GetCurrentRange()
            {
                return currentRange;
            }

            public Range GetStartRange()
            {
                return startRange;
            }

            public void Group(bool orientVertical, bool hide = true)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);
                
                if (orientVertical)
                {
                    Excel.GroupAndHideRows(workSheet, GroupStartRow, GroupEndRow, hide);
                }
                else
                {
                    Excel.GroupAndHideColumns(workSheet, GroupStartColumn, GroupEndColumn, hide);
                }                
            }

            public void IncrementColumns(int count = 1)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // TODO(crhodes):
                //	Decide if need to add to ColumnsAdded or just adjust range.
                // Seems odd that Increment adds to ColumnsAdded but Decrement does not.
                ColumnsAdded += count;
                //ColumnOffsetMax += count;
                currentRange = currentRange.Offset[0, count];
            }

            public void IncrementRows(int count = 1)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // TODO(crhodes):
                //	Decide if need to add to RowsAdded or just adjust range.

                RowsAdded += count;
                currentRange = currentRange.Offset[count, 0];
            }

            //public void Initialize()
            //{
            //    RowOffset = 0;
            //    ColumnOffset = 0;
            //    RowsAdded = 0;
            //    ColumnsAdded = 0;
            //}

            public Range InsertRow(int columns)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                // currentRange is aleady where it should be.
                //Range rngOutput = currentRange;
                ////RowsAdded++;
                ////Range rngOutput = currentRange.Offset[RowsAdded++, 0];
                //currentRange = currentRange.Worksheet.Cells[CurrentRow(), CurrentColumn()];

                if (columns > ColumnsAdded)
                {
                    ColumnsAdded = columns;
                }

                return currentRange;
            }

            public void MarkStart(Excel.MarkType type = MarkType.None)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                // Seems like we always want to do this.
                // 
                ClearOffsets(clearOffsetMax: true);

                Excel.DisplayInWatchWindow(caller.Name, this);

                switch (type)
                {
                    case Excel.MarkType.None:
                        MarkStartRow = currentRange.Row;
                        MarkStartColumn = currentRange.Column;

                        break;

                    case Excel.MarkType.Group:
                        GroupStartRow = currentRange.Row;
                        GroupStartColumn = currentRange.Column;

                        break;

                    case Excel.MarkType.Table:
                        TableStartRow = currentRange.Row;
                        TableStartColumn = currentRange.Column;

                        break;

                    case Excel.MarkType.GroupTable:
                        GroupStartRow = currentRange.Row;
                        GroupStartColumn = currentRange.Column;
                        TableStartRow = currentRange.Row;
                        TableStartColumn = currentRange.Column;
                   
                        break;
                }

                Excel.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, this, "End");

            }

            public void MarkEnd(Excel.MarkType type = MarkType.None, string tableName = null)
            {
                StackFrame frame = new StackFrame(1);
                MethodBase caller = frame.GetMethod();

                Excel.DisplayInWatchWindow(caller.Name, this);

                switch (type)
                {
                    case Excel.MarkType.None:
                        MarkEndRow = currentRange.Row + RowsAdded;
                        MarkEndColumn = currentRange.Column + ColumnsAdded;

                        break;

                    case Excel.MarkType.Group:
                        GroupEndRow = currentRange.Row + RowOffset - 1;
                        GroupEndColumn = currentRange.Column + ColumnOffsetMax;

                        if (ColumnOffsetMax != 0)
                        {
                            // We have used ColumnOffsetMax and it was 
                            // incremented past end
                        	GroupEndColumn--;
                        }

                        break;

                    case Excel.MarkType.Table:
                        TableEndRow = currentRange.Row + RowOffset - 1;
                        //TableEndColumn = currentRange.Column + ColumnOffset - 1;
                        TableEndColumn = currentRange.Column + ColumnOffsetMax - 1;

                        if (tableName != null)
                        {
                        	CreateTable(tableName);
                        }

                        break;

                    case Excel.MarkType.GroupTable:
                        GroupEndRow = currentRange.Row + RowOffset - 1;
                        GroupEndColumn = currentRange.Column + ColumnOffsetMax;

                        if (ColumnOffsetMax != 0)
                        {
                            // We have used ColumnOffsetMax and it was 
                            // incremented past end
                        	GroupEndColumn--;
                        }

                        TableEndRow = currentRange.Row + RowOffset - 1;
                        //TableEndColumn = currentRange.Column + ColumnOffset - 1;
                        TableEndColumn = currentRange.Column + ColumnOffsetMax - 1;

                        if (tableName != null)
                        {
                            CreateTable(tableName);
                        }

                        break;
                }

                Excel.DisplayInWatchWindow(System.Reflection.MethodInfo.GetCurrentMethod().Name, this, "End");

            }

            /// <summary>
            /// Increment insertion point based on layout orientation.
            /// TODO(crhodes): Handle all four positions in future.
            /// </summary>
            /// <param name="orientVertical"></param>
            public void IncrementPosition(bool orientVertical)
            {
                if (orientVertical)
                {
                    IncrementRows();
                }
                else
                {
                    IncrementColumns();
                }
            }

            public int RowCurrent()
            {
                return currentRange.Row;
            }

            public int RowStart()
            {
                return startRange.Row;
            }

            public void SetRow(int row)
            {
                currentRange = workSheet.Cells[row, currentRange.Column];
            }

            public void SetColumn(int column)
            {
                currentRange = workSheet.Cells[currentRange.Row, column];    
            }

            public void SetLocation(int row, int column)
            {
                currentRange = workSheet.Cells[row, column];
            }

            public void UpdateOffsets()
            {
                int currentColumnOffset = ColumnCurrent() - GroupStartColumn + 1;

                if (currentColumnOffset > ColumnOffsetMax)
                {
                    ColumnOffsetMax = currentColumnOffset;
                }

                int currentRowOffset = RowCurrent() - GroupStartRow + 1;

                if (currentRowOffset > RowOffsetMax)
                {
                    RowOffsetMax = currentRowOffset;
                }
            }

            #endregion

            #region Private Methods
            
            private bool IsValidTableRange(int startRow, int startColumn, int endRow, int endColumn)
            {
                bool isValid = false;

                // Need to have at least one row (+ header) and one column in table.

                if (startRow > 0 && startColumn > 0
                    && (endRow - startRow) > 0
                    && (endColumn - startColumn) >= 0
                    )
                {
                    isValid = true;
                }

                return isValid;
            }

            #endregion

        }
    }
}