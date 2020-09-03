
using HugeChart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HugeChart.Presentation
{
    public abstract class ScaleComponent : UserControl
    {
        protected Canvas root;
        public ScaleComponent()
        {
            root = new Canvas();
            Content = root;

            //Range and Step
            SetCurrentValue(RangeProperty, new Range { From = 0d, To = 100d });
            SetCurrentValue(StepProperty, 10d);

            SizeChanged += (s, e) =>
            {
                (s as ScaleComponent)?.Refresh();
            };
        }

        public Range Range
        {
            get { return (Range)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Range.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range", typeof(Range), typeof(ScaleComponent), new PropertyMetadata(default(Range), OnParameterChanged));

        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(ScaleComponent), new PropertyMetadata(default(double), OnParameterChanged));
        
        
        protected static void OnParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as ScaleControl)?.Refresh();

        protected abstract void Refresh();


        //private double Normalize(double v) => (v - Minimum) / (Maximum - Minimum) * RenderSize.Width;
        protected abstract double Normalize(double v);

        /// <summary>
        /// If rendersize <=0 or data range is unavailable, No need to render data.
        /// </summary>
        protected virtual bool CanNotRender() 
            => RenderSize.Width <= 0 
            || RenderSize.Height <= 0 
            || Range == null 
            || Range.Distance == 0;
    }

}
