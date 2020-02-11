using DesktopApp.Model;
using DesktopApp.View;
using DesktopApp.ViewModel;
using Persistence;
using System;
using System.Configuration;
using System.Windows;


namespace DesktopApp
{
	public partial class App : Application
	{
		private ITRService service;
		private LoginWindow loginWindow;
			private LoginVM loginVM;
		private MainWindow mainWindow;
			private MainVM mainVM;
		private ManageWindow manageWindow;
			private ManageVM manageVM;
		private NewCourseWindow newCourseWindow;
			private NewCourseVM newCourseVM;


		public App( )
		{
			Startup += StartupHandler;
		}

		private void StartupHandler( object sender, StartupEventArgs e )
		{
			service = new TRService( ConfigurationManager.AppSettings["baseAddress"] );
			//service = new TRServiceMOCK( );
			CreateLoginWindow( );
		}

		private void ExitHandler( object sender, ExitEventArgs e )
		{
			if( service.UserName != null )
			{
				service.Logout( );
			}
		}

		private void CreateLoginWindow( )
		{
			loginVM = new LoginVM( service );
			//==[Events]===================================
				loginVM.MessageEvent += VMMessageHandler;
				loginVM.TryLoginEvent += VMTryLoginHandler;
			//=============================================

			loginWindow = new LoginWindow
			{
				DataContext = loginVM
			};

			loginWindow.Show( );
		}

		private void CreateMainWindow( )
		{
			mainVM = new MainVM( service );
			//==[Events]===================================
				mainVM.MessageEvent += VMMessageHandler;
				mainVM.NewCourseEvent += VMNewCourseHandler;
				mainVM.ManageEvent += VMManageHandler;
				mainVM.LogoutEvent += VMLogoutHandler;
			//=============================================

			mainWindow = new MainWindow
			{
				DataContext = mainVM
			};

			mainWindow.Show( );
		}


		#region Handlers
		private void VMTryLoginHandler( object sender, bool bResult )
		{
			if( bResult )
			{
				CreateMainWindow( );		
				loginWindow.Close( );
			}
			else
			{
				MessageBox.Show( "Login failed.", "TR" );
			}
		}

		private void VMNewCourseHandler( object sender, EventArgs e )
		{
			newCourseVM = new NewCourseVM( service );
			//==[Events]===================================
				newCourseVM.MessageEvent += VMMessageHandler;
				newCourseVM.SubmitNewCourseEvent += VMSubmitNewCourseHandler;
			//=============================================

			newCourseWindow = new NewCourseWindow( )
			{
				DataContext = newCourseVM
			};
			newCourseWindow.TrainingSelectionChangedEvent += TrainingSelectionChangedHandler;
			newCourseWindow.ShowDialog( );
		}

		private void VMSubmitNewCourseHandler( object sender, SubmitNewCourseEventArgs e )
		{
			MessageBoxResult res = MessageBox.Show( "Submit new course:\n\n" + 
				"Training: "		+ e.SelectedTraining + "\n" +
				"Subject: "			+ e.NewCourse.SelectedSubject + "\n" +
				"Max Students: "	+ e.NewCourse.MaxStudents + "\n" +
				"Weekday: "			+ StaticData.GetData( StaticData.Weekdays, e.NewCourse.SelectedWeekdayIdx ) + "\n" +			
				"Hour: "			+ StaticData.GetData( StaticData.Hours, e.NewCourse.SelectedHourIdx ) + "\n" +
				"Minute: "			+ StaticData.GetData( StaticData.Minutes, e.NewCourse.SelectedMinuteIdx ) + "\n" +
				"Duration: "		+ StaticData.GetData( StaticData.Durations, e.NewCourse.SelectedDurationIdx ) + "\n\n" +
				"Are you sure?",
				"Submit new course",
				MessageBoxButton.OKCancel );

			if( res == MessageBoxResult.OK )
			{
				try
				{
					service.AddNewCourse( e.NewCourse );
				}
				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
					return;
				}
				MessageBox.Show( "Success!." );
			}
		}

		private void VMManageHandler( object sender, string courseId )
		{
			manageVM = new ManageVM( service, courseId );
			//==[Events]===================================
				manageVM.MessageEvent += VMMessageHandler;
				manageVM.GetSelectedGradeEvent += VMGetSelectedGradeHandler;
			//=============================================

			manageWindow = new ManageWindow
			{
				DataContext = manageVM
			};
			manageWindow.ShowDialog( );
		}

		private void VMGetSelectedGradeHandler( object sender, GetSelectedGradeEventArgs e )
		{
			string g = manageWindow.GetGrade( e.StudentId );
			if( string.IsNullOrEmpty(g) )
				return;

			e.Grade = (EGrade)Convert.ToInt32( g );
		}

		private void VMLogoutHandler( object sender, EventArgs e )
		{
			CreateLoginWindow( );
			mainWindow.Close( );
		}

		private void VMMessageHandler( object sender, string msg )
		{
			MessageBox.Show( msg, "TR" );
		}

		private void TrainingSelectionChangedHandler( object sender, string newTrainingId )
		{
			newCourseVM.TrainingSelectionChangedAsync( newTrainingId );
		}
		#endregion
	}
}