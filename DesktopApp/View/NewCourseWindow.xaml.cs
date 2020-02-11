using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace DesktopApp.View
{
	public partial class NewCourseWindow : Window
	{
		public NewCourseWindow( )
		{
			InitializeComponent( );
		}


		private bool IsTextAllowed( string text )
		{
			return Regex.IsMatch( text, "[0-9]+" );
		}

		private void PreviewTextInputHandler( object sender, TextCompositionEventArgs e )
		{
			e.Handled = !IsTextAllowed( e.Text );
		}

		private void DataObjectPastingHandler( object sender, DataObjectPastingEventArgs e )
		{
			if( e.DataObject.GetDataPresent( typeof( string ) ) )
			{
				string text = (string)e.DataObject.GetData( typeof( string ) );
				if( !IsTextAllowed( text ) )
				{
					e.CancelCommand( );
				}
			}
			else
			{
				e.CancelCommand( );
			}
		}

		private void PreviewKeyDownHandler( object sender, KeyEventArgs e )
		{
			if( e.Key == Key.Space )
			{
				e.Handled = true;
			}
		}

		private void SelectionChangedHandler( object sender, SelectionChangedEventArgs e )
		{			
			string newTrainingId = (sender as ComboBox).SelectedValue.ToString( );
			TrainingSelectionChangedEvent?.Invoke( this, newTrainingId );
		}


		#region Events
		public event EventHandler<string> TrainingSelectionChangedEvent;
		#endregion
	}
}