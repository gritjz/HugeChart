using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HugeChart.Presentation
{
    public class ChartFrameLine : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get
            {
                double width = double.IsNaN(Width) ? RenderSize.Width : Width;
                double height = double.IsNaN(Height) ? RenderSize.Height : Height;
                if (width <= 0 || height <= 0) return StreamGeometry.Empty;
                if (HorizontalFrameCount <= 1 || VerticalFrameCount <= 1) return StreamGeometry.Empty;

                StreamGeometry stream = new StreamGeometry();

                using (StreamGeometryContext geometry = stream.Open())
                {
                    //Counter of current drawing line, set 1 to avoid left end
                    int count = 1;
                    //offset is the distance between two lines
                    double offset = width / HorizontalFrameCount;
                    Point p1, p2;
                    do
                    {
                        p1 = new Point(count * offset, 0);
                        p2 = new Point(count * offset, height);

                        geometry.BeginFigure(p1, false, false);
                        geometry.LineTo(p2, true, false);

                        count += 1;
                    } while (count < HorizontalFrameCount);

                    //reset count to draw vertical line
                    count = 1;
                    offset = height / VerticalFrameCount;

                    do
                    {
                        p1 = new Point(0, count * offset);
                        p2 = new Point(width, count * offset);

                        geometry.BeginFigure(p1, false, false);
                        geometry.LineTo(p2, true, false);

                        count += 1;
                    } while (count < VerticalFrameCount);

                }
                return stream;
            }
        }
        public ChartFrameLine()
        {
            SetCurrentValue(HorizontalFrameCountProperty, 10d);
            SetCurrentValue(VerticalFrameCountProperty, 10d);
            SetCurrentValue(StrokeProperty, Brushes.Lime);
            SetCurrentValue(StrokeThicknessProperty, 0.5);
            SetCurrentValue(StrokeDashArrayProperty, new DoubleCollection() {2.0, 10.0});
            SizeChanged += (s, e) =>
            {
                var panel = this.Parent as Panel;
                if (panel == null) return;

                Binding actualWidthBinding = new Binding(nameof(ActualWidth))
                {
                    Source = panel
                };
                SetBinding(WidthProperty, actualWidthBinding);

                Binding actualHeightBinding = new Binding(nameof(ActualHeight))
                {
                    Source = panel
                };
                SetBinding(HeightProperty, actualHeightBinding);
            };
        }



        /// <summary>
        /// Horizontal background frame line count
        /// </summary>
        public double HorizontalFrameCount
        {
            get { return (double)GetValue(HorizontalFrameCountProperty); }
            set { SetValue(HorizontalFrameCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalFrameCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalFrameCountProperty =
            DependencyProperty.Register(nameof(HorizontalFrameCount), typeof(double), typeof(ChartFrameLine),
                new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// vertical background frame line count
        /// </summary>
        public double VerticalFrameCount
        {
            get { return (double)GetValue(VerticalFrameCountProperty); }
            set { SetValue(VerticalFrameCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalFrameCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalFrameCountProperty =
            DependencyProperty.Register(nameof(VerticalFrameCount), typeof(double), typeof(ChartFrameLine),
                 new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));






    }
}
