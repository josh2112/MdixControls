using System.Linq;

namespace Com.Josh2112.MdixControls
{
    public static class Parsing
    {
        public static int ParseDollarAmount( string text )
        {
            // $2065.37, ($2594.42), -$1,261.87
            float val = 1;
            text = text.Replace( "$", "" ).Replace( ",", "" );
            if( text.First() == '(' )
            {
                val = -1;
                text = text.Substring( 1, text.Length - 2 );
            }
            return (int)System.Math.Round( val * float.Parse( text ) * 100 );
        }
    }
}
