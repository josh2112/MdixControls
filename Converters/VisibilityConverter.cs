using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Com.Josh2112.MdixControls.Converters
{
    /// <summary>
    /// Determines a System.Windows.Visibility value for some object. The conversion
    /// is specified by the VisibleIf parameter.
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public enum VisibilityModes
        {
            IsTrue,
            IsFalse,
            IsNonNull,
            IsNull,
            StringIsNullOrWhitespace
        }

        public VisibilityModes VisibleIf { get; set; } = VisibilityModes.IsTrue;

        public bool NoCollapse { get; set; } = false;

        private Visibility BooleanToVisibility( bool val ) =>
            val ? Visibility.Visible : (NoCollapse ? Visibility.Hidden : Visibility.Collapsed);

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            switch( VisibleIf )
            {
                case VisibilityModes.IsTrue:
                    return BooleanToVisibility( System.Convert.ToBoolean( value ) );
                case VisibilityModes.IsFalse:
                    return BooleanToVisibility( !System.Convert.ToBoolean( value ) );
                case VisibilityModes.IsNonNull:
                    return BooleanToVisibility( value != null );
                case VisibilityModes.IsNull:
                    return BooleanToVisibility( value == null );
                case VisibilityModes.StringIsNullOrWhitespace:
                    return BooleanToVisibility( !string.IsNullOrWhiteSpace( (string)value ) );
                default:
                    return Visibility.Visible;
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
