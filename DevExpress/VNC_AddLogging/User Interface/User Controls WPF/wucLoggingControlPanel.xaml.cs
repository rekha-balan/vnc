using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Replacement;


namespace VNC_AddLogging.User_Interface.User_Controls_WPF
{
    /// <summary>
    /// Interaction logic for wucLoggingControlPanel.xaml
    /// </summary>
    public partial class wucLoggingControlPanel : Grid
    {
        #region Enums, Fields, Properties, Structures

        private FileChangeCollection fileChangeCollection = null;

        #endregion

        #region Constructors and Load

        public wucLoggingControlPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void btnAddLoggingToClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Class activeClass = CodeRush.Source.ActiveClass;

                fileChangeCollection = new FileChangeCollection();

                foreach (Method method in activeClass.AllMethods)
                {
                    AddLoggingToMethod(method);
                }

                CodeRush.File.ApplyChanges(fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();
                fileChangeCollection = null;
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString());
            }
        }

        private void btnAddLoggingToMethod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fileChangeCollection = new FileChangeCollection();

                Method activeMethod = CodeRush.Source.ActiveMethod;
                AddLoggingToMethod(activeMethod);

                CodeRush.File.ApplyChanges(fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();

                fileChangeCollection = null;
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString());
            }
        }

        private void btnAddLoggingToModule_Click(object sender, RoutedEventArgs e)
        {
        //    try
        //    {
        //        Class activeClass = CodeRush.Source.ActiveClass;

        //        fileChangeCollection = new FileChangeCollection();

        //        foreach (var method in activeClass.AllMethods)
        //        {
        //            Method methodDetails = (Method)method;

        //            System.Diagnostics.Debug.WriteLine(string.Format("Method:  Name:>{0}<  Type:>{1}<", methodDetails.Name, methodDetails.MethodType));
        //            AddLoggingToMethod((DevExpress.CodeRush.StructuralParser.Method)method);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello Logging");
        }

        #endregion

        private string GetLogEntryText()
        {
            string message = @"
    #If TRACE
        Dim startTicks As Long = Log.Trace9(""Enter"", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

";
            return message;
        }

        private string GetLogExitText()
        {
            string message = @"

    #If TRACE
        Log.Trace9(""Exit"", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
";
            return message;
        }
        #region Main Methods

        private void AddLoggingToMethod(Method method)
        {
            if (method == null)
            {
                return;
            }

            try
            {
                SourceRange methodBody = method.BlockCodeRange;
                SourcePoint startBody = methodBody.Start;
                SourcePoint endBody = methodBody.End;

                Helper.WriteToDebugWindow(string.Format("Method Type:>{0}<", method.MethodType));

                Helper.WriteToDebugWindow(string.Format("Method Line:>{0}< Offset:>{1}<", method.StartLine, method.StartOffset));

                Helper.WriteToDebugWindow(string.Format("  Body Line:>{0}< >{1}<", startBody.Line, startBody.Offset));
                Helper.WriteToDebugWindow(string.Format("  Body Line:>{0}< >{1}<", endBody.Line, endBody.Offset));

                Helper.WriteToDebugWindow(string.Format("Method Line:>{0}< Offset:>{1}<", method.EndLine, method.EndOffset));

                SourcePoint logEntryPoint = new SourcePoint();
                SourcePoint logExitPoint = new SourcePoint();

                logEntryPoint.Line = startBody.Line;
                logEntryPoint.Offset = 1;

                // If we are in a function, searchbackward through the method body looking for the first return.

                if (method.MethodType == MethodTypeEnum.Function)
                {
                    logExitPoint.Line = method.EndLine - 1;
                    logExitPoint.Offset = endBody.Offset;
                }
                else
                {
                    var returnLine = method.FindChildByElementType(LanguageElementType.Return);
                    Helper.WriteToDebugWindow(string.Format("  Return Start Line:>{0}< Offset:>{1}<", returnLine.StartLine, returnLine.StartOffset));
                    Helper.WriteToDebugWindow(string.Format("  Return End   Line:>{0}< Offset:>{1}<", returnLine.EndLine, returnLine.EndOffset));

                    logExitPoint.Line = method.EndLine - 1;
                    logExitPoint.Offset = endBody.Offset;
                }

                if (startBody.Line == endBody.Line)
                {
                    Helper.WriteToDebugWindow("Empty method body, skipping.");
                    return;
                }

                SourceFile activeFile = CodeRush.Source.ActiveSourceFile;

                Helper.WriteToDebugWindow(string.Format("Entry Insert Line:>{0}< Offset:>{1}<", logEntryPoint.Line, logEntryPoint.Offset));

                FileChange logEntryFileChange = new FileChange(activeFile.Name, logEntryPoint, GetLogEntryText());
                fileChangeCollection.Add(logEntryFileChange);

                Helper.WriteToDebugWindow(string.Format("Exit  Insert Line:>{0}< Offset:>{1}<", logExitPoint.Line, logExitPoint.Offset));

                FileChange logExitFileChange = new FileChange(activeFile.Name, logExitPoint, GetLogExitText());
                fileChangeCollection.Add(logExitFileChange);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString());
            }
        }

        #endregion
    }
}
