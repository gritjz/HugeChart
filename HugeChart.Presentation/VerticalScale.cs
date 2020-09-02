using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace HugeChart.Presentation
{
    public class VerticalScale : ScaleControl
    {
        protected override double Normalize(double v) => RenderSize.Height - (v - Range.Min) / Range.Distance * RenderSize.Height;

        protected override void Refresh()
        {
            root.Children.Clear();
            if (RenderSize.Width <= 0) return;

            // double v = Minimum;
            double v = Range.Min;
            double y;
            do
            {
                y = Normalize(v);
                root.Children.Add(
                new Line
                {
                    X1 = 0,
                    X2 = this.StrokeLength,
                    Y1 = y,
                    Y2 = y,
                    Stroke = this.Stroke,
                    StrokeThickness = this.StrokeThickness,
                });
                v += Step;
            } while (v <= Range.Max);
        }


    }

}
