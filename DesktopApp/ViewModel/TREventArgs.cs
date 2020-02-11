using Persistence;
using Persistence.DTO;
using System;


namespace DesktopApp.ViewModel
{
	public class GetSelectedGradeEventArgs : EventArgs
	{
		public string StudentId { get; set; }

		//[return]
		public EGrade Grade { get; set; }
	}

	public class SubmitNewCourseEventArgs : EventArgs
	{
		public string SelectedTraining { get; set; }
		public NewCourseDTO NewCourse { get; set; }
	}
}