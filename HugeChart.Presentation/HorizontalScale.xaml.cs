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
            SetCurrentValue(RangeProperty, new Range { From = 0d, To = 100d });
            SetCurrentValue(StepProperty, 10d);

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
                    Y2 = 10,
                    Stroke = Brushes.Brown,
                    StrokeThickness = 2d,
                });
                v += Step;
            } while (v <= Range.Max);
        }

        //private double Normalize(double v) => (v - Minimum) / (Maximum - Minimum) * RenderSize.Width;
        private double Normalize(double v) => (v - Range.Min) / Range.Distance * RenderSize.Width;
    }
}
