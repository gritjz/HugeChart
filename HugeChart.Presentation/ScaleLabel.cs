using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HugeChart.Presentation
{
    public abstract class ScaleLabel : ScaleComponent
    {
        public ScaleLabel()
        {
            SetCurrentValue(FontFamilyProperty, new FontFamily("ConSolas"));
        }



        public HorizontalAlignment HorizontalLabelAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalLabelAlignmentProperty); }
            set { SetValue(HorizontalLabelAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalLabelAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalLabelAlignmentProperty =
            DependencyProperty.Register("HorizontalLabelAlignment", typeof(HorizontalAlignment), typeof(ScaleLabel),
                new PropertyMetadata(default(HorizontalAlignment), OnParameterChanged));





        public VerticalAlignment VerticalLabelAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalLabelAlignmentProperty); }
            set { SetValue(VerticalLabelAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalLabelAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalLabelAlignmentProperty =
            DependencyProperty.Register("VerticalLabelAlignment", typeof(VerticalAlignment), typeof(ScaleLabel),
                new PropertyMetadata(default(VerticalAlignment), OnParameterChanged));



        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ForegroundProperty)
                Refresh();
            else if (e.Property == BackgroundProperty)
                Refresh();
            else if (e.Property == FontSizeProperty)
                Refresh();
        }

        /// <summary>
        /// no render when step = 0
        /// </summary>
        /// <returns></returns>
        protected override bool CanNotRender()
           => base.CanNotRender() || Step == 0;
    }
}
