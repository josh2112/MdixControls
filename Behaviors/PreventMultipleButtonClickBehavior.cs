using Microsoft.Xaml.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Com.Josh2112.MdixControls.Behaviors
{
    /// <summary>
    /// This behavior prevents multiple clicks on a button by intercepting the PreviewMouseDown event
    /// and marking it as handled if the click count is greater than 1. There's an attached property,
    /// IsEnabled, that can be used to include the behavior in a style.
    /// </summary>
    public class PreventMultipleButtonClickBehavior : Behavior<ButtonBase>
    {
        public static DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached( "IsEnabled", typeof( bool ),
            typeof( PreventMultipleButtonClickBehavior ), new UIPropertyMetadata( false, OnIsEnabledChanged ) );

        public static void SetIsEnabled( DependencyObject target, bool value ) =>
            target.SetValue( IsEnabledProperty, value );

        public static bool GetIsEnabled( DependencyObject target ) =>
            (bool)target.GetValue( IsEnabledProperty );

        private static void OnIsEnabledChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if( !(d is ButtonBase) ) return;
            var behaviors = Interaction.GetBehaviors( d );

            if( (bool)e.NewValue && !(bool)e.OldValue )
            {
                if( !behaviors.Any( b => b is PreventMultipleButtonClickBehavior ) ) behaviors.Add( new PreventMultipleButtonClickBehavior() );
            }
            else if( (bool)e.OldValue && !(bool)e.NewValue )
            {
                var pmbcBehaviors = behaviors.Where( b => b is PreventMultipleButtonClickBehavior ).ToList();
                foreach( var b in pmbcBehaviors ) behaviors.Remove( b );
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseDown += ButtonBase_PreviewMouseDown;
        }

        private void ButtonBase_PreviewMouseDown( object sender, MouseButtonEventArgs e )
        {
            if( e.ClickCount > 1 ) e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= ButtonBase_PreviewMouseDown;
            base.OnDetaching();
        }
    }
}
