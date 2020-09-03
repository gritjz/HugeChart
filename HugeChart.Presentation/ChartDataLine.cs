using HugeChart.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HugeChart.Presentation
{
    public class ChartDataLine : Shape
    {

        private double dlx;
        private double dly;
        private double dlHeight;

        public ChartDataLine()
        {
            SetCurrentValue(StrokeProperty, Brushes.Lime);
            SetCurrentValue(StrokeThicknessProperty, 1d);

            SetCurrentValue(HorizontalRangeProperty, new Range(0, 200));
            SetCurrentValue(VerticalRangeProperty, new Range(-100, 100));
            ChartDataSeries series = new ChartDataSeries()
            {
                new Point(  0,   0),
                new Point( 50, -20),
                new Point(100,  20),
                new Point(125,  60),
                new Point(150, -50),
                new Point(175, -80)
            };
            SetCurrentValue(ChartDataSeriesProperty, series);

            SizeChanged += (s, e) =>
            {
                ResetScale(s as ChartDataLine);
            };


        }

        public Range HorizontalRange
        {
            get { return (Range)GetValue(HorizontalRangeProperty); }
            set { SetValue(HorizontalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalRangeProperty =
            DependencyProperty.Register(nameof(HorizontalRange), typeof(Range), typeof(ChartDataLine),
                new PropertyMetadata(default(ChartDataSeries),
                    (s, e) =>
                    {
                        ResetScale(s as ChartDataLine);
                    }));



        public Range VerticalRange
        {
            get { return (Range)GetValue(VerticalRangeProperty); }
            set { SetValue(VerticalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VarticalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalRangeProperty =
            DependencyProperty.Register(nameof(VerticalRange), typeof(Range), typeof(ChartDataLine),
                 new PropertyMetadata(default(ChartDataSeries),
                    (s, e) =>
                    {
                        ResetScale(s as ChartDataLine);
                    }));



        public ChartDataSeries ChartDataSeries
        {
            get { return (ChartDataSeries)GetValue(ChartDataSeriesProperty); }
            set { SetValue(ChartDataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartDataSeriesProperty =
            DependencyProperty.Register(nameof(ChartDataSeries), typeof(ChartDataSeries), typeof(ChartDataLine),
                new PropertyMetadata(default(ChartDataSeries), OnChartDataSeriesChanged));

        private static void OnChartDataSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as ChartDataLine;
            if (self == null) return;

            self.InvalidateVisual();
        }

     


        protected override Geometry DefiningGeometry => GetGeometry();

        private Geometry GetGeometry()
        {
            if (dlx == 0 || dly == 0) return StreamGeometry.Empty;
            if (ChartDataSeries == null || ChartDataSeries.Count <= 1) return StreamGeometry.Empty;


            StreamGeometry stream = new StreamGeometry();
            using (StreamGeometryContext geom = stream.Open())
            {
                var points = new List<Point>();
                ChartDataSeries.ToList().ForEach(
                    p =>
                    {
                        points.Add(Normalize(p));
                    }
                    );
                geom.BeginFigure(points[0], false, false);
                geom.PolyLineTo(points, true, true);
            }
            return stream;
        }

        private Point Normalize(Point p) =>
            new Point
            (
                (p.X - HorizontalRange.Min) * dlx,
                dlHeight - (p.Y - VerticalRange.Min) * dly
            );

        private static void ResetScale(ChartDataLine line)
        {
           if(line == null) return;

            line.dlx = 0;
            line.dly = 0;

            if (line.HorizontalRange != null && line.HorizontalRange.Distance != 0)
            {
                if (!double.IsNaN(line.Width))
                    line.dlx = line.Width / line.HorizontalRange.Distance;
                else if (line.RenderSize.Width > 0)
                    line.dlx = line.RenderSize.Width / line.HorizontalRange.Distance;
            }
            if (line.VerticalRange != null && line.VerticalRange.Distance != 0)
            {
                if (!double.IsNaN(line.Height))
                {
                    line.dly = line.Height / line.VerticalRange.Distance;
                    line.dlHeight = line.Height;
                }
                else if (line.RenderSize.Height > 0)
                {
                    line.dly = line.RenderSize.Height / line.VerticalRange.Distance;
                    line.dlHeight = line.RenderSize.Height;
                }
            }

            line.InvalidateVisual();
        }


    }
}
