using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Ink;
//using IdentityMine.Avalon.PatientSimulator;
//using IdentityMine.Avalon.Controls;
//using PatientHelper;
using System.Windows.Media.Media3D;
using XamlTransitions;

namespace VNCAssemblyViewer
{
    public class Detail3DManager
    {

        #region Globals

        FrameworkElement detailBack2D;
        FrameworkElement detailBack2DWrapper;
        TranslateTransform detailBack2DWrapperTranslate;
        TranslateTransform detailFront2DWrapperTranslate;
        TranslateTransform masterView2DWrapperTranslate;
        FrameworkElement detailFront2D;
        FrameworkElement detailMain;
        FrameworkElement baseFrameworkElement;
        FrameworkElement masterView2D;

        bool gridRelayout = false;
        XamlTransitions.Rotate3D detailRotater3DTransition;
        XamlTransitions.Rotate3D masterRotater3DTransition;

        // Timer
        private System.Windows.Threading.DispatcherTimer uiMasterView3DTimer;
        private const double timerMasterView3DFrameRate = 200.0; // 1000 / 200 = 5 fps

        #endregion

        public Detail3DManager(object fe)
        {
            baseFrameworkElement = fe as FrameworkElement;

            if (baseFrameworkElement == null)
                return;

            detailFront2D = (FrameworkElement)baseFrameworkElement.FindName("DetailFront2D");
            detailBack2D = (FrameworkElement)baseFrameworkElement.FindName("DetailBack2D");
            detailBack2DWrapperTranslate = (TranslateTransform)baseFrameworkElement.FindName("DetailBack2DWrapperTranslate");
            detailFront2DWrapperTranslate = (TranslateTransform)baseFrameworkElement.FindName("DetailFront2DWrapperTranslate");
            masterView2DWrapperTranslate = (TranslateTransform)baseFrameworkElement.FindName("MasterView2DWrapperTranslate");
            detailBack2DWrapper = (FrameworkElement)baseFrameworkElement.FindName("DetailBack2DWrapper");
            detailMain = (FrameworkElement)baseFrameworkElement.FindName("DetailMain");
            detailRotater3DTransition = (XamlTransitions.Rotate3D)baseFrameworkElement.FindName("DetailRotater3DTransition");
            detailRotater3DTransition.RotateCompleted += new XamlTransitions.Rotate3D.SelectedEventHandler(serverDetailRotater3DTransition_RotateCompleted);

            masterView2D = (FrameworkElement)baseFrameworkElement.FindName("MasterView2D");
            masterRotater3DTransition = (XamlTransitions.Rotate3D)baseFrameworkElement.FindName("Rotater3DTransition");
            masterRotater3DTransition.RotateCompleted += new XamlTransitions.Rotate3D.SelectedEventHandler(patientMasterRotater3DTransition_RotateCompleted);

            detailMain.LayoutUpdated += new EventHandler(detailMain_LayoutUpdated); // detail page changes layout with in the grid and 2D and 3D elements need to be updated

            detailBack2DWrapperTranslate.X = 10000; // move back offscreen

            //setup a timer for updating the MasterView3D area. Due to bugs in VisualBrush on Beta2. This technique is being used
            //brushMaster = new VisualBrush(visual);
            uiMasterView3DTimer = new System.Windows.Threading.DispatcherTimer();
            uiMasterView3DTimer.Interval = TimeSpan.FromMilliseconds(timerMasterView3DFrameRate);
            uiMasterView3DTimer.Tick += new EventHandler(uiTimerMasterView3D_Tick);
            uiMasterView3DTimer.Stop();
        }

        #region Events

        void serverDetailRotater3DTransition_RotateCompleted(object sender, EventArgs e)
        {
            double angleTo = (double)detailRotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateToProperty);
            double angleFrom = (double)detailRotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty);

            if (angleTo == 180)
            {
                detailRotater3DTransition.Visibility = Visibility.Collapsed;
                detailBack2DWrapperTranslate.X = 0;

                uiMasterView3DTimer.Start();
            }

            if (angleTo == 0)
            {
                detailFront2DWrapperTranslate.X = 0;
                masterView2DWrapperTranslate.X = 0;
                uiMasterView3DTimer.Stop();
                detailRotater3DTransition.Visibility = Visibility.Collapsed;
                masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)null);
            }


            if (angleTo == 90)
            {
                if (angleFrom == 0)
                {
                    detailMain.SetValue(Grid.ColumnProperty, (int)1);
                    detailMain.SetValue(Grid.ColumnSpanProperty, (int)2);
                    gridRelayout = true; // event occurs aftre layout is done - snap shot is done at this time
                }

                if (angleFrom == 180)
                {
                    detailMain.SetValue(Grid.ColumnProperty, (int)2);
                    detailMain.SetValue(Grid.ColumnSpanProperty, (int)1);
                    gridRelayout = true; // event occurs aftre layout is done - snap shot is done at this time

                    // Rotate Master View
                    masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)30);
                    masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)0);
                    masterRotater3DTransition.Rotate();
                    double scaleTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.ScaleXProperty);
                    masterRotater3DTransition.AnimateScaleXTo(scaleTo);
                    double translateTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.TranslateXProperty);
                    masterRotater3DTransition.AnimateTranslateXTo(translateTo);
                    double scaleYTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.ScaleYProperty);
                    masterRotater3DTransition.AnimateScaleYTo(scaleYTo);
                }
            }
        }

        void patientMasterRotater3DTransition_RotateCompleted(object sender, EventArgs e)
        {

        }

        void detailMain_LayoutUpdated(object sender, EventArgs e)
        {
            double angleTo = (double)detailRotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateToProperty);
            double angleFrom = (double)detailRotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty);

            if (gridRelayout == true)
            {
                gridRelayout = false;

                if (angleFrom == 0)
                {
                    Brush brushBack = Transition3DHelper.CreateBrushFromUIElementWithBitmap(detailBack2D);
                    DiffuseMaterial dmBack = new DiffuseMaterial(brushBack);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.BackMaterialProperty, (Material)dmBack);

                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)90);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)180);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.DurationTimeProperty, (double)0.15);
                    detailRotater3DTransition.Rotate();
                }

                if (angleFrom == 180)
                {
                    Brush brushFront = Transition3DHelper.CreateBrushFromUIElementWithBitmap(detailFront2D);
                    DiffuseMaterial dmFront = new DiffuseMaterial(brushFront);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)dmFront);

                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)90);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)0);
                    detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.DurationTimeProperty, (double)0.15);

                    detailRotater3DTransition.Rotate();
                }
            }
        }

        #endregion

        #region Public

        public void ShowDetailFront()
        {
            Brush brushBack = Transition3DHelper.CreateBrushFromUIElementWithBitmap(detailBack2D);
            DiffuseMaterial dmBack = new DiffuseMaterial(brushBack);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.BackMaterialProperty, (Material)dmBack);
            detailRotater3DTransition.Visibility = Visibility.Visible;

            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)180);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)90);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.DurationTimeProperty, (double)0.15);

            detailRotater3DTransition.Rotate();

            // Rotate Master View
            Brush brushFront = Transition3DHelper.CreateBrushFromUIElementWithBitmap(masterView2D);
            DiffuseMaterial dmFront = new DiffuseMaterial(brushFront);
            masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)dmFront);

            detailFront2DWrapperTranslate.X = 1000;
            detailBack2DWrapperTranslate.X = 10000;
        }

        public void ShowDetailBack()
        {
            Brush brushFront = Transition3DHelper.CreateBrushFromUIElementWithBitmap(detailFront2D);
            DiffuseMaterial dmFront = new DiffuseMaterial(brushFront);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)dmFront);
            detailRotater3DTransition.Visibility = Visibility.Visible;

            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)0);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)90);
            detailRotater3DTransition.SetValue(XamlTransitions.Rotate3D.DurationTimeProperty, (double)0.15);

            detailRotater3DTransition.Rotate();

            // Rotate Master View
            brushFront = Transition3DHelper.CreateBrushFromUIElementWithBitmap(masterView2D);
            dmFront = new DiffuseMaterial(brushFront);
            masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)dmFront);

            masterRotater3DTransition.Visibility = Visibility.Visible;
            masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty, (double)0);
            masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.AngleRotateToProperty, (double)30);
            masterRotater3DTransition.Rotate();
            double scaleTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.ScaleXToProperty);
            masterRotater3DTransition.AnimateScaleXTo(scaleTo);
            double translateTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.TranslateXToProperty);
            masterRotater3DTransition.AnimateTranslateXTo(translateTo);
            double scaleYTo = (double)masterRotater3DTransition.GetValue(XamlTransitions.Rotate3D.ScaleYToProperty);
            masterRotater3DTransition.AnimateScaleYTo(scaleYTo);

            detailFront2DWrapperTranslate.X = 10000;
            detailBack2DWrapperTranslate.X = 10000;
            masterView2DWrapperTranslate.X = 10000;
        }

        #endregion

        #region Private

        void uiTimerMasterView3D_Tick(object sender, EventArgs e)
        {
            Brush brushFront = Transition3DHelper.CreateBrushFromUIElementWithBitmap(masterView2D);
            DiffuseMaterial dmFront = new DiffuseMaterial(brushFront);
            masterRotater3DTransition.SetValue(XamlTransitions.Rotate3D.FrontMaterialProperty, (Material)dmFront);
        }

        #endregion

    }
}
