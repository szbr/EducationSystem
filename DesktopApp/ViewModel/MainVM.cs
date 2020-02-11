using DesktopApp.Model;
using Persistence.DTO;
using System;
using System.Collections.ObjectModel;


namespace DesktopApp.ViewModel
{
	public class MainVM : ViewModelBase
	{
		private readonly ITRService model;


		public MainVM( ITRService model )
		{
			this.model = model;

			LoggedInUser = "Logged in as " + model.UserName;

			NewCourseCmd = new DelegateCommand( p => NewCourseEvent?.Invoke( this, EventArgs.Empty ) );
			ManageCmd = new DelegateCommand( p => Manage( p as string ) );
			LogoutCmd = new DelegateCommand( p => Logout( ) );

			LoadCoursesAsync( );
		}


		private async void LoadCoursesAsync( )
		{
			try
			{				
				Courses = new ObservableCollection<CourseVM>( );
				foreach( CourseDTO dto in await model.GetTeacherCourses( ) )
				{
					Courses.Add( new CourseVM { Data = dto } );
				}
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
			}
		}

		private void Manage( string courseId )
		{
			ManageEvent?.Invoke( this, courseId );
		}

		private void Logout( )
		{
			try
			{
				model.Logout( );
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
				return;
			}
			LogoutEvent?.Invoke( this, EventArgs.Empty );
		}


		#region Bindings
		public string LoggedInUser { get; set; }

		private ObservableCollection<CourseVM> courses;
		public ObservableCollection<CourseVM> Courses
		{
			get => courses;
			set
			{
				courses = value;
				OnPropertyChanged( );
			}
		}
		#endregion


		#region Delegates
		public DelegateCommand NewCourseCmd { get; }
		public DelegateCommand ManageCmd { get; }
		public DelegateCommand LogoutCmd { get; }
		#endregion


		#region Events
		public event EventHandler NewCourseEvent;
		public event EventHandler<string> ManageEvent;
		public event EventHandler LogoutEvent;
		#endregion
	}
}