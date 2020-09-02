using HugeChart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HugeChart.Presentation
{
    /// <summary>
    /// Interaction logic for HorizontalScale.xaml
    /// </summary>
    public abstract class ScaleControl : UserControl
    {
        protected Canvas root;
        public ScaleControl()
        {
            root = new Canvas();
            Content = root;
            //Use when needs Maximum and Minimum Dependency
            //SetCurrentValue(MinimumProperty, 0d);
            //SetCurrentValue(MaximumProperty, 100d);

            //Range and Step
            SetCurrentValue(RangeProperty, new Range { From = 0d, To = 100d });
            SetCurrentValue(StepProperty, 10d);

            //Stroke color length and thickness
            SetCurrentValue(StrokeProperty, Brushes.Lime);
            SetCurrentValue(StrokeLengthProperty, 12d);
            SetCurrentValue(StrokeThicknessProperty, 2d);


            SizeChanged += (s, e) =>
            {
                (s as ScaleControl)?.Refresh();
            };
        }


        #region Maximum and Minimum for scale, No need after setup Range class
        //public double Maximum
        //{
        //    get { return (double)GetValue(MaximumProperty); }
        //    set { SetValue(MaximumProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MaximumProperty =
        //    DependencyProperty.Register("Maximum", typeof(double), typeof(HorizontalScale), new PropertyMetadata(default(double), OnParameterChanged));



        //public double Minimum
        //{
        //    get { return (double)GetValue(MinimumProperty); }
        //    set { SetValue(MinimumProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MinimumProperty =
        //    DependencyProperty.Register("Minimum", typeof(double), typeof(HorizontalScale), new PropertyMetadata(default(double), OnParameterChanged));
        #endregion

        public Range Range
        {
            get { return (Range)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Range.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range", typeof(Range), typeof(ScaleControl), new PropertyMetadata(default(Range), OnParameterChanged));

        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(ScaleControl), new PropertyMetadata(default(double), OnParameterChanged));

        /// <summary>
        /// Stroke Color
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(ScaleControl), new PropertyMetadata(default(Brush), OnParameterChanged));

        /// <summary>
        /// Stroke Length
        /// </summary>
        public double StrokeLength
        {
            get { return (double)GetValue(StrokeLengthProperty); }
            set { SetValue(StrokeLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeLengthProperty =
            DependencyProperty.Register("StrokeLength", typeof(double), typeof(ScaleControl), new PropertyMetadata(default(double), OnParameterChanged));


        /// <summary>
        /// Stroke Thickness
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(ScaleControl), new PropertyMetadata(default(double), OnParameterChanged));




        protected static void OnParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as ScaleControl)?.Refresh();

        protected abstract void Refresh();


        //private double Normalize(double v) => (v - Minimum) / (Maximum - Minimum) * RenderSize.Width;
        protected abstract double Normalize(double v);
    }
}
