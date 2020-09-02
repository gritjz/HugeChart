using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace HugeChart.Presentation
{
    public class HorizontalScale : ScaleControl
    {
        protected override double Normalize(double v) => (v - Range.Min) / Range.Distance * RenderSize.Width;

        protected override void Refresh()
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
    }
}
