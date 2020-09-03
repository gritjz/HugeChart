using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace HugeChart.Presentation
{
    public class HorizontalLabel : ScaleLabel
    {
        protected override double Normalize(double v) => (v - Range.Min) / Range.Distance * RenderSize.Width;

        protected override void Refresh()
        {
            root.Children.Clear();

            if (CanNotRender()) return;

            // double v = Minimum;
            double v = Range.Min;
            double width = RenderSize.Width / (Range.Distance / Step);
            double x;
            do
            {
                x = Normalize(v);
                var label = new Label
                {
                    Content = v.ToString(),
                    Width = width,
                    VerticalAlignment = VerticalAlignment,
                    HorizontalContentAlignment = HorizontalLabelAlignment,
                    Foreground = Foreground,
                    FontSize = FontSize,
                    FontFamily = FontFamily
                };
                root.Children.Add(label);
                Canvas.SetLeft(label, x - width / 2);
                v += Step;
            } while (v <= Range.Max);
        }
    }
}
