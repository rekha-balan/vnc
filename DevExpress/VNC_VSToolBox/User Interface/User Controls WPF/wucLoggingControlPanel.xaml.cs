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
        private void btnDisplayProjectInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplayProjectInfo();
        }

        private void btnDisplaySourceFileInfo_Click(object sender, RoutedEventArgs e)
        {
            SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

            DisplaySourceFileInfo(sourceFile);
        }
        private void btnDisplaySolutionInfo_Click(object sender, RoutedEventArgs e)
        {
            DisplaySolutionInfo();
        }

        private void btnImportsEaseCore_Click(object sender, RoutedEventArgs e)
        {
            ImportsEaseCore();
        }
        #endregion

        #region Main Methods

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

                logEntryPoint.Line = startBody.Line;
                logEntryPoint.Offset = 1;

                // N.B. Write the Exit Trace Lines before the Etnry Trace lines to handle edge cases with returns.

                // If we are in a function, search backward through the method body looking for the first return.

                if (method.MethodType == MethodTypeEnum.Function)
                {
                    // TODO(crhodes):
                    // This section works fine if there is only one Return statement.  If there are multiple it finds the first one.
                    // Need to handle multiple return lines.  Should probably just log and handle manually for now as there may not be a block to use.

                    LanguageElement codeLine = method.GetLastCodeChild();

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

                        Helper.WriteToDebugWindow(string.Format("Using FindChildByElementType in {0}", method.Name), Helper.DebugDisplay.Always);
                        Helper.WriteToDebugWindow(string.Format("  Return Start Line:>{0}< Offset:>{1}<", returnLine.StartLine, returnLine.StartOffset), Helper.DebugDisplay.Debug);
                        Helper.WriteToDebugWindow(string.Format("  Return End   Line:>{0}< Offset:>{1}<", returnLine.EndLine, returnLine.EndOffset), Helper.DebugDisplay.Debug);

                        logExitPoint.Line = returnLine.StartLine;
                        logExitPoint.Offset = 1;
                    }

                    AddExitTraceLines(method, logExitPoint, activeFile);
                }
                else
                {
                    // No return value so simple add the Exit Trace Lines

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

        private void AddExitTraceLines(Method method, SourcePoint logExitPoint, SourceFile activeFile)
        {
            Helper.WriteToDebugWindow(string.Format("Exit  Insert Line:>{0}< Offset:>{1}<", logExitPoint.Line, logExitPoint.Offset), Helper.DebugDisplay.Debug);

            FileChange logExitFileChange = new FileChange(activeFile.Name, logExitPoint, GetLogExitText(CodeRush.Language.GetLanguageID(method)));
            _fileChangeCollection.Add(logExitFileChange);
        }

        private void AddEntryTraceLines(Method method, SourcePoint logEntryPoint, SourceFile activeFile)
        {
            Helper.WriteToDebugWindow(string.Format("Entry Insert Line:>{0}< Offset:>{1}<", logEntryPoint.Line, logEntryPoint.Offset), Helper.DebugDisplay.Debug);

            FileChange logEntryFileChange = new FileChange(activeFile.Name, logEntryPoint, GetLogEntryText(CodeRush.Language.GetLanguageID(method)));
            _fileChangeCollection.Add(logEntryFileChange);
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

        private void DisplayProjectInfo()
        {
            try
            {
                ProjectElement project = CodeRush.Source.ActiveProject;

                DisplayProjectInfo(project);

                foreach (SourceFile sourceFile in project.AllFiles)
                {
                    DisplaySourceFileInfo(sourceFile);
                }
            }
            catch (Exception ex)
            {
                Helper.WriteToDebugWindow(ex.ToString(), Helper.DebugDisplay.Always);
            }
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


        private void ImportsEaseCore()
        {
            try
            {
                SourceFile sourceFile = CodeRush.Source.ActiveSourceFile;

                // TODO(crhodes):
                // Extract most of this to method that understands languages.


                if (sourceFile.UsingList.ContainsValue("EaseCore"))
                {
                    Helper.WriteToDebugWindow("Already contains EaseCore", Helper.DebugDisplay.Debug);
                }
                else
                {

                    Helper.WriteToDebugWindow("Adding Reference to EaseCore", Helper.DebugDisplay.Debug);
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


                    NamespaceReference namespaceReference = new NamespaceReference("EaseCore");
                    // HACK(crhodes)
                    // Hard code to VB for now.

                    string code = CodeRush.Language.GenerateElement(namespaceReference, CodeRush.Language.GetLanguageID(namespaceReference));
                    //string code = CodeRush.Language.GenerateElement(namespaceReference, Str.Language.VisualBasic);
                    CodeRush.Documents.ActiveTextDocument.InsertLines(sourceRange.Bottom.Line + 1, new string[] { code });
                }
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

        #endregion

        #region Private Methods

        private void DisplayProjectInfo(ProjectElement project)
        {
            Helper.WriteToDebugWindow(string.Format("Project: >{0,20}< >{1,10}< >{2,10}< >{3,10}< >{4}<",
                                    project.Name, project.AssemblyName, project.Language, project.TargetFramework, project.FilePath), Helper.DebugDisplay.Always);
        }

        private static void DisplayProjectSummaryInfo(ProjectElement project)
        {
            Helper.WriteToDebugWindow(string.Format("Project: >{0,20}< >{1,10}< >{2,10}< >{3,10}< >{4}<",
                                    project.Name, project.AssemblyName, project.Language, project.TargetFramework, project.FilePath), Helper.DebugDisplay.Always);
        }

        private static void DisplayRegionInfo(RegionDirectiveCollection regions)
        {
            Helper.WriteToDebugWindow(string.Format("      Regions: >{0}<  ", regions.Count), Helper.DebugDisplay.Debug);

            foreach (RegionDirective region in regions)
            {
                Helper.WriteToDebugWindow(string.Format("         Name: >{0}< >{1}< >{2}<", region.Name, region.StartLine, region.EndLine), Helper.DebugDisplay.Always);
            }
        }

        private static void DisplayNodeInfo(NodeList nodes)
        {
            Helper.WriteToDebugWindow(string.Format("         Nodes: >{0}<", nodes.Count), Helper.DebugDisplay.Debug);

            foreach(TypeDeclaration type in nodes)
            {
                Helper.WriteToDebugWindow(string.Format("           Name: >{0}<  Type: >{1}<", type.Name, type.ElementType.ToString()), Helper.DebugDisplay.Always);
            }
        }

        private static void DisplaySolutionInfo(SolutionElement solution)
        {
            Helper.WriteToDebugWindow(string.Format("Solution: >{0}<  Projects:>{1}<", solution.FilePath, solution.ProjectElements.Count), Helper.DebugDisplay.Always);
        }

        private static void DisplaySourceFileInfo(SourceFile sourceFile)
        {
            if (null == sourceFile)
            {
                MessageBox.Show("No active SourceFile");
                return;
            }

            try
            {
                Helper.WriteToDebugWindow(string.Format("  File: >{0}<  >{1}<", sourceFile.FilePath, sourceFile.Name), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("    OptionExplicit: >{0}<  OptionInfer: >{1}<  OptionStrict: >{2}<",
                    sourceFile.OptionExplicit, sourceFile.OptionInfer, sourceFile.OptionStrict), Helper.DebugDisplay.Always);

                //Helper.WriteToDebugWindow(string.Format("    ElementType >{1}<", sourceFile.ElementType.ToString()), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("    UsingList: >{0}<  ", sourceFile.UsingList.Count), Helper.DebugDisplay.Always);
                DisplayRegionInfo(sourceFile.Regions);

                Helper.WriteToDebugWindow(string.Format("    EndLine: >{0}<  ", sourceFile.EndLine), Helper.DebugDisplay.Always);
                Helper.WriteToDebugWindow(string.Format("    Nodes: >{0}<  ", sourceFile.Nodes.Count), Helper.DebugDisplay.Always);
                DisplayNodeInfo(sourceFile.Nodes);


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
                    break;
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
                    break;
            }

            return message;
        }

        #endregion
    }
}

