using Com.Josh2112.MdixControls.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Com.Josh2112.MdixControls.Converters
{
    /// <summary>
    /// Returns the display name or description of a given enum value or Type,
    /// optionally changing the case.
    /// </summary>
    public class EnumOrTypeToTextConverter : IValueConverter
    {
        public enum Sources { DisplayName, Description }
        public enum Cases { None, Lower, Upper }

        public Sources Source { get; set; } = Sources.DisplayName;

        public Cases ChangeCase { get; set; } = Cases.None;

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            string str;

            if( value is Enum en ) str = Source == Sources.Description ? en.GetDescription() : en.GetDisplayName();
            else if( value is Type ty ) str = Source == Sources.Description ? ty.GetDescription() : ty.GetDisplayName();
            else if( value != null ) str = value.ToString() ?? "(unknown)";
            else str = "(null)";

            switch( ChangeCase )
            {
                case Cases.Lower: return str.ToLower();
                case Cases.Upper: return str.ToUpper();
                default: return str;
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
