using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

namespace XamlTransitions
{
    /// <summary>
    /// Interaction logic for Rotate3D.xaml
    /// </summary>

    public partial class Rotate3D : Grid
    {

        public Rotate3D()
        {
            InitializeComponent();
        }

         private void OnLoaded(object sender, EventArgs e)
        {
            // Get the mesh objects for changing the material
            ModelVisual3D mv3d = myViewport3D.Children[0] as ModelVisual3D;
            Model3DGroup m3dgBase = mv3d.Content as Model3DGroup;

            Model3DGroup m3dg = m3dgBase.Children[0] as Model3DGroup;
            _FrontPlane = m3dg.Children[0] as GeometryModel3D;
            _BackPlane = m3dg.Children[1] as GeometryModel3D;

        }

        #region Dependecy Properties

        #region Dependency Properties AngleRotateTo

        public static readonly DependencyProperty AngleRotateToProperty = DependencyProperty.RegisterAttached("AngleRotateTo",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(AngleRotateToInvalidated)));

        public static double GetAngleRotateTo(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.AngleRotateToProperty));
        }

        public static void SetAngleRotateTo(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.AngleRotateToProperty, value);
        }

        private static void AngleRotateToInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties AngleRotateFrom

        public static readonly DependencyProperty AngleRotateFromProperty = DependencyProperty.RegisterAttached("AngleRotateFrom",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(AngleRotateFromInvalidated)));

        public static double GetAngleRotateFrom(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.AngleRotateFromProperty));
        }

        public static void SetAngleRotateFrom(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.AngleRotateFromProperty, value);
        }

        private static void AngleRotateFromInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties FrontMaterial

        public static readonly DependencyProperty FrontMaterialProperty = DependencyProperty.RegisterAttached("FrontMaterial",
            typeof(Material), typeof(Rotate3D), new FrameworkPropertyMetadata((Material)null, new PropertyChangedCallback(FrontMaterialInvalidated)));

        public static Material GetFrontMaterial(DependencyObject d)
        {
            return (Material)(d.GetValue(Rotate3D.FrontMaterialProperty));
        }

        public static void SetFrontMaterial(DependencyObject d, Material value)
        {
            d.SetValue(Rotate3D.FrontMaterialProperty, value);
        }

        private static void FrontMaterialInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties BackMaterial

        public static readonly DependencyProperty BackMaterialProperty = DependencyProperty.RegisterAttached("BackMaterial",
            typeof(Material), typeof(Rotate3D), new FrameworkPropertyMetadata((Material)null, new PropertyChangedCallback(BackMaterialInvalidated)));

        public static Material GetBackMaterial(DependencyObject d)
        {
            return (Material)(d.GetValue(Rotate3D.BackMaterialProperty));
        }

        public static void SetBackMaterial(DependencyObject d, Material value)
        {
            d.SetValue(Rotate3D.BackMaterialProperty, value);
        }

        private static void BackMaterialInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleX

        public static readonly DependencyProperty ScaleXProperty = DependencyProperty.RegisterAttached("ScaleX",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleXInvalidated)));

        public static double GetScaleX(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleXProperty));
        }

        public static void SetScaleX(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleXProperty, value);
        }

        private static void ScaleXInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleY

        public static readonly DependencyProperty ScaleYProperty = DependencyProperty.RegisterAttached("ScaleY",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleYInvalidated)));

        public static double GetScaleY(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleYProperty));
        }

        public static void SetScaleY(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleYProperty, value);
        }

        private static void ScaleYInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties TranslateX

        public static readonly DependencyProperty TranslateXProperty = DependencyProperty.RegisterAttached("TranslateX",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0.0, new PropertyChangedCallback(TranslateXInvalidated)));

        public static double GetTranslateX(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.TranslateXProperty));
        }

        public static void SetTranslateX(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.TranslateXProperty, value);
        }

        private static void TranslateXInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties TranslateY

        public static readonly DependencyProperty TranslateYProperty = DependencyProperty.RegisterAttached("TranslateY",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0.0, new PropertyChangedCallback(TranslateYInvalidated)));

        public static double GetTranslateY(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.TranslateYProperty));
        }

        public static void SetTranslateY(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.TranslateYProperty, value);
        }

        private static void TranslateYInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleXBack

        public static readonly DependencyProperty ScaleXBackProperty = DependencyProperty.RegisterAttached("ScaleXBack",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleXBackInvalidated)));

        public static double GetScaleXBack(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleXBackProperty));
        }

        public static void SetScaleXBack(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleXBackProperty, value);
        }

        private static void ScaleXBackInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleYBack

        public static readonly DependencyProperty ScaleYBackProperty = DependencyProperty.RegisterAttached("ScaleYBack",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleYBackInvalidated)));

        public static double GetScaleYBack(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleYBackProperty));
        }

        public static void SetScaleYBack(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleYBackProperty, value);
        }

        private static void ScaleYBackInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties TranslateXBackBack

        public static readonly DependencyProperty TranslateXBackProperty = DependencyProperty.RegisterAttached("TranslateXBack",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0.0, new PropertyChangedCallback(TranslateXBackInvalidated)));

        public static double GetTranslateXBack(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.TranslateXBackProperty));
        }

        public static void SetTranslateXBack(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.TranslateXBackProperty, value);
        }

        private static void TranslateXBackInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties TranslateYBack

        public static readonly DependencyProperty TranslateYBackProperty = DependencyProperty.RegisterAttached("TranslateYBack",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0.0, new PropertyChangedCallback(TranslateYBackInvalidated)));

        public static double GetTranslateYBack(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.TranslateYBackProperty));
        }

        public static void SetTranslateYBack(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.TranslateYBackProperty, value);
        }

        private static void TranslateYBackInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleXTo

        public static readonly DependencyProperty ScaleXToProperty = DependencyProperty.RegisterAttached("ScaleXTo",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleXToInvalidated)));

        public static double GetScaleXTo(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleXToProperty));
        }

        public static void SetScaleXTo(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleXToProperty, value);
        }

        private static void ScaleXToInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties ScaleYTo

        public static readonly DependencyProperty ScaleYToProperty = DependencyProperty.RegisterAttached("ScaleYTo",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(ScaleYToInvalidated)));

        public static double GetScaleYTo(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.ScaleYToProperty));
        }

        public static void SetScaleYTo(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.ScaleYToProperty, value);
        }

        private static void ScaleYToInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion

        #region Dependency Properties TranslateXTo

        public static readonly DependencyProperty TranslateXToProperty = DependencyProperty.RegisterAttached("TranslateXTo",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)1.0, new PropertyChangedCallback(TranslateXToInvalidated)));

        public static double GetTranslateXTo(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.TranslateXToProperty));
        }

        public static void SetTranslateXTo(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.TranslateXToProperty, value);
        }

        private static void TranslateXToInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
             #endregion

        #region Dependency Properties DurationTime

        public static readonly DependencyProperty DurationTimeProperty = DependencyProperty.RegisterAttached("DurationTime",
            typeof(double), typeof(Rotate3D), new FrameworkPropertyMetadata((double)0.0, new PropertyChangedCallback(DurationTimeInvalidated)));

        public static double GetDurationTime(DependencyObject d)
        {
            return (double)(d.GetValue(Rotate3D.DurationTimeProperty));
        }

        public static void SetDurationTime(DependencyObject d, double value)
        {
            d.SetValue(Rotate3D.DurationTimeProperty, value);
        }

        private static void DurationTimeInvalidated(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
        }
        #endregion
        #endregion

        #region Event Handlers

        public delegate void SelectedEventHandler(object sender, EventArgs e);

        public event SelectedEventHandler RotateCompleted;
        protected virtual void OnRotateCompleted(object o, EventArgs e)
        {
            if (RotateCompleted != null)
                RotateCompleted(o, e);
        }

        #endregion Event Handlers

        #region Events

        private void OnRotateStoryboard(object sender, EventArgs e)
        {
            Clock TimelineClock = (Clock)sender;

            // Started Clock
            if (TimelineClock.CurrentState == ClockState.Active)
            {

            }

            // Ended Clock
            if ((TimelineClock.CurrentState != ClockState.Active))
            {
                OnRotateCompleted(null, new EventArgs());
            }
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        public void Rotate()
        {
            Storyboard s = (Storyboard)this.FindResource("RotateStoryboard");

            // elementbinding doesn't work in Beta bits. So create the animation in code for now
            ParallelTimeline pt = s.Children[0] as ParallelTimeline;
            pt.Children.Clear();
            //double from = (double)this.GetValue(Rotate3D.AngleRotateFromProperty);
            double to = (double)this.GetValue(Rotate3D.AngleRotateToProperty);
            double duration = (double)this.GetValue(Rotate3D.DurationTimeProperty);
            pt.Children.Add(new DoubleAnimation(to, new Duration(TimeSpan.FromSeconds(duration)),FillBehavior.HoldEnd));

            this.BeginStoryboard(s);
        }

        public void AnimateScaleXTo(double to)
        {
            Storyboard s = (Storyboard)this.FindResource("ScaleXStoryboard");

            // elementbinding doesn't work in Beta bits. So create the animation in code for now
            ParallelTimeline pt = s.Children[0] as ParallelTimeline;
            pt.Children.Clear();
            double duration = (double)this.GetValue(Rotate3D.DurationTimeProperty);
            pt.Children.Add(new DoubleAnimation(to, new Duration(TimeSpan.FromSeconds(duration)), FillBehavior.HoldEnd));

            this.BeginStoryboard(s);
        }

        public void AnimateScaleYTo(double to)
        {
            Storyboard s = (Storyboard)this.FindResource("ScaleYStoryboard");

            // elementbinding doesn't work in Beta bits. So create the animation in code for now
            ParallelTimeline pt = s.Children[0] as ParallelTimeline;
            pt.Children.Clear();
            double duration = (double)this.GetValue(Rotate3D.DurationTimeProperty);
            pt.Children.Add(new DoubleAnimation(to, new Duration(TimeSpan.FromSeconds(duration)), FillBehavior.HoldEnd));

            this.BeginStoryboard(s);
        }


        public void AnimateTranslateXTo(double to)
        {
            Storyboard s = (Storyboard)this.FindResource("TranslateXStoryboard");

            // elementbinding doesn't work in Beta bits. So create the animation in code for now
            ParallelTimeline pt = s.Children[0] as ParallelTimeline;
            pt.Children.Clear();
            double duration = (double)this.GetValue(Rotate3D.DurationTimeProperty);
            pt.Children.Add(new DoubleAnimation(to, new Duration(TimeSpan.FromSeconds(duration)), FillBehavior.HoldEnd));

            this.BeginStoryboard(s);
        }

        #endregion

        #region Private Methods


        #endregion

        #region Globals

        // Geometry models
        GeometryModel3D _FrontPlane;
        GeometryModel3D _BackPlane;

        #endregion
    }

 }