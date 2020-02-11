using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace DesktopApp.ViewModel
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null )
		{
			PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		protected void OnMessage( string msg )
		{
			MessageEvent?.Invoke( this, msg );
		}

		
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<string> MessageEvent;
		#endregion
	}
}