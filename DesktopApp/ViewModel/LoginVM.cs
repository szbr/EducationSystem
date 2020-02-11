using DesktopApp.Model;
using System;
using System.Windows.Controls;


namespace DesktopApp.ViewModel
{
	public class LoginVM : ViewModelBase
	{
		private readonly ITRService model;


		public LoginVM( ITRService model )
		{
			this.model = model;

			LoginCmd = new DelegateCommand( p => Login( p ) );
		}


		private async void Login( object p )
		{
			PasswordBox pwbox = (PasswordBox)p;
			if( pwbox == null )
				return;

			try
			{
				bool bResult = await model.Login( UserName, pwbox.Password );
				TryLoginEvent?.Invoke( this, bResult );
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
			}
		}


		#region Bindings
		public string UserName { get; set; }
		#endregion


		#region Delegates
		public DelegateCommand LoginCmd { get; }
		#endregion


		#region Events
		public event EventHandler<bool> TryLoginEvent;
		#endregion
	}
}