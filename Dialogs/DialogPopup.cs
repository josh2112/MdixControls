using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Com.Josh2112.MdixControls.Dialogs
{
    [TemplatePart( Name = "PART_Popup", Type = typeof( Popup ) )]
    [TemplateVisualState( GroupName = "PopupStates", Name = "Open" )]
    [TemplateVisualState( GroupName = "PopupStates", Name = "Closed" )]
    public class DialogPopup : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register( nameof( IsOpen ), typeof( bool ),
            typeof( DialogPopup ), new PropertyMetadata( false, ( s, e ) => (s as DialogPopup)!.OnIsOpenChanged() ) );

        public static readonly DependencyProperty DialogThemeProperty = DependencyProperty.Register( nameof( DialogTheme ), typeof( BaseTheme ),
            typeof( DialogPopup ), new PropertyMetadata( default( BaseTheme ) ) );

        /// <summary>
        /// Animates the dialog open or closed.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue( IsOpenProperty ); }
            set { SetValue( IsOpenProperty, value ); }
        }

        /// <summary>
        /// Sets the theme (light/dark) for the dialog.
        /// </summary>
        public BaseTheme DialogTheme
        {
            get => (BaseTheme)GetValue( DialogThemeProperty );
            set => SetValue( DialogThemeProperty, value );
        }

        private readonly UIElement owner;

        static DialogPopup() =>
            DefaultStyleKeyProperty.OverrideMetadata( typeof( DialogPopup ), new FrameworkPropertyMetadata( typeof( DialogPopup ) ) );

        internal DialogPopup( UIElement owner, Dialog dialog )
        {
            this.owner = owner;
            Content = dialog;
        }

        public override void OnApplyTemplate()
        {
            (GetTemplateChild( "PART_Popup" ) as Popup)!.PlacementTarget = owner;

            base.OnApplyTemplate();

            VisualStateManager.GoToState( this, "Closed", false );

            IsOpen = true;
        }

        private void OnIsOpenChanged() => VisualStateManager.GoToState( this, IsOpen ? "Open" : "Closed", true );
    }
}
