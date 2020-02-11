using System.Windows;
using System.Windows.Controls;


namespace DesktopApp.View
{
	public partial class ManageWindow : Window
	{
		public ManageWindow( )
		{
			InitializeComponent( );
		}


		public string GetGrade( string studentId )
		{
			foreach( var item in itemsControl.Items )
			{
				ContentPresenter cp = (ContentPresenter)itemsControl.ItemContainerGenerator.ContainerFromItem( item );
				ComboBox cb = (ComboBox)cp.ContentTemplate.FindName( "cbox_manage", cp );

				if( cb.Tag.ToString( ) == studentId )
				{
					return ((ComboBoxItem)cb.SelectedItem).Content.ToString( );
				}
			}

			return null;
		}
	}
}