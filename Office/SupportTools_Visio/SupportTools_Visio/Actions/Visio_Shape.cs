﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Visio = Microsoft.Office.Interop.Visio;
using VisioHelper = VNC.AddinHelper.Visio;
//using static VNC.Helper;
using VNC;

using System.Drawing;

namespace SupportTools_Visio.Actions
{
    class Visio_Shape
    {

        #region Main Function Routines

        public static void ListInvocationsInMethod(Visio.Application app, string doc, string page, string shape, string shapeu, string[] array)
        {
            

        }

        public static void ListMethodsInClass(Visio.Application app, string doc, string page, string shape, string shapeu, string[] array)
        {


        }

        public static void Add_User_IsPageName()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            StringBuilder sb = new StringBuilder();

            Visio.Page page = app.ActivePage;

            Visio.Selection selection = app.ActiveWindow.Selection;

            VisioHelper.DisplayInWatchWindow(string.Format(" Page({0}) selection.Count: {1}", page.NameU, selection.Count));

            //for (int i = 0; i < selection.Count; i++)
            //{
            //    var item = selection[i];

            //    VisioHelper.DisplayInWatchWindow(string.Format("  Shape({0})", item.Name));
            //}

            foreach (Visio.Shape shape in selection)
            {
                VisioHelper.DisplayInWatchWindow(string.Format("  Shape({0})", shape.Name));

                try
                {
                    var isPageName = shape.CellExists["User.IsPageName", 1];
                    var isPageName0 = shape.CellExists["User.IsPageName", 0];

                    if (isPageName != 0)
                    {
                        Visio.Cell cell = shape.Cells["User.IsPageName"];

                        cell.ResultIU = 1.0;
                        VisioHelper.DisplayInWatchWindow(string.Format("  Shape({0}).Cell(Section:{1} RowName:{2} Name:{3})", shape.Name, cell.Section, cell.RowName, cell.Name));
                    }
                    else
                    {
                        shape.AddNamedRow((short)Visio.VisSectionIndices.visSectionUser, "IsPageName",0 );
                    	shape.Cells["User.isPageName"].ResultIU = 1.0;
                    }

                    UpdatePageNameShape(shape, page.NameU);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }

            VisioHelper.DisplayInWatchWindow(string.Format("Visio_Shape.Add_User_IsPageName() {0}", "End"));
        }

        public static void AddColorSupportToSelection()
        {
            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                Add_ColorSupport(shape);
            }
        }

        public static void AddHyperlinkToPage_FromShapeText()
        {
            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                string pageName = shape.Text;
                AddHyperLink(shape, pageName);
            }
        }

        public static void AddHyperLink(Visio.Shape shape, string pageName)
        {
            try
            {
                // TODO(crhodes):
                //	Validate pageName matches an existing pageName

                Visio.Hyperlink hlink = shape.Hyperlinks.Add();
                // hlink.Name = "do we need a name?";
                hlink.SubAddress = pageName;
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }
        public static void Add_IDandTextSupport_ToSelection()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                Add_IDandTextSupport(shape);
            }
        }

        public static void Add_IDSupport_ToSelection()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                Add_IDSupport(shape);
            }
        }

        public static void Add_TextControl_ToSelection()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                Add_TextTransformControl(shape);
            }
        }

        public static void DisplayInfo(Visio.Shape shape)
        {
            var isPageName = shape.CellExists["User.IsPageName", 0];    // 0 is Local and Inherited, 1 is Local only 

            //VisioHelper.DisplayInWatchWindow(string.Format("    Shape({0}).IsPageName({1})", shape.Name, isPageName));

            if (isPageName != 0)
            {
                Visio.Cell cell = shape.Cells["User.IsPageName"];
            }
            else
            {
                
            }

            VisioHelper.DisplayInWatchWindow(string.Format("   Shape(ID:{0}  Name:{1}  Text:>{2}<)",
                shape.ID, shape.Name, shape.Text));
        }

        public static void DisplayShapeInfo(Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()  Shape(ID:{1}  Name:{2}  Text:>{3}<)",
                MethodBase.GetCurrentMethod().Name,
                shape.ID, shape.Name, shape.Text));
        }

        public static void GatherInfo()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            StringBuilder sb = new StringBuilder();

            Visio.Page page = app.ActivePage;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                DisplayInfo(shape);
            }

            VisioHelper.DisplayInWatchWindow(string.Format("{0}() {1}",
                MethodBase.GetCurrentMethod().Name, "End"));
        }

        public static void HandleShapeAdded(Visio.Shape shape)
        {
            var isPageName = shape.CellExists["User.IsPageName", 0];    // 0 is Local and Inherited, 1 is Local only 
            //VisioHelper.DisplayInWatchWindow(string.Format("{0}({1}  isPageName:{2})",
            //    MethodBase.GetCurrentMethod().Name, shape.Name, isPageName));

            //VisioHelper.DisplayInWatchWindow(string.Format("  UpdatePageNameShape({0}).IsPageName({1})", shape.Name, isPageName));

            if (isPageName != 0)
            {
                Visio.Cell cell = shape.Cells["User.IsPageName"];

                //VisioHelper.DisplayInWatchWindow(string.Format("    Shape({0}).Cell(Section:{1} RowName:{2} Name:{3} Value:{4})",
                //    shape.Name, cell.Section, cell.RowName, cell.Name, cell.ResultIU));

                if (cell.ResultIU > 0)
                {
                    Visio.Application app = Globals.ThisAddIn.Application;
                    Visio.Page page = app.ActivePage;
                    shape.Text = page.NameU;
                }
            }
        }

        public static void LinkShapeToPage(Visio.Application app, string doc, string page, string shape, string shapeu, String[] args)
        {
            string pageLevel = args[0];
            string separator = "";

            VisioHelper.DisplayInWatchWindow(string.Format("{0}() PageLevel:{1}",
                MethodBase.GetCurrentMethod().Name,
                pageLevel));

            // Current shape contains text for new page name.

            Visio.Shape activeShape = app.ActivePage.Shapes[shape];
            VisioHelper.DisplayInWatchWindow(string.Format("  Shape(Name:{0}  Text:{1}", activeShape.Name, activeShape.Text));

            // Update the current shape's hyperlink to point to the page represented by the text

            if (pageLevel.Length > 0)
            {
                separator = "-";
            }

            // shape.Text comes in as OBJ if use fields and Shape Data.   Use shape.Characters instead. 
            string pageName = string.Format("{0}{1}{2}", pageLevel, separator, activeShape.Characters.TextAsString.Replace("\n", " "));
            //string pageName = string.Format("{0}{1}{2}", pageLevel, separator, activeShape.Text.Replace("\n", " "));

            // TODO(crhodes):
            //	Not sure which of these two approaches is doing the magic.

            Visio.Hyperlink newHyperLink = activeShape.AddHyperlink();
            newHyperLink.SubAddress = pageName;

            //activeShape.CellsSRC[(short)Visio.VisSectionIndices.visSectionHyperlink, 0, 2].Formula = pageName;

        }

        public static void MakeLinkableMaster()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                MakeLinkableMaster(shape);
            }
        }
        private static void MakeLinkableMaster(Microsoft.Office.Interop.Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            try
            {
                Validate_Action_SectionExists(shape);

                Add_ActionSection_Row(shape,
                    "CreateActivityPage",
                    "=RUNADDONWARGS(\"QueueMarkerEvent\",\"CreatePageForShape,,, Page % 20Base\")",
                    "\"Create Page for Shape\"");
                Add_ActionSection_Row(shape,
                    "LinkShapeToPage",
                    "=RUNADDONWARGS(\"QueueMarkerEvent\",\"LinkShapeToPage, \")",
                    "\"Link Shape to Page\"");

                Add_Prop_Row(shape, "PageName", "PageName", (short)Visio.VisCellVals.visPropTypeString, null, "<page name>".WrapInDblQuotes());
                Add_Prop_Row(shape, "HyperLink", "HyperLink", (short)Visio.VisCellVals.visPropTypeString, null, "");

                // For now assume the shape does not have any hyperlinks

                Visio.Hyperlink newHyperLink = shape.AddHyperlink();

                // This doesn't work as the value is treated as a string.
                //newHyperLink.SubAddress = "Prop.HyperLink";

                // Need to go at it as a CellSRC

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    (short)Visio.VisRowIndices.visRow1stHyperlink,
                    (short)Visio.VisCellIndices.visHLinkSubAddress].FormulaU = "Prop.HyperLink";

                Validate_TextField_SectionExists(shape);

                // Not sure how to go about this.  Macro recorder shows this

                Visio.Characters characters = shape.Characters;
                characters.AddCustomFieldU("Prop.PageName", (short)Visio.VisFieldFormats.visFmtNumGenNoUnits);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        public static void Populate_Actions_Section(Visio.Shape shape, string actionName, string action, string menu, string tagName, string buttonFace, string sortKey, string isChecked, string isDisabled, string isReadOnly, string isInvisible, string beginGroup, string flyoutChild)
        {
            Add_ActionSection_Row(shape,
                actionName,
                action,
                menu,
                tagName,
                buttonFace, sortKey, isChecked, isDisabled, isReadOnly, isInvisible, beginGroup, flyoutChild);
        }

        public static void Populate_Hyperlinks_Section(Visio.Shape shape, string rowName, string description, string address, string subAddress, string extraInfo, string frame, string sortKey, string newWindow, string default1, string invisible)
        {
            Add_HyperlinkSection_Row(shape,
                rowName,
                description, 
                address, 
                subAddress, 
                extraInfo, frame, sortKey, newWindow, default1, invisible);

        }

        private static void Set_RowFill_Cell(Visio.Shape shape, Visio.VisCellIndices cellIndex, string value)
        {
            if (value != null)
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowFill,
                    (short)cellIndex].FormulaU = value;
            }
        }

        private static void Set_RowXFormOut_Cell(Visio.Shape shape, Visio.VisCellIndices cellIndex, string value)
        {
            if (value != null)
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowXFormOut,
                    (short)cellIndex].FormulaU = value;
            }
        }

        public static void Set_FillFormat_Section(Microsoft.Office.Interop.Visio.Shape shape, 
            string fillForegnd = null, string fillForegndTrans = null, 
            string fillBkgnd = null, string fillBkgndTrans = null, string fillPattern = null,
            string shdwForegnd = null, string shdwForegndTrans = null, 
            string shdwPattern = null, string shapeShdwOffsetX = null, string shapeShdwOffsetY = null,
            string shapeShdwType = null, string shapeShdwObliqueAngle = null, string shapeShdwScaleFactor = null, 
            string shapeShdwBlur = null, string shapeShdwShow = null)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            // This Section always exists, so just set values

            // Everything defaults to null and is in the likely order of most often changed.
            // If null, skip setting value.

            try
            {
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillForegnd, fillForegnd);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillForegndTrans, fillForegndTrans);

                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillBkgnd, fillBkgnd);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillBkgndTrans, fillBkgndTrans);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillPattern, fillPattern);

                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwForegnd, shdwForegnd);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwForegndTrans, shdwForegndTrans);

                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwPattern, shdwPattern);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwOffsetX, shapeShdwOffsetX);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwOffsetY, shapeShdwOffsetY);

                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwType, shapeShdwType);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwObliqueAngle, shapeShdwObliqueAngle);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwScaleFactor, shapeShdwScaleFactor);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwBlur, shapeShdwBlur);
                Set_RowFill_Cell(shape, Visio.VisCellIndices.visFillShdwShow, shapeShdwShow);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        public static void Set_ShapeTransform_Section(Visio.Shape shape, 
                string width = null, string height = null, string pinX = null, string pinY = null, 
                string flipX = null, string flipY = null, string locPinX = null, string locPinY = null, 
                string angle = null, string resizeMode = null)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            // This Section always exists, so just set values

            // Everything defaults to null and is in the likely order of most often changed.
            // If null, skip setting value.

            try
            {
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormWidth, width);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormHeight, height);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormPinX, pinX);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormPinY, pinY);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormFlipX, flipX);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormFlipY, flipY);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormLocPinX, locPinX);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormLocPinY, locPinY);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormAngle, angle);
                Set_RowXFormOut_Cell(shape, Visio.VisCellIndices.visXFormResizeMode, resizeMode);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        public static void UpdatePageNameShape(Visio.Shape shape, string pageName)
        {
            var isPageName = shape.CellExistsU["User.IsPageName", 0];    // 0 is Local and Inherited, 1 is Local only 

            VisioHelper.DisplayInWatchWindow(string.Format("{0}({1}  isPageName:{2})",
                MethodBase.GetCurrentMethod().Name, shape.Name, isPageName));

            //VisioHelper.DisplayInWatchWindow(string.Format("  UpdatePageNameShape({0}).IsPageName({1})", shape.Name, isPageName));

            if (isPageName != 0)
            {
                Visio.Cell cell = shape.CellsU["User.IsPageName"];

                VisioHelper.DisplayInWatchWindow(string.Format("    Shape({0}).Cell(Section:{1} RowName:{2} Name:{3} Value:{4})",
                    shape.Name, cell.Section, cell.RowName, cell.Name, cell.ResultIU));

                if (cell.ResultIU > 0)
                {
                    shape.Text = pageName;
                }
            }
        }

        public static void ZeroMargins()
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Visio.Application app = Globals.ThisAddIn.Application;

            Visio.Selection selection = app.ActiveWindow.Selection;

            foreach (Visio.Shape shape in selection)
            {
                ZeroMargins(shape);
            }
        }

        #endregion


        #region Private Methods

        private static void Add_ColorSupport(Visio.Shape shape)
        {
            // Have to add these in the right order as there are some dependencies

            string value = string.Empty;

            value = "AliceBlue; AntiqueWhite; Aqua; Aquamarine; Azure; Beige; Bisque; Black; BlanchedAlmond; Blue; BlueViolet; Brown; BurlyWood; CadetBlue; Chartreuse; Chocolate; Coral; CornflowerBlue; Cornsilk; Crimson; Cyan; DarkBlue; DarkCyan; DarkGoldenrod; DarkGray; DarkGreen; DarkKhaki; DarkMagenta; DarkOliveGreen; DarkOrange; DarkOrchid; DarkRed; DarkSalmon; DarkSeaGreen; DarkSlateBlue; DarkSlateGray; DarkTurquoise; DarkViolet; DeepPink; DeepSkyBlue; DimGray; DodgerBlue; Firebrick; FloralWhite; ForestGreen; Fuchsia; Gainsboro; GhostWhite; Gold; Goldenrod; Gray; Green; GreenYellow; Honeydew; HotPink; IndianRed; Indigo; Ivory; Khaki; Lavender; LavenderBlush; LawnGreen; LemonChiffon; LightBlue; LightCoral; LightCyan; LightGoldenrodYellow; LightGreen; LightGray; LightPink; LightSalmon; LightSeaGreen; LightSkyBlue; LightSlateGray; LightSteelBlue; LightYellow; Lime; LimeGreen; Linen; Magenta; Maroon; MediumAquamarine; MediumBlue; MediumOrchid; MediumPurple; MediumSeaGreen; MediumSlateBlue; MediumSpringGreen; MediumTurquoise; MediumVioletRed; MidnightBlue; MintCream; MistyRose; Moccasin; NavajoWhite; Navy; OldLace; Olive; OliveDrab; Orange; OrangeRed; Orchid; PaleGoldenrod; PaleGreen; PaleTurquoise; PaleVioletRed; PapayaWhip; PeachPuff; Peru; Pink; Plum; PowderBlue; Purple; Red; RosyBrown; RoyalBlue; SaddleBrown; Salmon; SandyBrown; SeaGreen; SeaShell; Sienna; Silver; SkyBlue; SlateBlue; SlateGray; Snow; SpringGreen; SteelBlue; Tan; Teal; Thistle; Tomato; Turquoise; Violet; Wheat; White; WhiteSmoke; Yellow; YellowGreen";
            value = string.Format("\"{0}\"", value);
            Add_User_Row(shape, "colorNames", value);

            value = "RGB(240, 248, 255); RGB(250, 235, 215); RGB(0, 255, 255); RGB(127, 255, 212); RGB(240, 255, 255); RGB(245, 245, 220); RGB(255, 228, 196); RGB(0, 0, 0); RGB(255, 235, 205); RGB(0, 0, 255); RGB(138, 43, 226); RGB(165, 42, 42); RGB(222, 184, 135); RGB(95, 158, 160); RGB(127, 255, 0); RGB(210, 105, 30); RGB(255, 127, 80); RGB(100, 149, 237); RGB(255, 248, 220); RGB(220, 20, 60); RGB(0, 255, 255); RGB(0, 0, 139); RGB(0, 139, 139); RGB(184, 134, 11); RGB(169, 169, 169); RGB(0, 100, 0); RGB(189, 183, 107); RGB(139, 0, 139); RGB(85, 107, 47); RGB(255, 140, 0); RGB(153, 50, 204); RGB(139, 0, 0); RGB(233, 150, 122); RGB(143, 188, 139); RGB(72, 61, 139); RGB(47, 79, 79); RGB(0, 206, 209); RGB(148, 0, 211); RGB(255, 20, 147); RGB(0, 191, 255); RGB(105, 105, 105); RGB(30, 144, 255); RGB(178, 34, 34); RGB(255, 250, 240); RGB(34, 139, 34); RGB(255, 0, 255); RGB(220, 220, 220); RGB(248, 248, 255); RGB(255, 215, 0); RGB(218, 165, 32); RGB(128, 128, 128); RGB(0, 128, 0); RGB(173, 255, 47); RGB(240, 255, 240); RGB(255, 105, 180); RGB(205, 92, 92); RGB(75, 0, 130); RGB(255, 255, 240); RGB(240, 230, 140); RGB(230, 230, 250); RGB(255, 240, 245); RGB(124, 252, 0); RGB(255, 250, 205); RGB(173, 216, 230); RGB(240, 128, 128); RGB(224, 255, 255); RGB(250, 250, 210); RGB(144, 238, 144); RGB(211, 211, 211); RGB(255, 182, 193); RGB(255, 160, 122); RGB(32, 178, 170); RGB(135, 206, 250); RGB(119, 136, 153); RGB(176, 196, 222); RGB(255, 255, 224); RGB(0, 255, 0); RGB(50, 205, 50); RGB(250, 240, 230); RGB(255, 0, 255); RGB(128, 0, 0); RGB(102, 205, 170); RGB(0, 0, 205); RGB(186, 85, 211); RGB(147, 112, 219); RGB(60, 179, 113); RGB(123, 104, 238); RGB(0, 250, 154); RGB(72, 209, 204); RGB(199, 21, 133); RGB(25, 25, 112); RGB(245, 255, 250); RGB(255, 228, 225); RGB(255, 228, 181); RGB(255, 222, 173); RGB(0, 0, 128); RGB(253, 245, 230); RGB(128, 128, 0); RGB(107, 142, 35); RGB(255, 165, 0); RGB(255, 69, 0); RGB(218, 112, 214); RGB(238, 232, 170); RGB(152, 251, 152); RGB(175, 238, 238); RGB(219, 112, 147); RGB(255, 239, 213); RGB(255, 218, 185); RGB(205, 133, 63); RGB(255, 192, 203); RGB(221, 160, 221); RGB(176, 224, 230); RGB(128, 0, 128); RGB(255, 0, 0); RGB(188, 143, 143); RGB(65, 105, 225); RGB(139, 69, 19); RGB(250, 128, 114); RGB(244, 164, 96); RGB(46, 139, 87); RGB(255, 245, 238); RGB(160, 82, 45); RGB(192, 192, 192); RGB(135, 206, 235); RGB(106, 90, 205); RGB(112, 128, 144); RGB(255, 250, 250); RGB(0, 255, 127); RGB(70, 130, 180); RGB(210, 180, 140); RGB(0, 128, 128); RGB(216, 191, 216); RGB(255, 99, 71); RGB(64, 224, 208); RGB(238, 130, 238); RGB(245, 222, 179); RGB(255, 255, 255); RGB(245, 245, 245); RGB(255, 255, 0); RGB(154, 205, 50)";
            value = string.Format("\"{0}\"", value);
            Add_User_Row(shape, "colorValues", value);

            Add_Prop_Row(shape, "Color", "Color", (short)Visio.VisCellVals.visPropTypeListFix, "User.colorNames", "", "", "");

            value = "INDEX(LOOKUP(Prop.Color,User.colorNames),User.colorValues)";
            Add_User_Row(shape, "Color", value);
        }

        private static void Add_IDandTextSupport(Microsoft.Office.Interop.Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Validate_Prop_SectionExists(shape);

            Add_Prop_Row(shape, rowName: "ID", label: "ID", type: (short)Visio.VisCellVals.visPropTypeString, format: null, value: "Xnnn".WrapInDblQuotes());
            Add_Prop_Row(shape, "ShowID", "Show ID", (short)Visio.VisCellVals.visPropTypeBool, null, "TRUE".WrapInDblQuotes());

            Add_Prop_Row(shape, "Text", "Text", (short)Visio.VisCellVals.visPropTypeString, null, "<text>".WrapInDblQuotes());
            Add_Prop_Row(shape, "TextLeft", "Text Left", (short)Visio.VisCellVals.visPropTypeString, null, "<left text>".WrapInDblQuotes());
            Add_Prop_Row(shape, "TextTop", "Text Top", (short)Visio.VisCellVals.visPropTypeString, null, "<top text>".WrapInDblQuotes());
            Add_Prop_Row(shape, "TextRight", "Text Right", (short)Visio.VisCellVals.visPropTypeString, null, "<right text>".WrapInDblQuotes());
            Add_Prop_Row(shape, "TextBottom", "Text Bottom", (short)Visio.VisCellVals.visPropTypeString, null, "<bottom text>".WrapInDblQuotes());

            Add_Prop_Row(shape, "ShowLeftText", "Show Left Text", (short)Visio.VisCellVals.visPropTypeBool, null, "FALSE".WrapInDblQuotes());
            Add_Prop_Row(shape, "ShowTopText", "Show Top Text", (short)Visio.VisCellVals.visPropTypeBool, null, "FALSE".WrapInDblQuotes());
            Add_Prop_Row(shape, "ShowRightText", "Show Right Text", (short)Visio.VisCellVals.visPropTypeBool, null, "FALSE".WrapInDblQuotes());
            Add_Prop_Row(shape, "ShowBottomText", "Show Bottom Text", (short)Visio.VisCellVals.visPropTypeBool, null, "FALSE".WrapInDblQuotes());

            Add_Prop_Row(shape, "SizeText", "Size Text", (short)Visio.VisCellVals.visPropTypeNumber, "0.0".WrapInDblQuotes(), "1.0");
            Add_Prop_Row(shape, "SizeLeftText", "Size Left Text", (short)Visio.VisCellVals.visPropTypeNumber, "0.0".WrapInDblQuotes(), "1.0");
            Add_Prop_Row(shape, "SizeTopText", "Size Top Text", (short)Visio.VisCellVals.visPropTypeNumber, "0.0".WrapInDblQuotes(), "1.0");
            Add_Prop_Row(shape, "SizeRightText", "Size Right Text", (short)Visio.VisCellVals.visPropTypeNumber, "0.0".WrapInDblQuotes(), "1.0");
            Add_Prop_Row(shape, "SizeBottomText", "Size Bottom Text", (short)Visio.VisCellVals.visPropTypeNumber, value: "1.0", format: "0.0".WrapInDblQuotes());
            Add_Prop_Row(shape, "SizeIDText", "Size ID Text", (short)Visio.VisCellVals.visPropTypeNumber, value: "1.0", format: "0.0".WrapInDblQuotes());

            Add_Prop_Row(shape, "GroupName", "Group Name", (short)Visio.VisCellVals.visPropTypeString, null, "<group name>".WrapInDblQuotes());
            Add_Prop_Row(shape, "TabColor", "Tab Color (RGB)", (short)Visio.VisCellVals.visPropTypeString, null, "RGB(128,128,128)");
        }

        private static void Add_IDSupport(Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Validate_Prop_SectionExists(shape);

            Add_Prop_Row(shape, "ID", "ID", (short)Visio.VisCellVals.visPropTypeString, null, "000".WrapInDblQuotes());
            Add_Prop_Row(shape, "ShowID", "Show ID", (short)Visio.VisCellVals.visPropTypeBool, null, "TRUE".WrapIn("\""));
            Add_Prop_Row(shape, "Text", "Text", (short)Visio.VisCellVals.visPropTypeString, null, "<text>".WrapInDblQuotes());
            Add_Prop_Row(shape, "SizeText", "Size Text", (short)Visio.VisCellVals.visPropTypeNumber, format: "0.0".WrapInDblQuotes(), value: "1.0");
        }

        // TODO(crhodes):
        // This section should be reviewed and if appropriate lifted out into the ShapeEditor 
        // and associated configuration file

        private static void Add_TextTransformControl(Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Populate_Controls_Section(shape,
                "Width*0.5",
                "Height*0.5",
                "Controls.Row_1",
                "Controls.Row_1.Y",
                "0",
                "0",
                "TRUE",
                "Reposition Text");

            Set_TextTransform_Section(shape,
                "GUARD(Width*0.9)",
                "GUARD(Height*0.9)",
                "GUARD(Controls.Row_1)",
                "GUARD(Controls.Row_1.Y)",
                "TxtWidth*0.5",
                "TxtHeight*0.5",
                "0 deg"
                );

            Set_TextBlockFormat_Section(shape,
                "Char.Size/2",
                "Char.Size/4",
                "Char.Size/2",
                "Char.Size/4",
                "0",
                "1",
                "0",
                "0%",
                "0.5 in");
        }

        private static void ZeroMargins(Visio.Shape shape)
        {
            VisioHelper.DisplayInWatchWindow(string.Format("{0}()",
                MethodBase.GetCurrentMethod().Name));

            Validate_TextBlockFormat_SectionExists(shape);

            Set_TextBlockMargins(shape, "0", "0", "0", "0");
        }

        #endregion


        #region Utility Routines

        private static void Add_ActionSection_Row(Visio.Shape shape, string rowName, 
            string action, 
            string menu,
            string tagName = "", 
            string buttonFace = "", 
            string sortKey = "",
            string isChecked = "0", 
            string isDisabled = "0", 
            string isReadOnly = "0", 
            string isInvisible = "0", 
            string beginGroup = "0", 
            string flyoutChild = "0")
        {
            //result = shape.AddRow((short)Visio.VisSectionIndices.visSectionAction, (short)Visio.VisRowIndices.visRowLast, (short)Visio.VisRowTags.visTagDefault);
            // TODO(crhodes):
            // Determine what this does if row already exists.

            var rowNumber = shape.AddNamedRow((short)Visio.VisSectionIndices.visSectionAction, rowName, (short)Visio.VisRowTags.visTagDefault);
            
            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionAction].FormulaU = action;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionMenu].FormulaU = menu.WrapInDblQuotes();
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionTagName].FormulaU = tagName;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionButtonFace].FormulaU = buttonFace;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionSortKey].FormulaU = sortKey;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionChecked].FormulaU = isChecked;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionDisabled].FormulaU = isDisabled;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionReadOnly].FormulaU = isReadOnly;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionInvisible].FormulaU = isInvisible;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionBeginGroup].FormulaU = beginGroup;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionFlyoutChild].FormulaU = flyoutChild;
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        private static void Add_HyperlinkSection_Row(Visio.Shape shape, 
            string rowName, 
            string description, 
            string address, 
            string subAddress,
            string extraInfo = "", 
            string frame = "", 
            string sortKey = "", 
            string newWindow = "0", 
            string default1 = "0", 
            string invisible = "0")
        {
            //result = shape.AddRow((short)Visio.VisSectionIndices.visSectionAction, (short)Visio.VisRowIndices.visRowLast, (short)Visio.VisRowTags.visTagDefault);
            // TODO(crhodes):
            // Determine what this does if row already exists.

            var rowNumber = shape.AddNamedRow((short)Visio.VisSectionIndices.visSectionHyperlink, rowName, (short)Visio.VisRowTags.visTagDefault);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkDescription].FormulaU = description;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkAddress].FormulaU = address;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkSubAddress].FormulaU = subAddress;  // Wrapping in doubleqoutes would break entering formulas
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkExtraInfo].FormulaU = extraInfo;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkExtraInfo].FormulaU = frame;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkSortKey].FormulaU = sortKey;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkNewWin].FormulaU = newWindow;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkDefault].FormulaU = default1;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionHyperlink,
                    rowNumber,
                    (short)Visio.VisCellIndices.visHLinkInvisible].FormulaU = invisible;

            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }

        }

        internal static void Add_ShapeDataSection_Row(Visio.Shape shape, string rowName, 
            string action, 
            string menu,
            string tagName = "", 
            string buttonFace = "", 
            string sortKey = "",
            string isChecked = "0", 
            string isDisabled = "0", 
            string isReadOnly = "0", 
            string isInvisible = "0", 
            string beginGroup = "0", 
            string flyoutChild = "0")
        {
            var rowNumber = shape.AddNamedRow((short)Visio.VisSectionIndices.visSectionAction, rowName, (short)Visio.VisRowTags.visTagDefault);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionAction].FormulaU = action;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionMenu].FormulaU = menu;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionTagName].FormulaU = tagName;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionButtonFace].FormulaU = buttonFace;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionSortKey].FormulaU = sortKey;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionChecked].FormulaU = isChecked;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionDisabled].FormulaU = isDisabled;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionReadOnly].FormulaU = isReadOnly;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionInvisible].FormulaU = isInvisible;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionBeginGroup].FormulaU = beginGroup;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionAction,
                    rowNumber,
                    (short)Visio.VisCellIndices.visActionFlyoutChild].FormulaU = flyoutChild;
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        /// <summary>
        /// Add a Prop (ShapeData) section to a ShapeSheet
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="rowName"></param>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <param name="format">Values must be placed in Quotes ("") if strings</param>
        /// <param name="value">Values must be placed in Quotes ("") if strings</param>
        /// <param name="prompt"></param>
        /// <param name="sortKey"></param>
        /// <param name="invisible"></param>
        /// <param name="ask"></param>
        /// <param name="langID"></param>
        /// <param name="calendar"></param>
        internal static void Add_Prop_Row(Visio.Shape shape, 
            string rowName, 
            string label, short type, string format, string value, 
            string prompt = null, string sortKey = null,
            string invisible = null, string ask = null, string langID = null, string calendar = null)
        {
            Validate_Prop_SectionExists(shape);

            try
            {
                // Add the Row

                short rowNumber = shape.AddNamedRow(
                    (short)Visio.VisSectionIndices.visSectionProp,
                    rowName,
                    (short)Visio.VisRowTags.visTagDefault);

                // And the important cells: Label, Type, Value

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionProp,
                    rowNumber,
                    (short)Visio.VisCellIndices.visCustPropsLabel].FormulaU = string.Format("\"{0}\"", label);

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionProp,
                    rowNumber,
                    (short)Visio.VisCellIndices.visCustPropsType].FormulaU = type.ToString();

                if (format != null)
                {
                    shape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionProp,
                        rowNumber,
                        (short)Visio.VisCellIndices.visCustPropsFormat].FormulaU = format;
                }

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionProp,
                    rowNumber,
                    (short)Visio.VisCellIndices.visCustPropsValue].FormulaU = value;

                // And any optional cells

                if (null != prompt)
                {
                    shape.CellsSRC[
                       (short)Visio.VisSectionIndices.visSectionProp,
                       rowNumber,
                       (short)Visio.VisCellIndices.visCustPropsPrompt].FormulaU = string.Format("\"{0}\"", prompt);
                }

                if (null != sortKey)
                {
                    shape.CellsSRC[
                        (short)Visio.VisSectionIndices.visSectionProp,
                        rowNumber,
                        (short)Visio.VisCellIndices.visCustPropsSortKey].FormulaU = string.Format("\"{0}\"", sortKey);
                }

                // TODO(crhodes):
                // Add support for remaining optional arguments

            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Add_User_Row(Visio.Shape shape, 
            string rowName, string value, 
            string prompt="")
        {
            Validate_User_SectionExists(shape);

            try
            {
                short rowNumber = shape.AddNamedRow(
                    (short)Visio.VisSectionIndices.visSectionUser, 
                    rowName, 
                    (short)Visio.VisRowTags.visTagDefault);

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionUser,
                    (short)(Visio.VisRowIndices.visRowControl + rowNumber),
                    (short)Visio.VisCellIndices.visUserValue].FormulaU = value;

                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionUser,
                    (short)(Visio.VisRowIndices.visRowControl + rowNumber),
                    (short)Visio.VisCellIndices.visUserPrompt].FormulaU = string.Format("\"{0}\"", prompt);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Populate_Controls_Section(Visio.Shape shape, 
            string X, string Y, 
            string XDynamics, string YDynamics, 
            string XBehavior, string YBehavior, 
            string CanGlue, string Tip)
        {
            // There can be more than one Controls Row so need to think through how to handle existing rows.

            Validate_Controls_SectionExists(shape);

            short newRow = shape.AddRow(
                (short)Visio.VisSectionIndices.visSectionControls,
                (short)Visio.VisRowIndices.visRowControl,
                (short)Visio.VisRowTags.visTagDefault);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlX].FormulaU = X;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlY].FormulaU = Y;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlXDyn].FormulaU = XDynamics;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlYDyn].FormulaU = YDynamics;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlXCon].FormulaU = XBehavior;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlYCon].FormulaU = YBehavior;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlGlue].FormulaU = CanGlue;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    (short)Visio.VisRowIndices.visRowControl + 0,
                    (short)Visio.VisCellIndices.visCtlTip].FormulaU = string.Format("\"{0}\"", Tip);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Populate_Controls_Section(Visio.Shape shape, string rowName,
            string X, string Y,
            string XDynamics, string YDynamics,
            string XBehavior, string YBehavior,
            string CanGlue, string Tip)
        {
            // There can be more than one Controls Row so need to think through how to handle existing rows.

            Validate_Controls_SectionExists(shape);

            short newRow = shape.AddNamedRow(
                (short)Visio.VisSectionIndices.visSectionControls,
                rowName,
                (short)Visio.VisRowTags.visTagDefault);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlX].FormulaU = X;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlY].FormulaU = Y;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlXDyn].FormulaU = XDynamics;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlYDyn].FormulaU = YDynamics;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlXCon].FormulaU = XBehavior;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlYCon].FormulaU = YBehavior;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlGlue].FormulaU = CanGlue;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionControls,
                    newRow,
                    (short)Visio.VisCellIndices.visCtlTip].FormulaU = string.Format("\"{0}\"", Tip);
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Set_TextBlockFormat_Section(Visio.Shape shape, 
            string LeftMargin, string TopMargin, string RightMargin, string BottomMargin, 
            string TextDirection, string VerticalAlign, string TextBkgnd, string TextBkgndTrans, string DefaultTabStop)
        {
            Validate_TextBlockFormat_SectionExists(shape);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkLeftMargin].FormulaU = LeftMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkTopMargin].FormulaU = TopMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkRightMargin].FormulaU = RightMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkBottomMargin].FormulaU = BottomMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkDirection].FormulaU = TextDirection;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkVerticalAlign].FormulaU = VerticalAlign;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkBkgnd].FormulaU = TextBkgnd;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkBkgndTrans].FormulaU = TextBkgndTrans;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkDefaultTabStop].FormulaU = DefaultTabStop;

            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Set_TextBlockMargins(Visio.Shape shape, 
            string LeftMargin, 
            string TopMargin, 
            string RightMargin, 
            string BottomMargin)
        {
            // TODO(crhodes):
            // Consider making some of the arguments optional with reasonable defaults

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkLeftMargin].FormulaU = LeftMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkTopMargin].FormulaU = TopMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkRightMargin].FormulaU = RightMargin;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowText,
                    (short)Visio.VisCellIndices.visTxtBlkBottomMargin].FormulaU = BottomMargin;
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        internal static void Set_TextTransform_Section(Visio.Shape shape,
            string Width, string Height, 
            string PinX, string PinY, 
            string LocPinX, string LocPinY, 
            string Angle)
        {
            Validate_TextXForm_SectionExists(shape);

            try
            {
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject, 
                    (short)Visio.VisRowIndices.visRowTextXForm, 
                    (short)Visio.VisCellIndices.visXFormWidth].FormulaU = Width;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormHeight].FormulaU = Height;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormPinX].FormulaU = PinX;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormPinY].FormulaU = PinY;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormLocPinX].FormulaU = LocPinX;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormLocPinY].FormulaU = LocPinY;
                shape.CellsSRC[
                    (short)Visio.VisSectionIndices.visSectionObject,
                    (short)Visio.VisRowIndices.visRowTextXForm,
                    (short)Visio.VisCellIndices.visXFormAngle].FormulaU = Angle;
            }
            catch (Exception ex)
            {
                VisioHelper.DisplayInWatchWindow(ex.ToString());
            }
        }

        private static void Validate_Action_SectionExists(Visio.Shape shape)
        {
            if (0 == shape.SectionExists[(short)Visio.VisSectionIndices.visSectionAction, 0])
            {
                try
                {
                    var result = shape.AddSection((short)Visio.VisSectionIndices.visSectionAction);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_Controls_SectionExists(Visio.Shape shape)
        {
            if (0 == shape.SectionExists[(short)Visio.VisSectionIndices.visSectionControls, 0])
            {
                try
                {
                    var result = shape.AddSection((short)Visio.VisSectionIndices.visSectionControls);
                    //result = shape.AddRow(
                    //    (short)Visio.VisSectionIndices.visSectionControls, 
                    //    (short)Visio.VisRowIndices.visRowControl, 
                    //    (short)Visio.VisRowTags.visTagDefault);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_Prop_SectionExists(Visio.Shape shape)
        {
            // NB. Shape Data = visSectionProp

            if (0 == shape.SectionExists[(short)Visio.VisSectionIndices.visSectionProp, 0])
            {
                try
                {
                    var result = shape.AddSection((short)Visio.VisSectionIndices.visSectionProp);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_TextBlockFormat_SectionExists(Visio.Shape shape)
        {
            // TextBlockFormat exists as a row in the SectionObject!

            if (0 == shape.RowExists[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowText,
                (short)Visio.VisExistsFlags.visExistsAnywhere])
            {
                try
                {
                    shape.AddRow(
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowText,
                        (short)Visio.VisRowTags.visTagDefault);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_TextXForm_SectionExists(Visio.Shape shape)
        {
            // TextXForm exists as a row in the SectionObject!

            if (0 == shape.RowExists[
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowTextXForm,
                (short)Visio.VisExistsFlags.visExistsAnywhere])
            {
                try
                {
                    shape.AddRow(
                        (short)Visio.VisSectionIndices.visSectionObject,
                        (short)Visio.VisRowIndices.visRowTextXForm,
                        (short)Visio.VisRowTags.visTagDefault);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_TextField_SectionExists(Visio.Shape shape)
        {
            if (0 == shape.RowExists[
                (short)Visio.VisSectionIndices.visSectionTextField,
                (short)Visio.VisRowIndices.visRowText,
                (short)Visio.VisExistsFlags.visExistsAnywhere])
            {
                try
                {
                    shape.AddRow(
                        (short)Visio.VisSectionIndices.visSectionTextField,
                        (short)Visio.VisRowIndices.visRowText,
                        (short)Visio.VisRowTags.visTagDefault);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }

        private static void Validate_User_SectionExists(Visio.Shape shape)
        {
            if (0 == shape.SectionExists[(short)Visio.VisSectionIndices.visSectionUser, 0])
            {
                try
                {
                    var result = shape.AddSection((short)Visio.VisSectionIndices.visSectionUser);
                }
                catch (Exception ex)
                {
                    VisioHelper.DisplayInWatchWindow(ex.ToString());
                }
            }
        }


        #endregion

    }
}
