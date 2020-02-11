using Persistence;
using Persistence.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DesktopApp.Model
{
	public interface ITRService
	{
		string UserName { get; }

		Task<IEnumerable<string>> GetTrainingIds( );
		Task<IEnumerable<string>> GetSubjects( string trainingId );
		Task<IEnumerable<CourseDTO>> GetTeacherCourses( );
		Task<IEnumerable<StudentDTO>> GetStudentsOnCourse( string courseId );
		
		Task AddNewCourse( NewCourseDTO newCourseDTO );
		Task AddGrade( string userName, string courseId, EGrade grade );
		Task<bool> Login( string userName, string password );
		Task Logout( );
	}
}