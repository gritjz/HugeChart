using HugeChart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Image = System.Windows.Controls.Image;
using Color = System.Windows.Media.Color;

namespace HugeChart.Presentation
{
    public class ChartDataLineAsync : UserControl
    {
        private double dlHeight;
        public double dlx { get; private set; }
        public double dly { get; private set; }
        public Canvas CanvasRoot { get; private set; }

        public ChartDataLineAsync()
        {
            CanvasRoot = new Canvas();
            Content = CanvasRoot;

            ChartDataSeries points = new ChartDataSeries();
            Random random = new Random();
            for (int i = 0; i < 100000; i++) 
            {
                points.Add(
                    new System.Windows.Point(
                        random.Next(0, 300),
                        random.Next(-100, 100)
                    ));
            }
            ChartDataSeries = points;

            SizeChanged += (s, e) => { ResetScale(); Refresh(); };
        }


        public ChartDataSeries ChartDataSeries
        {
            get { return (ChartDataSeries)GetValue(ChartDataSeriesProperty); }
            set { SetValue(ChartDataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartDataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartDataSeriesProperty =
            DependencyProperty.Register("ChartDataSeries", typeof(ChartDataSeries), typeof(ChartDataLineAsync),
                new PropertyMetadata(default(ChartDataSeries)));




        public Range HorizontalRange
        {
            get { return (Range)GetValue(HorizontalRangeProperty); }
            set { SetValue(HorizontalRangeProperty, value); }
        }
        /// <summary>
        /// <see cref="HorizontalRange"/>
        /// </summary>
        public static readonly DependencyProperty HorizontalRangeProperty = DependencyProperty.Register
                                                                        (
                                                                            nameof(HorizontalRange),
                                                                            typeof(Range),
                                                                            typeof(ChartDataLineAsync),
                                                                            new PropertyMetadata(
                                                                                default(Range),
                                                                                OnParamterChanged)
                                                                        );

        public Range VerticalRange
        {
            get { return (Range)GetValue(VerticalRangeProperty); }
            set { SetValue(VerticalRangeProperty, value); }
        }
        /// <summary>
        /// <see cref="VerticalRange"/>
        /// </summary>
        public static readonly DependencyProperty VerticalRangeProperty = DependencyProperty.Register
                                                                        (
                                                                            nameof(VerticalRange),
                                                                            typeof(Range),
                                                                            typeof(ChartDataLineAsync),
                                                                            new PropertyMetadata(
                                                                                default(Range),
                                                                                OnParamterChanged)
                                                                        );




        private static void OnParamterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ChartDataLineAsync)?.ResetScale();
        }

        private void ResetScale()
        {
            dlx = 0;
            dly = 0;

            if (HorizontalRange == null || VerticalRange == null)
                return;
            if (HorizontalRange.Distance == 0 || VerticalRange.Distance == 0)
                return;

            if (double.IsNaN(Width))
                dlx = RenderSize.Width / HorizontalRange.Distance;
            else
                dlx = Width / HorizontalRange.Distance;

            if (double.IsNaN(Height))
                dlHeight = RenderSize.Height;
            else
                dlHeight = Height;

            dly = dlHeight / VerticalRange.Distance;
        }

        private System.Drawing.PointF Normalize(System.Windows.Point p) => new System.Drawing.PointF(
           (float)((p.X - HorizontalRange.Min) * dlx),
            (float)(dlHeight - (p.Y - VerticalRange.Min) * dly)
            );


        public void Refresh()
        {
            CanvasRoot.Children.Clear();
            if (dlx == 0 || dly == 0)
                return;

            if (ChartDataSeries.Any())
            {
                var points = new List<PointF>();
                ChartDataSeries.ToList().ForEach(
                    p =>
                    {
                        points.Add(Normalize(p));

                    }
                );

                if (points.Count() <= 1) return;

               
                WriteableBitmap bitmap = new WriteableBitmap
                (
                    (int)this.RenderSize.Width,
                    (int)this.RenderSize.Height,
                    96, 96,
                    PixelFormats.Bgra32,
                    null
                );
                Image figure = new Image { Source = bitmap };
                bitmap.Lock(); 
                using (System.Drawing.Bitmap buff = new System.Drawing.Bitmap(
                        (int)bitmap.Width,
                        (int)bitmap.Height,
                        bitmap.BackBufferStride,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                        bitmap.BackBuffer))
                {
                    // GDI+ Drawing
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(buff))
                    {
                        Color color = (Foreground as SolidColorBrush).Color;
                        var brush = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                        var pen = new System.Drawing.Pen(brush, 1); //1 is minimum, bigger thickness could effect performance
                        graphics.DrawLines(pen, points.ToArray());
                        graphics.Flush();
                    }
                }

                bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)bitmap.Width, (int)bitmap.Height));
                bitmap.Unlock();

                CanvasRoot.Children.Add(figure);
            }


        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ForegroundProperty)
                Refresh();
        }

    }
}
