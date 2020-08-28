using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeChart.Data
{
    /// <summary>
    /// Data Range 
    /// </summary>
    [TypeConverter(typeof(RangeConverter))]
    public class Range
    {
        //prop生成 get set
        public double From { get; set; }
        public double To { get; set; }
        public double Max => Math.Max(From, To);
        public double Min => Math.Min(From, To);
        public double Distance => Math.Abs(Max - Min);
        
        //ctor生成Constructor
        public Range()
        {

        }

        public Range(double from, double to)
        {
            this.From = from;
            this.To = to;
        }

        public override string ToString() => $"{From}, {To}";
    }


}
