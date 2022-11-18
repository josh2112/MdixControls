
namespace Com.Josh2112.MdixControls.Dialogs
{
    /// <summary>
    /// Represents a traditional UI button. This class is meant to provide an easy
    /// way to define the button configuration of a dialog or a prompt. A button
    /// can have a "connotation" of positive, neutral, or negative. It's up to the
    /// specific app or UI how to visiaully represent these connotations.
    ///
    /// The constructor is hidden from the outside world. To create a
    /// button, use of the factory methods Negative(), Positive(),
    /// or Neutral(), passing it the button text. 
    ///  
    /// Also provided are factory methods for some some very common
    /// buttons such as OK, Cancel, and Remove.
    /// 
    /// If you have initialized a Localizer (from Com.Josh2112.Utilities), it will
    /// be used to find translations for the built-in labels ("OK", "Cancel", etc.)
    /// </summary>
    public class ButtonDef
    {
        public enum Connotations
        {
            Negative,
            Positive,
            Neutral
        }

        public string Text { get; private set; }
        public Connotations Connotation { get; private set; }

        private ButtonDef( string text, Connotations type ) { Text = text; Connotation = type; }

        public static ButtonDef Negative( string text ) => new ButtonDef( text, Connotations.Negative );
        public static ButtonDef Positive( string text ) => new ButtonDef( text, Connotations.Positive );
        public static ButtonDef Neutral( string text ) => new ButtonDef( text, Connotations.Neutral );

        public static ButtonDef OK { get; } = new ButtonDef( "OK", Connotations.Positive );
        public static ButtonDef Cancel { get; } = new ButtonDef( "Cancel", Connotations.Neutral );

        public static ButtonDef Yes { get; } = new ButtonDef( "Yes", Connotations.Positive );
        public static ButtonDef No { get; } = new ButtonDef( "No", Connotations.Negative );
        public static ButtonDef NoNeutral { get; } = new ButtonDef(  "No", Connotations.Neutral );

        public static ButtonDef Continue { get; } = new ButtonDef( "Continue", Connotations.Positive );
        public static ButtonDef Remove { get; } = new ButtonDef( "Remove", Connotations.Negative );

        public static ButtonDef Retry { get; } = new ButtonDef( "Retry", Connotations.Neutral );
    }
}
