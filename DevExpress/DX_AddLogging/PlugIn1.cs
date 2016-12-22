using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Replacement;

namespace DX_AddLogging
{
    public partial class PlugIn1 : StandardPlugIn
    {
        FileChangeCollection fileChangeCollection = null;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void AddLoggingToMethod(Method method)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("StartLine: >{0}< >{1}<", method.StartLine, method.StartOffset));
            System.Diagnostics.Debug.WriteLine(string.Format("EndLine:   >{0}< >{1}<", method.EndLine, method.EndOffset));
            SourceRange methodBody = method.BlockCodeRange;
            SourcePoint startBody = methodBody.Start;
            SourcePoint endBody = methodBody.End;
            System.Diagnostics.Debug.WriteLine(string.Format("StartBody: >{0}< >{1}<", startBody.Line, startBody.Offset));
            System.Diagnostics.Debug.WriteLine(string.Format("EndBody:   >{0}< >{1}<", endBody.Line, endBody.Offset));

            SourcePoint logEntryPoint = new SourcePoint();
            SourcePoint logExitPoint = new SourcePoint();

            logEntryPoint.Line = startBody.Line;
            logEntryPoint.Offset = 1;

            logExitPoint.Line = method.EndLine - 1;
            logExitPoint.Offset = endBody.Offset;

            try
            {
                SourceFile activeFile = CodeRush.Source.ActiveSourceFile;



                string LOGENTRY = @"
    #If TRACE
        Dim startTicks As Long = Log.Trace(""Enter"", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

";

                string LOGEXIT = @"
    #If TRACE
        Log.Trace(""Exit"", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
";

                //FileChangeCollection fileChangeCollection = new FileChangeCollection();

                FileChange logExitFileChange = new FileChange(activeFile.Name, logExitPoint, LOGEXIT);
                fileChangeCollection.Add(logExitFileChange);

                //method.ParseOnDemandIfNeeded();

                //CodeRush.Source.ParseIfTextChanged();

                FileChange logEntryFileChange = new FileChange(activeFile.Name, logEntryPoint, LOGENTRY);
                fileChangeCollection.Add(logEntryFileChange);

                //CodeRush.Source.ParseIfTextChanged();
                // TODO(crhodes):
                // Need to figure out how to force a reparse.


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private IEnumerable<Method> GetMethods(Class targetClass)
        {
            IEnumerable<Method> methods = (IEnumerable<Method>)targetClass.AllMethods;

            return methods;
        }

        private void actAddLogging_Execute(ExecuteEventArgs ea)
        {
            try
            {
                Class activeClass = CodeRush.Source.ActiveClass;

                //IEnumerable<Method> methods = (IEnumerable<Method>)activeClass.AllMethods;

                fileChangeCollection = new FileChangeCollection();

                foreach (var method in activeClass.AllMethods)
                {
                    Method methodDetails = (Method)method;

                    System.Diagnostics.Debug.WriteLine(string.Format("Method:  Name:>{0}<  Type:>{1}<", methodDetails.Name, methodDetails.MethodType));
                    AddLoggingToMethod((DevExpress.CodeRush.StructuralParser.Method)method);
                }

                //Method activeMethod = CodeRush.Source.ActiveMethod;

                //AddLoggingToMethod(activeMethod);

                CodeRush.File.ApplyChanges(fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}