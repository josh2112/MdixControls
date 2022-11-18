using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Josh2112.MdixControls.Utilities
{
    [Flags]
    public enum DescriptionRetreivalFlags { None = 0x0, Upper = 0x1, Lower = 0x2 }

    static public class TypeUtils
    {
        public static T? GetAttribute<T>( this Type value ) where T : Attribute => Attribute.GetCustomAttribute( value, typeof( T ) ) as T;

        public static string GetDisplayName( this Type typeVal, DescriptionRetreivalFlags modifiers = 0 ) =>
            typeVal.GetDisplayName( typeVal.Name, modifiers );

        public static string GetDescription( this Type typeVal ) =>
            typeVal.GetDescription( typeVal.Name );
    }

    static public class EnumUtils
    {
        public static T? GetAttribute<T>( this Enum value ) where T : Attribute =>
            ((object)value).GetAttribute<T>();

        public static string GetDisplayName( this Enum enumVal, DescriptionRetreivalFlags modifiers = 0 ) =>
            enumVal.GetDisplayName( enumVal.ToString(), modifiers );

        public static string GetDescription( this Enum enumVal ) =>
            enumVal.GetDescription( enumVal.ToString() );

        public static string GetQualifiedName( this Enum enumVal ) =>
            enumVal.GetType().Name + "." + enumVal.ToString();

        public static IEnumerable<T> AllValues<T>() where T : struct => Enum.GetValues( typeof( T ) ).Cast<T>();

        public static IList<T> ToFlagList<T>( this T enumVal ) where T : struct =>
            AllValues<T>().Where( f => ((Enum)(object)enumVal).HasFlag( (Enum)(object)f ) ).ToList();

        /// <summary>
        /// Wrapper around System.Enum.Parse() which checks that the result
        /// actually a member of the enum (which Parse() doesn't guarantee!)
        /// and returns null if not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? Parse<T>( string value ) where T : struct, IConvertible
        {
            var result = (T)Enum.Parse( typeof( T ), value );
            return Enum.IsDefined( typeof( T ), result ) ? (T?)result : null;
        }
    }

    static internal class Helpers
    {
        public static T? GetAttribute<T>( this object value ) where T : Attribute
        {
            var fieldInfo = value.ToString() is string str ? value.GetType().GetField( str ) : null;
            return fieldInfo?.GetCustomAttributes( typeof( T ), false )?.FirstOrDefault() as T ?? null;
        }

        /// <summary>
        /// Returns the Name property from DisplayName or Display attribute (whichever found first)
        /// attached to this object, applying the given modifiers. If neither attribute is found,
        /// returns the fallback value.
        /// Modifiers:
        ///     Plural: Returns PluralName property instead (if set; falls back to Name)
        ///     Upper: Converts to upper-case before returning
        ///     Lower: Converts to lower-case before returning
        /// </summary>
        /// <param name="typeVal"></param>
        /// <param name="fallback"></param>
        /// <param name="modifiers">DescriptionRetreivalFlags</param>
        /// <returns></returns>
        public static string GetDisplayName( this object typeVal, string fallback, DescriptionRetreivalFlags modifiers = 0 )
        {
            string result = typeVal.GetAttribute<DisplayAttribute>()?.Name?? StringUtils.PrettifyIdentifier( fallback );

            if( modifiers.HasFlag( DescriptionRetreivalFlags.Upper ) )
                return result.ToUpperInvariant();
            else if( modifiers.HasFlag( DescriptionRetreivalFlags.Lower ) )
                return result.ToLowerInvariant();
            else
                return result;
        }

        /// <summary>
        /// Returns Description property of DescriptionAttribute or DisplayAttribute, whichever is found first.
        /// </summary>
        /// <param name="typeVal"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static string GetDescription( this object typeVal, string fallback ) =>
            typeVal.GetAttribute<DescriptionAttribute>()?.Description ??
                typeVal.GetAttribute<DisplayAttribute>()?.Name ??
                StringUtils.PrettifyIdentifier( fallback );
    }

    public static class StringUtils
    {
        /// <summary>
        /// Adds spaces into camel-cased text to make it 'pretty'.
        /// ex: "IsRunningSimulation" => "is running simulation"
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static string PrettifyIdentifier( this string identifier ) => string.IsNullOrWhiteSpace( identifier ) ? "" :
            identifier[0] + string.Concat( identifier.Skip( 1 ).Select( x => char.IsUpper( x ) ? " " + char.ToLower( x ) : x.ToString() ) );

        /// <summary>
        /// Creates camel-case text out of a phrase containing whitespace.
        /// ex: "is running simulation?" => "isRunningSimulation"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToCamelCase( this string text )
        {
            var chars = text.ToCharArray();
            for( int i = 1; i < chars.Length; ++i )
                if( char.IsWhiteSpace( chars[i - 1] ) )
                    chars[i] = char.ToUpper( chars[i] );

            var filtered = chars.Where( c => char.IsLetterOrDigit( c ) || c == '_' ).ToArray();
            return char.ToLower( filtered[0] ) + new string( filtered.Skip( 1 ).ToArray() );
        }
    }
}
