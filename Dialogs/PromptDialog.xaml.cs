using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Com.Josh2112.MdixControls.Dialogs
{
    /// <summary>
    /// The prompt dialog presents a title, message and series of buttons. It returns the button that was pressed.
    /// To present a message with a default OK button, use InfoDialog.
    /// </summary>
    public partial class PromptDialog : IHasDialogResult<ButtonDef>
    {
        public enum Answers { Cancel, No, Yes };

        public string Title { get; }
        public string Message { get; private set; }
        public ButtonDef[] Buttons { get; private set; }

        public DialogResult<ButtonDef> Result { get; } = new DialogResult<ButtonDef>();

        /// <summary>
        /// Creates a prompt dialog with the given title, question, and button list.
        /// Buttons will be laid out from left to right in the order given.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="question"></param>
        /// <param name="buttons"></param>
        public PromptDialog( string title, string message, params ButtonDef[] buttons )
        {
            Title = title;
            Message = message;
            Buttons = buttons;

            InitializeComponent();

            // Find the rightmost "positive" and "neutral" buttons and make them respond to Enter and Esc keys.
            buttonContainer.RealizeTemplatedItems( elements =>
            {
                var btns = elements.Cast<Button>().Reverse();

                var defaultButton = btns.FirstOrDefault( b => (b.DataContext as ButtonDef)!.Connotation == ButtonDef.Connotations.Positive );
                if( defaultButton != null ) defaultButton.IsDefault = true;

                var cancelButton = btns.FirstOrDefault( b => (b.DataContext as ButtonDef)!.Connotation == ButtonDef.Connotations.Neutral );
                if( cancelButton != null ) cancelButton.IsCancel = true;

                // If only one button (probably "OK"), make it respond to both Enter and Esc
                if( btns.Count() == 1 )
                {
                    btns.First().IsDefault = true;
                    btns.First().IsCancel = true;
                }
            } );
        }

        private void Button_Click( object sender, RoutedEventArgs e ) => Result.Set( ((sender as FrameworkElement)!.DataContext as ButtonDef)! );

        /// <summary>
        /// Generic prompt for closing a window with unsaved changes. The user is given options to save the changes,
        /// forget them, or don't close the window and keep working. Returns a predefined button def of either
        /// Yes, No, or Cancel.
        /// </summary>
        /// <param name="host">Window in which to show the dialog</param>
        /// <param name="title">Optional title</param>
        /// <param name="message">Optional prompt</param>
        /// <returns>Yes => save changes, No => discard changes, Cancel => cancel close</returns>
        public static async Task<Answers> AskSaveChangesAsync( Window host, string title = "Save changes?", string message = "There are unsaved changes. Do you want to save them?" )
        {
            ButtonDef cancelBtn = ButtonDef.Neutral( "Keep working" ), noBtn = ButtonDef.Neutral( "Forget changes" ),
                    yesBtn = ButtonDef.Positive( "Save" );

            var result = await host.ShowDialogForResultAsync( new PromptDialog( title, message, noBtn, cancelBtn, yesBtn ) );

            if( result == cancelBtn ) return Answers.Cancel;
            else if( result == noBtn ) return Answers.No;
            else return Answers.Yes;
        }

        public static async Task<bool> AskDeleteItemAsync( Window host, string itemName ) =>
            await host.ShowDialogForResultAsync( new PromptDialog( $"Remove {itemName}", $"Are you sure you want to remove this {itemName}?",
                ButtonDef.NoNeutral, ButtonDef.Remove ) ) == ButtonDef.Remove;
    }

    public static class ItemsControlExtensions
    {
        private static void RealizeTemplatedItems_impl( ItemsControl itemsControl, Action<IEnumerable<object>> whatToDo )
        {
            whatToDo( itemsControl.Items.Cast<object>().Select( item =>
            {
                var container = itemsControl.ItemContainerGenerator.ContainerFromItem( item );
                var contentPresenter = container as ContentPresenter ?? FindVisualDescendantOfType<ContentPresenter>( container );
                contentPresenter?.ApplyTemplate();
                return VisualTreeHelper.GetChild( contentPresenter, 0 );
            } ) );
        }

        private static T? FindVisualDescendantOfType<T>( DependencyObject obj, int indent = 0 ) where T : DependencyObject
        {
            if( obj == null ) return null;

            for( int i = 0; i < VisualTreeHelper.GetChildrenCount( obj ); ++i )
            {
                var child = VisualTreeHelper.GetChild( obj, i );
                var result = child as T ?? FindVisualDescendantOfType<T>( child, indent + 2 );
                if( result != null ) return result;
            }
            return null;
        }

        public static void RealizeTemplatedItems( this ItemsControl itemsControl, Action<IEnumerable<object>> whatToDo )
        {
            if( itemsControl.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated )
                RealizeTemplatedItems_impl( itemsControl, whatToDo );
            else
            {
                void onStatusChanged( object? s, EventArgs e )
                {
                    if( itemsControl.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated )
                    {
                        itemsControl.ItemContainerGenerator.StatusChanged -= onStatusChanged;
                        RealizeTemplatedItems_impl( itemsControl, whatToDo );
                    }
                }

                itemsControl.ItemContainerGenerator.StatusChanged += onStatusChanged;
            }
        }
    }
}
