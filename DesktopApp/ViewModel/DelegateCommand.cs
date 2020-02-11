using System;
using System.Windows.Input;


namespace DesktopApp.ViewModel
{
	public class DelegateCommand : ICommand
	{
		private readonly Action<object> execute;
		private readonly Func<object, bool> canExecute;

		public DelegateCommand( Action<object> execute ) : this( null, execute ) { }

		public DelegateCommand( Func<object, bool> canExecute, Action<object> execute )
		{
			this.canExecute = canExecute;
			this.execute = execute;
		}

		public bool CanExecute( object parameter )
		{
			return canExecute == null ? true : canExecute( parameter );
		}

		public void Execute( object parameter )
		{
			if( !CanExecute( parameter ) )
			{
				throw new InvalidOperationException( "Command execution is disabled." );
			}
			execute( parameter );
		}

		public void RaiseCanExecuteChanged( )
		{
			CanExecuteChanged?.Invoke( this, EventArgs.Empty );
		}

		public event EventHandler CanExecuteChanged;
	}
}