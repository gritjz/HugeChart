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
using HugeChart.Data;
namespace HugeChart.Presentation
{
    /// <summary>
    /// Interaction logic for HorizontalScale.xaml
    /// </summary>
    public partial class HorizontalScale : UserControl
    {

        public HorizontalScale()
        {
            InitializeComponent();
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
                (s as HorizontalScale)?.Refresh();
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
            DependencyProperty.Register("Range", typeof(Range), typeof(HorizontalScale), new PropertyMetadata(default(Range), OnParameterChanged));



        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(HorizontalScale), new PropertyMetadata(default(double), OnParameterChanged));




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
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(HorizontalScale), new PropertyMetadata(default(Brush), OnParameterChanged));

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
            DependencyProperty.Register("StrokeLength", typeof(double), typeof(HorizontalScale), new PropertyMetadata(default(double), OnParameterChanged));


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
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(HorizontalScale), new PropertyMetadata(default(double), OnParameterChanged));




        private static void OnParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as HorizontalScale)?.Refresh();

        private void Refresh()
        {
            root.Children.Clear();
            if (RenderSize.Width <= 0) return;

            // double v = Minimum;
            double v = Range.Min;
            double x;
            do
            {
                x = Normalize(v);
                root.Children.Add(
                new Line
                {
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = this.StrokeLength,
                    Stroke = this.Stroke,
                    StrokeThickness = this.StrokeThickness,
                });
                v += Step;
            } while (v <= Range.Max);
        }

        //private double Normalize(double v) => (v - Minimum) / (Maximum - Minimum) * RenderSize.Width;
        private double Normalize(double v) => (v - Range.Min) / Range.Distance * RenderSize.Width;
    }
}
