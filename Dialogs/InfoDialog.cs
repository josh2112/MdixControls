
namespace Com.Josh2112.MdixControls.Dialogs
{
    /// <summary>
    /// Presents a title, message, and OK button.
    /// </summary>
    public class InfoDialog : PromptDialog
    {
        public InfoDialog( string title, string message )
            : base( title, message, ButtonDef.OK ) { }
    }
}
