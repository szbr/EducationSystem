using DesktopApp.Model;
using Persistence.DTO;
using System;
using System.Collections.ObjectModel;


namespace DesktopApp.ViewModel
{
	public class ManageVM : ViewModelBase
	{
		private readonly ITRService model;
		private readonly string courseId;


		public ManageVM( ITRService model, string courseId )
		{
			this.model = model;
			this.courseId = courseId;

			AddGradeCmd = new DelegateCommand( p => AddGrade( p as string ) );

			LoadStudentsAsync( );
		}


		private async void LoadStudentsAsync( )
		{
			try
			{				
				Students = new ObservableCollection<StudentVM>( );
				foreach( StudentDTO dto in await model.GetStudentsOnCourse( courseId ) )
				{
					Students.Add( new StudentVM( dto ) );
				}
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
			}
		}

		private void AddGrade( string studentId )
		{
			GetSelectedGradeEventArgs e = new GetSelectedGradeEventArgs
			{
				StudentId = studentId
			};
			GetSelectedGradeEvent?.Invoke( this, e );

			try
			{
				model.AddGrade( studentId, courseId, e.Grade );

				foreach( StudentVM svm in Students )
				{
					if( svm.Data.UserName == studentId )
					{
						svm.AddGrade( e.Grade );
					}
				}
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
			}
		}


		#region Bindings
		private ObservableCollection<StudentVM> students;
		public ObservableCollection<StudentVM> Students
		{
			get => students;
			set
			{
				students = value;
				OnPropertyChanged( );
			}
		}
		#endregion


		#region Delegates
		public DelegateCommand AddGradeCmd { get; }
		#endregion


		#region Events
		public event EventHandler<GetSelectedGradeEventArgs> GetSelectedGradeEvent;
		#endregion
	}
}