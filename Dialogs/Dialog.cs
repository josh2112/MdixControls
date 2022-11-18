using Nito.AsyncEx;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Com.Josh2112.MdixControls.Dialogs
{
    public class DialogResult<T>
    {
        private readonly TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

        public void Set( T result ) => tcs.TrySetResult( result );

        public Task<T> WaitAsync( CancellationToken? ct = null ) =>
            ct.HasValue ? tcs.Task.WaitAsync( ct.Value ) : tcs.Task;
    }

    public interface IHasDialogResult { }

    public interface IHasDialogResult<T> : IHasDialogResult
    {
        DialogResult<T> Result { get; }
    }

    public class Dialog : ContentControl
    {
        public static readonly DependencyProperty DialogMarginProperty = DependencyProperty.Register( nameof( DialogMargin ), typeof( Thickness ),
            typeof( Dialog ), new PropertyMetadata( default( Thickness ) ) );

        public Thickness DialogMargin
        {
            get => (Thickness)GetValue( DialogMarginProperty );
            set => SetValue( DialogMarginProperty, value );
        }

        public bool HandlesFocus { get; protected set; } = false;

        static Dialog() =>
            DefaultStyleKeyProperty.OverrideMetadata( typeof( Dialog ), new FrameworkPropertyMetadata( typeof( Dialog ) ) );
    }
}
