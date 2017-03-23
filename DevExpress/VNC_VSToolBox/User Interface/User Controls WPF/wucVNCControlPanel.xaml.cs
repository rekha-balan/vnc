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
using DevExpress.DXCore.Constants;


namespace VNC_VSToolBox.User_Interface.User_Controls_WPF
{
    /// <summary>
    /// Interaction logic for wucLoggingControlPanel.xaml
    /// </summary>
    public partial class wucVNCControlPanel : Grid
    {
        #region Enums, Fields, Properties, Structures

        private FileChangeCollection _fileChangeCollection = null;

        #endregion

        #region Constructors and Load

        public wucVNCControlPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void btnAddImportToProject_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes):
            // Get this from the UI

            AddImportToProject("EaseCore");
        }

        private void btnAddLoggingToClass_Click(object sender, RoutedEventArgs e)
        {
            AddLoggingToClass();
        }

        private void btnAddLoggingToMethod_Click(object sender, RoutedEventArgs e)
        {
            AddLoggingToActiveMethod();
        }

        private void btnAddLoggingToModule_Click(object sender, RoutedEventArgs e)
        {
            AddLoggingToActiveModule();
        }

        private void btnAddLoggingToProject_Click(object sender, RoutedEventArgs e)
        {
            AddLoggingToProject();
        }

        private void btnAddLoggingToSolution_Click(object sender, RoutedEventArgs e)
        {
            AddLoggingToSolution();
        }

        private void btnAddParametersToEnterTrace_Click(object sender, RoutedEventArgs e)
        {
            AddParametersToEnterTrace();
        }

        private void btnAddParametersToExitTrace_Click(object sender, RoutedEventArgs e)
        {
            AddParametersToExitTrace();
        }

        private void btnCountClipboardToExitTrace_Click(object sender, RoutedEventArgs e)
        {
            AddClipboardToExitTrace(UseCount: true);
        }

        private void btnDisplayFileNodeInfo_Click(object sender, RoutedEventArgs e)
        {
            SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

            Helper.WriteToDebugWindow(string.Format("File: >{0}<", sourceFile.Name), Helper.DebugDisplay.Always);

            DisplayNodeInfo(sourceFile.Nodes, 4);
        }

        private void btnDisplayMethodInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayMethodInfo();
        }

        private void btnDisplayProjectDetailInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayProjectDetailInfo();
        }

        private void btnInsertDefaultRegions_Click(object sender, RoutedEventArgs e)
        {
            InsertDefaultRegions();
        }

        private void btnInsertLoggingProperties_Click(object sender, RoutedEventArgs e)
        {
            InsertLoggingProperties();
        }

        private void btnClearListBox_Click(object sender, RoutedEventArgs e)
        {
            this.lbDebugWindow.Items.Clear();
        }

        private void btnDisplayContextInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayContextInfo();
        }

        private void btnDisplayProjectSummaryInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayProjectSummaryInfo();
        }

        private void btnDisplaySourceFileInfo_Click(object sender, RoutedEventArgs e)
        {
            SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

            DisplaySourceFileInfo(sourceFile, 0);
        }

        private void btnDisplaySolutionInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplaySolutionInfo();
        }

        private void btnImportsEaseCore_Click(object sender, RoutedEventArgs e)
        {
            ImportsEaseCore();
        }

        private void btnInstrumentClass_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes):
            // Should do some checks to validate in Class, etc.
            InsertLoggingProperties();
            AddLoggingToClass();
            ImportsEaseCore();
        }

        private void btnInstrumentModule_Click(object sender, RoutedEventArgs e)
        {
            // TODO(crhodes):
            // Should do some checks to validate in Module, etc.
            InsertLoggingProperties();
            AddLoggingToActiveModule();
            ImportsEaseCore();
        }

        private void btnClipboardToExitTrace_Click(object sender, RoutedEventArgs e)
        {
            AddClipboardToExitTrace(UseCount: false);
        }
        #endregion

        #region Main Methods

        private void AddImportToProject(string nameSpace)
        {
            try
            {
                ProjectElement project = CodeRush.Source.ActiveProject;

                if (null == project)
                {
                    Helper.WriteToDebugWindow("No Active Project, exiting", Helper.DebugDisplay.Always);
                    return;
                }

                Helper.WriteToDebugWindow(string.Format("Adding Import To Project ({0})", project.Name), Helper.DebugDisplay.Always);

                FileChangeCollection fileChangeCollection = new FileChangeCollection();

                foreach (SourceFile sourceFile in project.AllFiles)
                {
                    if (IsValidSourceFile(sourceFile))
                    {
                        Helper.WriteToDebugWindow(String.Format("File: >{0}<", sourceFile.Name), Helper.DebugDisplay.Always);

                        if (! ContainsNamespaceReference(sourceFile, nameSpace))
                        {
                            SourceRange sourceRange = GetNamespaceSourceRange(sourceFile);
                            SourcePoint sourcePoint = new SourcePoint(sourceRange.Bottom.Line + 1, 0);

                            NamespaceReference namespaceReference = new NamespaceReference(nameSpace);

                            string code = CodeRush.Language.GenerateElement(namespaceReference, CodeRush.Language.GetLanguageID(namespaceReference));

                            FileChange fileChange = new FileChange(sourceFile.Name, sourcePoint, code);
                            //FileChange fileChange = new FileChange(sourceFile.Name, sourceRange, code);
                            fileChangeCollection.Add(fileChange);
                        }
                    }
                }

                CodeRush.File.ApplyChanges(fileChangeCollection);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void AddLoggingToClass()
        {
            try
            {
                if (LanguageElementType.Class != CodeRush.Source.ActiveType.ElementType)
                {
                    MessageBox.Show("Not in a Class ElementType");
                    return;
                }

                Class activeClass = CodeRush.Source.ActiveClass;

                _fileChangeCollection = new FileChangeCollection();

                foreach (Method method in activeClass.AllMethods)
                {
                    AddLoggingToMethod(method);
                }

                CodeRush.File.ApplyChanges(_fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();
                _fileChangeCollection = null;
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void AddLoggingToActiveMethod()
        {
            try
            {
                //if (LanguageElementType.Method != CodeRush.Source.Active.ElementType)
                //{
                //    MessageBox.Show("Not in a Method ElementType");
                //    return;
                //}

                Method method = CodeRush.Source.ActiveMethod;

                if (null == method)
                {
                    MessageBox.Show("not in a method elementtype");
                    return;
                }

                _fileChangeCollection = new FileChangeCollection();

                AddLoggingToMethod(method);

                CodeRush.File.ApplyChanges(_fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();

                _fileChangeCollection = null;
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void AddLoggingToActiveModule()
        {
            try
            {
                if (LanguageElementType.Module != CodeRush.Source.ActiveType.ElementType)
                {
                    MessageBox.Show("Not in a module ElementType");
                    return; 
                }

                Module module = CodeRush.Source.ActiveType as Module;

                _fileChangeCollection = new FileChangeCollection();

                foreach (Method method in module.AllMethods)
                {
                    AddLoggingToMethod(method);
                }

                CodeRush.File.ApplyChanges(_fileChangeCollection);
                CodeRush.Source.ParseIfTextChanged();
                _fileChangeCollection = null;
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Debug);
            }
        }

        private static void AddLoggingToProject()
        {
            MessageBox.Show("My, my.  Aren't you brave!  Not implemented yet.");

            if (LanguageElementType.ProjectElement != CodeRush.Source.ActiveType.ElementType)
            {
                MessageBox.Show("Not in a Project ElementType");
                return;
            }
        }

        private static void AddLoggingToSolution()
        {
            MessageBox.Show("What are you nuts!  Not implemented yet, anyway");
        }

        void AddParametersToEnterTrace()
        {
            Boolean isSupportedType = false;

            try
            {
                Method method = CodeRush.Source.ActiveMethod;

                if (null == method)
                {
                    MessageBox.Show("Not in a method ElementType");
                    return;
                }

                TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
                TextViewSelection selection = activeTextDocument.ActiveViewSelection;
                DisplaySelectionInfo(selection);

                if ("Enter" != selection.Text)
                {
                    MessageBox.Show("Must select Enter text first.  Aborting.", "Context Error");
                    return;
                }

                StringBuilder sbFinalOutput = new StringBuilder();
                StringBuilder sbFormatString = new StringBuilder();
                StringBuilder sbArgs = new StringBuilder();

                sbFinalOutput.AppendFormat("String.Format(\"Enter");

                Int16 position = 0;

                foreach (Param parameter in method.AllParameters)
                {
                    Helper.WriteToDebugWindow(string.Format("  Name:>{0}< Type:({1}) ByVal:({2}) ByRef:({3}) Optional:({4}) Default:({5})  ",
                        parameter.Name, parameter.ParamType, parameter.IsByVal, parameter.IsReferenceParam, parameter.IsOptional, parameter.IsDefault), Helper.DebugDisplay.Debug);

                    switch (parameter.ParamType.ToLower())
                    {
                        case "boolean":
                        case "byte":
                        case "double":
                        case "int16":
                        case "int32":
                        case "integer":
                        case "long":
                        case "short":
                        case "single":
                        case "string":
                            isSupportedType = true;
                            break;

                        default:
                            Helper.WriteToDebugWindow(string.Format("Unsupported Type:({0}) Skipping",
                                parameter.ParamType), Helper.DebugDisplay.Always);

                            isSupportedType = false;
                            break;
                    }

                    if (isSupportedType && ! parameter.IsReferenceParam)
                    {
                        sbFormatString.AppendFormat(" {0}:({{{1}}})", parameter.Name, position);
                        sbArgs.AppendFormat(", {0}", parameter.Name);
                        position++;
                    }
                }

                sbFinalOutput.AppendFormat("{0}", sbFormatString.ToString());
                sbFinalOutput.AppendFormat("\"");
                sbFinalOutput.AppendFormat("{0}", sbArgs.ToString());
                sbFinalOutput.AppendFormat(")");

                Helper.WriteToDebugWindow(string.Format("sbFormatString:>{0}<", sbFormatString.ToString()), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("sbArgs:>{0}<", sbArgs.ToString()), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("sbFinalOutput:>{0}<", sbFinalOutput.ToString()), Helper.DebugDisplay.Debug);

                SourcePoint startPoint = selection.StartSourcePoint;
                SourcePoint endPoint = selection.EndSourcePoint;

                // Expand selection to include surrounding quotes. Enter -> "Enter"

                startPoint.Offset = startPoint.Offset - 1;
                endPoint.Offset = endPoint.Offset + 1;

                SourceRange sourceRange = new SourceRange(startPoint, endPoint);

                activeTextDocument.SelectText(sourceRange);

                activeTextDocument.ReplaceSelection(sbFinalOutput.ToString(), keepSelection: false);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        void AddClipboardToExitTrace(Boolean UseCount)
        {
            try
            {
                Method method = CodeRush.Source.ActiveMethod;

                if (null == method)
                {
                    MessageBox.Show("Not in a method ElementType");
                    return;
                }

                TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
                TextViewSelection selection = activeTextDocument.ActiveViewSelection;
                DisplaySelectionInfo(selection);

                if ("Exit" != selection.Text)
                {
                    MessageBox.Show("Must select Exit text first.  Aborting.", "Context Error");
                    return;
                }

                string cliboardVariable = Clipboard.GetText();

                if (0 == cliboardVariable.Length)
                {
                    MessageBox.Show("Clipboard empty.  Must select return variable and copy to clipboard.  Aborting.", "Context Error");
                    return;
                }

                StringBuilder sbFinalOutput = new StringBuilder();
                StringBuilder sbFormatString = new StringBuilder();
                StringBuilder sbArgs = new StringBuilder();

                if (UseCount)
                {
                    sbFinalOutput.AppendFormat("String.Format(\"Exit Count:({{0}})\", UBound({0}))", cliboardVariable);
                }
                else
                {
                    sbFinalOutput.AppendFormat("String.Format(\"Exit ({{0}})\", {0})", cliboardVariable);                    
                }

                Helper.WriteToDebugWindow(string.Format("sbFinalOutput:>{0}<", sbFinalOutput.ToString()), Helper.DebugDisplay.Debug);


                SourcePoint startPoint = selection.StartSourcePoint;
                SourcePoint endPoint = selection.EndSourcePoint;

                // Expand selection to include surrounding quotes. Exit -> "Exit"

                startPoint.Offset = startPoint.Offset - 1;
                endPoint.Offset = endPoint.Offset + 1;

                SourceRange sourceRange = new SourceRange(startPoint, endPoint);

                activeTextDocument.SelectText(sourceRange);

                activeTextDocument.ReplaceSelection(sbFinalOutput.ToString(), keepSelection: false);

                // Clear clipboard so don't accidentally carry something forward.

                Clipboard.Clear();
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        void AddParametersToExitTrace()
        {
            Boolean isSupportedType = false;

            try
            {
                Method method = CodeRush.Source.ActiveMethod;

                if (null == method)
                {
                    MessageBox.Show("Not in a method ElementType");
                    return;
                }

                TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
                TextViewSelection selection = activeTextDocument.ActiveViewSelection;
                DisplaySelectionInfo(selection);

                if ("Exit" != selection.Text)
                {
                    MessageBox.Show("Must select Exit text first.  Aborting.", "Context Error");
                    return;
                }

                StringBuilder sbFinalOutput = new StringBuilder();
                StringBuilder sbFormatString = new StringBuilder();
                StringBuilder sbArgs = new StringBuilder();

                Int16 argPostion = 0;

                if (method.MethodType == MethodTypeEnum.Function)
                {
                    sbFinalOutput.AppendFormat("String.Format(\"Exit ({{0}})");
                    sbArgs.AppendFormat(", RETURNVALUE");
                    argPostion = 1;
                }
                else
                {
                    sbFinalOutput.AppendFormat("String.Format(\"Exit");             
                }

                foreach (Param parameter in method.AllParameters)
                {
                    Helper.WriteToDebugWindow(string.Format("  Name:>{0}< Type:({1}) ByVal:({2}) ByRef:({3}) Optional:({4}) Default:({5})  ",
                        parameter.Name, parameter.ParamType, parameter.IsByVal, parameter.IsReferenceParam, parameter.IsOptional, parameter.IsDefault), Helper.DebugDisplay.Debug);

                    switch (parameter.ParamType.ToLower())
                    {
                        case "boolean":
                        case "byte":
                        case "double":
                        case "int16":
                        case "int32":
                        case "integer":
                        case "long":
                        case "short":
                        case "single":
                        case "string":
                            isSupportedType = true;
                            break;

                        default:
                            Helper.WriteToDebugWindow(string.Format("Unsupported Type:({0}) Skipping",
                                parameter.ParamType), Helper.DebugDisplay.Always);

                            isSupportedType = false;
                            break;
                    }

                    if (isSupportedType && parameter.IsReferenceParam)
                    {
                        sbFormatString.AppendFormat(" {0}:({{{1}}})", parameter.Name, argPostion);
                        sbArgs.AppendFormat(", {0}", parameter.Name);
                        argPostion++;
                    }
                }

                sbFinalOutput.AppendFormat("{0}", sbFormatString.ToString());
                sbFinalOutput.AppendFormat("\"");
                sbFinalOutput.AppendFormat("{0}", sbArgs.ToString());
                sbFinalOutput.AppendFormat(")");

                Helper.WriteToDebugWindow(string.Format("sbFormatString:>{0}<", sbFormatString.ToString()), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("sbArgs:>{0}<", sbArgs.ToString()), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("sbFinalOutput:>{0}<", sbFinalOutput.ToString()), Helper.DebugDisplay.Debug);

                SourcePoint startPoint = selection.StartSourcePoint;
                SourcePoint endPoint = selection.EndSourcePoint;

                // Expand selection to include surrounding quotes. Exit -> "Exit"

                startPoint.Offset = startPoint.Offset - 1;
                endPoint.Offset = endPoint.Offset + 1;

                SourceRange sourceRange = new SourceRange(startPoint, endPoint);

                activeTextDocument.SelectText(sourceRange);

                activeTextDocument.ReplaceSelection(sbFinalOutput.ToString(), keepSelection: false);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void ImportsEaseCore()
        {
            try
            {
                SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

                ImportsNamespaceReferenceInActiveDocument(sourceFile, "EaseCore");
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void InsertDefaultRegions()
        {
            // TODO(crhodes):
            // Handle creating blank area then calling this.

            try
            {
                CodeRush.Templates.ExpandAtCursor(false);
                CodeRush.Templates.ExpandTemplate("IDR");
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void InsertLoggingProperties()
        {
            // TODO(crhodes):
            // Determine if in Module or Class and insert at first appropriate line.  Have to handle inheritance, etc.

            try
            {
                CodeRush.Templates.ExpandAtCursor(false);
                CodeRush.Templates.ExpandTemplate("LOGPROP");
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        #endregion

        #region Utility Methods

        private bool ContainsNamespaceReference(SourceFile sourceFile, string nameSpaceName)
        {
            Boolean containsRefererece = false;

            if (sourceFile.UsingList.ContainsValue(nameSpaceName))
            {
                Helper.WriteToDebugWindow(string.Format("Already contains ({0})", nameSpaceName), Helper.DebugDisplay.Debug);
                containsRefererece = true;
            }

            return containsRefererece;
        }

        private static void DisplayContextInfo()
        {
            try
            {
                SolutionElement solution = CodeRush.Source.ActiveSolution;
                ProjectElement project = CodeRush.Source.ActiveProject;
                LanguageElement active = CodeRush.Source.Active;
                LanguageElement activeType = CodeRush.Source.ActiveType;
                Method activeMethod = CodeRush.Source.ActiveMethod;

                Helper.WriteToDebugWindow(string.Format("Solution: >{0}<  ", solution.Name), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("Project: >{0}<  ", project.Name), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("Active: Name:>{0}< ElementType:>{1}< ", active.Name, active.ElementType.ToString()), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("ActiveType: Name:>{0}<  ElementType:>{1}< ", activeType.Name, activeType.ElementType.ToString()), Helper.DebugDisplay.Always);

                if (activeMethod != null)
                {
                    Helper.WriteToDebugWindow(string.Format("Method: Name:>{0}<", activeMethod.Name), Helper.DebugDisplay.Always);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        void DisplayMethodInfo()
        {
            try
            {
                SolutionElement solution = CodeRush.Source.ActiveSolution;
                ProjectElement project = CodeRush.Source.ActiveProject;
                LanguageElement active = CodeRush.Source.Active;
                LanguageElement activeType = CodeRush.Source.ActiveType;
                Method activeMethod = CodeRush.Source.ActiveMethod;

                Helper.WriteToDebugWindow(string.Format("Solution: >{0}<  ", solution.Name), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("Project: >{0}<  ", project.Name), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("Active: Name:>{0}< ElementType:>{1}< ", active.Name, active.ElementType.ToString()), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("ActiveType: Name:>{0}<  ElementType:>{1}< ", activeType.Name, activeType.ElementType.ToString()), Helper.DebugDisplay.Always);

                if (activeMethod != null)
                {
                    Helper.WriteToDebugWindow(string.Format("Method: Name:>{0}<", activeMethod.Name), Helper.DebugDisplay.Always);
                    DisplayMethodInfo(activeMethod);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        void DisplayMethodInfo(Method method)
        {
            Helper.WriteToDebugWindow(string.Format("Parameters:"), Helper.DebugDisplay.Always);

            foreach (Param parameter in method.AllParameters)
            {
                Helper.WriteToDebugWindow(string.Format("  Name:>{0}< Type:({1}) ByVal:({2}) ByRef:({3}) Optional:({4}) Default:({5})  ", 
                    parameter.Name, parameter.ParamType, parameter.IsByVal, parameter.IsReferenceParam, parameter.IsOptional, parameter.IsDefault), Helper.DebugDisplay.Always);
            }
        }

        private void DisplayProjectDetailInfo()
        {
            try
            {
                ProjectElement project = CodeRush.Source.ActiveProject;

                DisplayProjectDetailInfo(project, 0);

                foreach (SourceFile sourceFile in project.AllFiles)
                {
                    DisplaySourceFileInfo(sourceFile, 4);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void DisplayProjectSummaryInfo()
        {
            try
            {
                ProjectElement project = CodeRush.Source.ActiveProject;

                DisplayProjectSummaryInfo(project);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void DisplaySelectionInfo(TextViewSelection selection)
        {
            SourcePoint startPoint = selection.StartSourcePoint;
            SourcePoint endPoint = selection.EndSourcePoint;

            Helper.WriteToDebugWindow(string.Format("Selection"), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  Text:>{0}<", selection.Text), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  StartLine:>{0}<", selection.StartLine), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  StartOffset:>{0}<", selection.StartOffset), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  StartPosition:>{0}<", selection.StartPosition), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  StartSourcePoint<"), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("    Line:>{0}<", startPoint.Line), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("    Offset:>{0}<", startPoint.Offset), Helper.DebugDisplay.Debug);


            Helper.WriteToDebugWindow(string.Format("  EndLine:>{0}<", selection.EndLine), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  EndOffset:>{0}<", selection.EndOffset), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  EndPosition:>{0}<", selection.EndPosition), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("  EndSourcePoint<"), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("    Line:>{0}<", endPoint.Line), Helper.DebugDisplay.Debug);
            Helper.WriteToDebugWindow(string.Format("    Offset:>{0}<", endPoint.Offset), Helper.DebugDisplay.Debug);

        }

        private static void DisplaySolutionInfo()
        {
            try
            {
                SolutionElement solution = CodeRush.Source.ActiveSolution;
                DisplaySolutionInfo(solution);

                if (solution == null)
                {
                    MessageBox.Show("No Active Solution");
                    return;
                }

                foreach (ProjectElement project in solution.AllProjects)
                {
                    DisplayProjectSummaryInfo(project);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private static bool IsValidSourceFile(SourceFile sourceFile)
        {
            Boolean isValid = false;

            string fileExtension = System.IO.Path.GetExtension(sourceFile.Name);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(sourceFile.Name);

            if ( ! System.IO.Path.GetExtension(fileNameWithoutExtension).ToUpper().Equals(".DESIGNER"))
            {
                string languageID = CodeRush.Language.GetLanguageID(fileExtension);

                switch (languageID.ToLower())
                {
                    case "csharp":
                        isValid = true;
                        break;

                    case "basic":
                        isValid = true;
                        break;

                    default:
                        break;
                }
            }

            return isValid;
        }

        private static string PadString(char pad, Int16 length)
        {
            return new string(pad, length);
        }

        #endregion

        #region Private Methods

        private void AddEntryTraceLines(Method method, SourcePoint logEntryPoint, SourceFile activeFile)
        {
            try
            {
                Helper.WriteToDebugWindow(string.Format("Entry Insert Line:>{0}< Offset:>{1}<", logEntryPoint.Line, logEntryPoint.Offset), Helper.DebugDisplay.Debug);

                FileChange logEntryFileChange = new FileChange(activeFile.Name, logEntryPoint, GetLogEntryText(CodeRush.Language.GetLanguageID(method)));
                _fileChangeCollection.Add(logEntryFileChange);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void AddExitTraceLines(Method method, SourcePoint logExitPoint, SourceFile activeFile)
        {
            try
            {
                Helper.WriteToDebugWindow(string.Format("Exit  Insert Line:>{0}< Offset:>{1}<", logExitPoint.Line, logExitPoint.Offset), Helper.DebugDisplay.Debug);

                FileChange logExitFileChange = new FileChange(activeFile.Name, logExitPoint, GetLogExitText(CodeRush.Language.GetLanguageID(method)));
                _fileChangeCollection.Add(logExitFileChange);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        /// <summary>
        /// Adds Entry and Exit Log.Trace methods to method.
        /// </summary>
        /// <param name="method"></param>
        private void AddLoggingToMethod(Method method)
        {
            Helper.WriteToDebugWindow(string.Format("Method: >{0}<", method.Name), Helper.DebugDisplay.Debug);

            if (method == null)
            {
                return;
            }

            // Skip constructors for now.  Not smart enough to detect calling base initialization routines.
            if (method.IsConstructor)
            {
                return;
            }

            // TODO(crhodes)
            // Consider breaking this into two parts.
            // AddEntryTrace() and AddExitTrace()
            // AddEntryTrace() is easy,
            // AddExitTrace() is easy if no return value, more difficult if returns as there can be multiple.

            try
            {
                SourceRange methodBody = method.BlockCodeRange;
                SourcePoint startBody = methodBody.Start;
                SourcePoint endBody = methodBody.End;

                Helper.WriteToDebugWindow(string.Format("Method Type:>{0}<", method.MethodType), Helper.DebugDisplay.Debug);

                Helper.WriteToDebugWindow(string.Format(" Method Start Line:>{0}< Offset:>{1}<", method.StartLine, method.StartOffset), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("   Body Start Line:>{0}< >{1}<", startBody.Line, startBody.Offset), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format("   Body End   Line:>{0}< >{1}<", endBody.Line, endBody.Offset), Helper.DebugDisplay.Debug);
                Helper.WriteToDebugWindow(string.Format(" Method End  Line:>{0}< Offset:>{1}<", method.EndLine, method.EndOffset), Helper.DebugDisplay.Debug);

                if (startBody.Line == method.EndLine)
                {
                    Helper.WriteToDebugWindow("Empty method body, skipping.", Helper.DebugDisplay.Debug);
                    return;
                }

                SourcePoint logEntryPoint = new SourcePoint();
                SourcePoint logExitPoint = new SourcePoint();
                SourceFile activeFile = CodeRush.Source.ActiveSourceFile;

                if (method.StartLine < startBody.Line)
                {
                    // This is the normal case
                    logEntryPoint.Line = startBody.Line;
                    logEntryPoint.Offset = 1;
                }
                else if (method.StartLine == startBody.Line)
                {
                    // This can happen if there is a comment at the end of the method declaration.
                    // Don't worry about the formatting; drop a marker so we can inspect.

                    Helper.WriteToDebugWindow(string.Format("{0} method.StartLine == startBody.Line", method.Name), Helper.DebugDisplay.Always);

                    logEntryPoint.Line = startBody.Line;
                    logEntryPoint.Offset = startBody.Offset;

                    CodeRush.Markers.Drop(logEntryPoint);
                }

                // N.B. Write the Exit Trace Lines before the Entry Trace lines to handle edge cases with returns.

                // If we are in a function, search backward through the method body looking for the first return.

                if (method.MethodType == MethodTypeEnum.Function)
                {
                    // TODO(crhodes):
                    // This section works fine if there is only one Return statement.  If there are multiple it finds the first one.
                    // Need to handle multiple return lines.  Should probably just log and handle manually for now as there may not be a block to use.
                    // Drop a marker so can easily go to the issues.

                    LanguageElement codeLine = method.GetLastCodeChild();

                    if (null == codeLine)
                    {
                        Helper.WriteToDebugWindow(string.Format("{0} LastCodeChild returned null", method.Name), Helper.DebugDisplay.Always);
                        CodeRush.Markers.Drop(logEntryPoint);
                        return;
                    }

                    if (codeLine.ElementType == LanguageElementType.Return)
                    {
                        Helper.WriteToDebugWindow("LastCodeChild was return", Helper.DebugDisplay.Debug);

                        Helper.WriteToDebugWindow(string.Format("  Return Start Line:>{0}< Offset:>{1}<", codeLine.StartLine, codeLine.StartOffset), Helper.DebugDisplay.Debug);
                        Helper.WriteToDebugWindow(string.Format("  Return End   Line:>{0}< Offset:>{1}<", codeLine.EndLine, codeLine.EndOffset), Helper.DebugDisplay.Debug);

                        logExitPoint.Line = codeLine.StartLine;
                        logExitPoint.Offset = 1;
                        //logExitPoint.Offset = codeLine.StartOffset;
                    }
                    else
                    {
                        var returnLine = method.FindChildByElementType(LanguageElementType.Return);

                        if (null != returnLine)
                        {
                            Helper.WriteToDebugWindow(string.Format("Using FindChildByElementType in {0}", method.Name), Helper.DebugDisplay.Always);
                            Helper.WriteToDebugWindow(string.Format("  Return Start Line:>{0}< Offset:>{1}<", returnLine.StartLine, returnLine.StartOffset), Helper.DebugDisplay.Debug);
                            Helper.WriteToDebugWindow(string.Format("  Return End   Line:>{0}< Offset:>{1}<", returnLine.EndLine, returnLine.EndOffset), Helper.DebugDisplay.Debug);

                            logExitPoint.Line = returnLine.StartLine;
                            logExitPoint.Offset = 1;

                            CodeRush.Markers.Drop(logExitPoint);
                        }
                        else
                        {
                            Helper.WriteToDebugWindow(string.Format("FindChildByElementType returned null in {0}", method.Name), Helper.DebugDisplay.Always);
                            // No return value so simple add the Exit Trace Lines

                            logExitPoint.Line = method.EndLine - 1;
                            logExitPoint.Offset = endBody.Offset;

                            CodeRush.Markers.Drop(logExitPoint);
                        }
                    }

                    AddExitTraceLines(method, logExitPoint, activeFile);
                }
                else
                {
                    // No return value

                    logExitPoint.Line = method.EndLine - 1;
                    logExitPoint.Offset = endBody.Offset;

                    AddExitTraceLines(method, logExitPoint, activeFile);
                }

                AddEntryTraceLines(method, logEntryPoint, activeFile);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private void DisplayProjectDetailInfo(ProjectElement project, Int16 indentLevel)
        {
            Helper.WriteToDebugWindow(string.Format("Project: >{0}<", project.Name), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "Name", project.Name), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "FullName", project.FullName), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "FilePath", project.FilePath), Helper.DebugDisplay.Always);

            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "AssemblyName", project.AssemblyName), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "AssemblyReferences", project.AssemblyReferences.Count), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "Attributes", project.Attributes.Count), Helper.DebugDisplay.Always);

            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "Language", project.Language), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "TargetFramework", project.TargetFramework), Helper.DebugDisplay.Always);

            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "OptionExplicit", project.OptionExplicit), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "OptionInfer", project.OptionInfer), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "OptionStrict", project.OptionStrict), Helper.DebugDisplay.Always);

            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsCaseSensitiveLanguage", project.IsCaseSensitiveLanguage), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsCsharp", project.IsCSharp), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsLightSwitch", project.IsLightSwitch), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsMiscProject", project.IsMiscProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsMvcProject", project.IsMvcProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsSilverlightProject", project.IsSilverlightProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsUnmodeledProject", project.IsUnmodeledProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsUWPProject", project.IsUWPProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsValidProject", project.IsValidProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsWebApplication", project.IsWebApplication), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsWebSite", project.IsWebSite), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsWinRTProject", project.IsWinRTProject), Helper.DebugDisplay.Always);
            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "IsWpfProject", project.IsWpfProject), Helper.DebugDisplay.Always);

            Helper.WriteToDebugWindow(string.Format("  {0,25}: >{1}<", "Files", project.AllFilesCount), Helper.DebugDisplay.Always);
        }

        private static void DisplayProjectSummaryInfo(ProjectElement project)
        {
            Helper.WriteToDebugWindow(string.Format("Project: >{0,20}< >{1,10}< >{2,10}< >{3,10}< >{4}<",
                                    project.Name, project.AssemblyName, project.Language, project.TargetFramework, project.FilePath), Helper.DebugDisplay.Always);
        }

        private static void DisplayRegionInfo(RegionDirectiveCollection regions, Int16 indentLevel)
        {
            try
            {
                Helper.WriteToDebugWindow(string.Format("{0}Regions: >{1}<  ",
                    PadString(' ', indentLevel), regions.Count), Helper.DebugDisplay.Debug);

                foreach (RegionDirective region in regions)
                {
                    Helper.WriteToDebugWindow(string.Format("{0}     Name: >{1}< >{2}< >{3}<",
                        PadString(' ', indentLevel), region.Name, region.StartLine, region.EndLine), Helper.DebugDisplay.Always);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
        }

        private static void DisplayNodeInfo(NodeList nodes, Int16 indentLevel)
        {
            try
            {
                Helper.WriteToDebugWindow(string.Format("{0}Nodes: >{1}<",
                    PadString(' ', indentLevel),
                    nodes.Count), Helper.DebugDisplay.Debug);

                foreach (var node in nodes)
                {
                    Helper.WriteToDebugWindow(String.Format("{0}  Node: >{1}<  >{2}<",
                        PadString(' ', indentLevel),
                        node.ToString(), node.GetType()), Helper.DebugDisplay.Always);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
            //foreach (TypeDeclaration type in nodes)
            //{
            //    Helper.WriteToDebugWindow(string.Format("           Name: >{0}<  Type: >{1}<", type.Name, type.ElementType.ToString()), Helper.DebugDisplay.Always);
            //}
        }

        private static void DisplaySolutionInfo(SolutionElement solution)
        {
            Helper.WriteToDebugWindow(string.Format("Solution: >{0}<  Projects:>{1}<", solution.FilePath, solution.ProjectElements.Count), Helper.DebugDisplay.Always);
        }

        private static void DisplayUsingListInfo(System.Collections.SortedList usingList, Int16 indentLevel)
        {
            foreach (System.Collections.DictionaryEntry item in usingList)
            {
                Helper.WriteToDebugWindow(String.Format("{0}Item: >{1}<  >{2}<", 
                    PadString(' ', indentLevel),
                    item.Key, item.Value), Helper.DebugDisplay.Always);
            }
        }

        private static void DisplaySourceFileInfo(SourceFile sourceFile, Int16 indentLevel)
        {
            if (null == sourceFile)
            {
                MessageBox.Show("No active SourceFile");
                return;
            }

            try
            {
                Helper.WriteToDebugWindow(string.Format("{0}File: >{1}<", PadString(' ', indentLevel),
                    sourceFile.Name), Helper.DebugDisplay.Always);

                Helper.WriteToDebugWindow(string.Format("{0}    OptionExplicit: >{1}<  OptionInfer: >{2}<  OptionStrict: >{3}<",
                    PadString(' ', indentLevel),
                    sourceFile.OptionExplicit, sourceFile.OptionInfer, sourceFile.OptionStrict), Helper.DebugDisplay.Always);

                //Helper.WriteToDebugWindow(string.Format("    ElementType >{1}<", sourceFile.ElementType.ToString()), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("{0}    UsingList: >{1}<  ", PadString(' ', indentLevel), 
                    sourceFile.UsingList.Count), Helper.DebugDisplay.Always);

                DisplayUsingListInfo(sourceFile.UsingList, 4);
                DisplayRegionInfo(sourceFile.Regions, 4);

                Helper.WriteToDebugWindow(string.Format("{0}    EndLine: >{1}<  ", PadString(' ', indentLevel),
                    sourceFile.EndLine), Helper.DebugDisplay.Always);
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
            //foreach (SourceFile sourceFile in project.AllFiles)
            //{
            //    sourceFileCount++;
            //    

            //    foreach (TypeDeclaration typeDeclaration in sourceFile.AllTypes)
            //    {
            //        typeCount++;

            //        foreach (TypeDeclaration x in typeDeclaration.AllChildTypes)
            //        {
            //            childTypeCount++;
            //        }

            //        foreach (Const x in typeDeclaration.AllConstants)
            //        {
            //            constantsCount++;
            //        }

            //        foreach (Event x in typeDeclaration.AllEvents)
            //        {
            //            eventsCount++;
            //        }

            //        foreach (DevExpress.CodeRush.StructuralParser.IFieldElement x in typeDeclaration.AllFields)
            //        {
            //            fieldsCount++;
            //        }

            //        foreach (Member member in typeDeclaration.AllMembers)
            //        {
            //            System.Diagnostics.Debug.WriteLine(string.Format("    Member: >{0}<", member.Name));
            //            membersCount++;
            //        }

            //        foreach (Method method in typeDeclaration.AllMethods)
            //        {
            //            System.Diagnostics.Debug.WriteLine(string.Format("    Method: >{0}<", method.Name));
            //            methodsCount++;
            //        }

            //        foreach (Property x in typeDeclaration.AllProperties)
            //        {
            //            propertiesCount++;
            //        }
            //    }
            //}
        }

        private string GetLogEntryText(string languageID)
        {
            string message = null;

            switch (languageID.ToLower())
            {
                case "csharp":
                    message = @"
    #if TRACE
        long tartTicks = Log.Trace9(""Enter"", LOG_APPNAME, BASE_ERRORNUMBER + 0);
    #endif

";
                    break;

                case "basic":
                    message = @"
    #If TRACE
        Dim startTicks As Long = Log.Trace9(""Enter"", LOG_APPNAME, BASE_ERRORNUMBER + 0)
    #End If

";
                    break;

                default:
                    throw new Exception("Unsupported Language");
            }

            return message;
        }

        private string GetLogExitText(string languageID)
        {
            string message = null;

            switch (languageID.ToLower())
            {
                case "csharp":
                    message = @"
    #if TRACE
        Log.Trace9(""Exit"", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks);
    #endif

";
                    break;

                case "basic":
                    message = @"

    #If TRACE
        Log.Trace9(""Exit"", LOG_APPNAME, BASE_ERRORNUMBER + 0, startTicks)
    #End If
";
                    break;

                default:
                    throw new Exception("Unsupported Language");
            }

            return message;
        }

        private static SourceRange GetNamespaceSourceRange(SourceFile sourceFile)
        {
            SourceRange sourceRange = new SourceRange(0, 0);

            foreach (LanguageElement element in sourceFile.Nodes)
            {
                if (element is NamespaceReference)
                {
                    if (sourceRange.Top < element.Range.Top)
                    {
                        sourceRange = element.Range.Clone();
                    }
                }
            }

            return sourceRange;
        }

        private void ImportsNamespaceReferenceInActiveDocument(SourceFile sourceFile, string nameSpace)
        {
            if (! ContainsNamespaceReference(sourceFile, nameSpace))
            {
                Helper.WriteToDebugWindow(string.Format("Adding Reference to ({0})", nameSpace), Helper.DebugDisplay.Debug);

                SourceRange sourceRange = GetNamespaceSourceRange(sourceFile);

                NamespaceReference namespaceReference = new NamespaceReference(nameSpace);

                string code = CodeRush.Language.GenerateElement(namespaceReference, CodeRush.Language.GetLanguageID(namespaceReference));
               
                CodeRush.Documents.ActiveTextDocument.InsertLines(sourceRange.Bottom.Line + 1, new string[] { code });
            }
        }

        #endregion
    }
}

