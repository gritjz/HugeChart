using System;
using System.ComponentModel;
using System.Globalization;

namespace HugeChart.Data
{
    public class RangeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {

            //split string and assign to an array

            string removeSpace = value.ToString().Replace(" ", string.Empty);

            string[] splitedString = removeSpace.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //Need length of 2
            if (splitedString.Length == 2)
            { 
                return new Range(double.Parse(splitedString[0]), double.Parse(splitedString[1])); 
            }



            //Return Range
            return base.ConvertFrom(context, culture, value);
        }

    }


}
