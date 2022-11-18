using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Com.Josh2112.MdixControls.Dialogs
{
    /// <summary>
    /// Dialog host modeled after the one in MaterialDesignInXAML.
    /// </summary>
    [TemplatePart( Name = "PART_DialogContainer", Type = typeof( Grid ) )]
    [TemplateVisualState( GroupName = "DialogHostStates", Name = "Open" )]
    [TemplateVisualState( GroupName = "DialogHostStates", Name = "Closed" )]
    public class DialogHost : ContentControl
    {
        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register(
            nameof( OverlayBackground ), typeof( Brush ), typeof( DialogHost ), new PropertyMetadata( Brushes.Black ) );

        /// <summary>
        /// The overlay brush that is used to dim the background behind the dialog
        /// </summary>
        public Brush OverlayBackground
        {
            get => (Brush)GetValue( OverlayBackgroundProperty );
            set => SetValue( OverlayBackgroundProperty, value );
        }

        private Grid dialogContainer = null!;

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( DialogHost ), new FrameworkPropertyMetadata( typeof( DialogHost ) ) );
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Grab out a reference to the dialog container and make sure we start in the "closed" state
            dialogContainer = (GetTemplateChild( "PART_DialogContainer" ) as Grid)!;
            VisualStateManager.GoToState( this, "Closed", false );
        }

        public IEnumerable<Dialog> Dialogs => dialogContainer.Children.Cast<DialogPopup>().Select( c => (c.Content as Dialog)! );

        public IEnumerable<Dialog> OpenDialogs => dialogContainer.Children.Cast<DialogPopup>().Where( c => c.IsOpen ).Select( c => (c.Content as Dialog)! );

        public Dialog? CurrentDialog => OpenDialogs.LastOrDefault();

        public async Task<T?> ShowDialogForResultAsync<T>( IHasDialogResult<T> dialog, CancellationToken? cancelToken = null )
        {
            VisualStateManager.GoToState( this, "Open", true );
         
            var dialogPopup = new DialogPopup( dialogContainer, (dialog as Dialog)! );
            dialogContainer.Children.Add( dialogPopup );

            // Focus the dialog after it's been shown (unless it wants to focus itself)
            if( dialog is Dialog d && !d.HandlesFocus )
                d.Loaded += ( s, e ) => d.Dispatcher.Invoke( () => System.Windows.Input.Keyboard.Focus( d ), DispatcherPriority.ApplicationIdle );

            var result = default( T );

            try
            {
                // Wait for the dialog to complete
                result = await dialog.Result.WaitAsync( cancelToken );
            }
            catch( TaskCanceledException )
            {
            }

            // Animate the dialog closed
            dialogPopup.IsOpen = false;

            // If this was the last dialog, close the dialog container
            if( !OpenDialogs.Any() )
                VisualStateManager.GoToState( this, "Closed", true );

            // After the animation has completed, remove the dialog
            await Dispatcher.BeginInvoke( (Action)(async () =>
            {
                await Task.Delay( 300 );
                dialogContainer.Children.Remove( dialogPopup );
            }) );

            return result;
        }
    }

    public static class DialogHostWindowExtensions
    {
        public static DialogHost GetDialogHost( this Window window ) => (window.Content as DialogHost)!;

        public static Task<T?> ShowDialogForResultAsync<T>( this Window window, IHasDialogResult<T> dialog, CancellationToken? cancelToken = null ) =>
            window.GetDialogHost().ShowDialogForResultAsync( dialog, cancelToken );
    }
}