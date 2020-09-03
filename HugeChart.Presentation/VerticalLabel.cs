using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HugeChart.Presentation
{
    public class VerticalLabel: ScaleLabel
    {
        protected override double Normalize(double v) => RenderSize.Height - (v - Range.Min) / Range.Distance * RenderSize.Height;

        protected override void Refresh()
        {
            root.Children.Clear();
            if (CanNotRender()) return;

            // double v = Minimum;
            double v = Range.Min;
            double height = RenderSize.Height / (Range.Distance / Step);
            double y;
            do
            {
                y = Normalize(v);
                var label = new Label
                {
                    Content = v.ToString(),
                    Height = height,
                    Width = RenderSize.Width,
                    //HorizontalContentAlignment = HorizontalLabelAlignment,
                    //VerticalContentAlignment = VerticalAlignment,
                    HorizontalContentAlignment = HorizontalAlignment,
                    VerticalContentAlignment = VerticalLabelAlignment,
                    Foreground = Foreground,
                    FontSize = FontSize,
                    FontFamily = FontFamily
                };
                root.Children.Add(label);
                Canvas.SetTop(label, y - height / 2);
                v += Step;
            } while (v <= Range.Max);
        }

    }
}
