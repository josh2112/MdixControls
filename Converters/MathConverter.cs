using System;
using System.Globalization;
using System.Windows.Data;

namespace Com.Josh2112.MdixControls.Converters
{
    public class MathConverter : IValueConverter
    {
        public enum Operations
        {
            IsLessThan,
        }

        public Operations Operation { get; set; }

        public double Operand { get; set; }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var val = System.Convert.ToDouble( value );

            return Operation switch {
                Operations.IsLessThan => val < Operand,
                _ => (object)false,
            };
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
