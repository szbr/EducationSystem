using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence;
using Persistence.DTO;


//=====================================================
// WebAPI nelkul az asztali alkalmazas tesztelesehez
//
//=====================================================
namespace DesktopApp.Model
{
	public class TRServiceMOCK : ITRService
	{
		private readonly string[] trainingIds = new string[]{ "IK", "BG" };
		private readonly string[] subjectsIK = new string[]{ "WAF", "OAF", "CPP" };
		private readonly string[] subjectsBG = new string[]{ "PSZ", "KV", "GY" };
		private readonly CourseDTO[] courses = new CourseDTO[]
		{
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-CPP_E+GY/2", SubjectName = "CPP", Schedule = "Monday 11:00-14:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-NM1_E+GY/3", SubjectName = "Nummod 1", Schedule = "Monday 14:00-16:00" },
			new CourseDTO { CourseId = "IK-OAF_E+GY/1", SubjectName = "OAF", Schedule = "Friday 12:00-14:00" }
		};
		private readonly StudentDTO[] students = new StudentDTO[]
		{
			new StudentDTO { UserName = "USERAA", Name = "Fullname0", Timestamp = DateTime.Now, Grades = new EGrade[]{ EGrade.FAIL, EGrade.VERYGOOD } },
			new StudentDTO { UserName = "USERBB", Name = "Fullname1", Timestamp = DateTime.Now, Grades = new EGrade[]{ EGrade.FAIL} },
			new StudentDTO { UserName = "USERCC", Name = "Fullname2", Timestamp = DateTime.Now, Grades = new EGrade[]{  } },
			new StudentDTO { UserName = "USERDD", Name = "Fullname3", Timestamp = DateTime.Now, Grades = new EGrade[]{ EGrade.VERYGOOD } }
		};


		public string UserName { get; private set; }


		public async Task<IEnumerable<string>> GetTrainingIds( )
		{
			return trainingIds;
		}

		public async Task<IEnumerable<string>> GetSubjects( string trainingId )
		{
			return trainingId == "IK" ? subjectsIK : subjectsBG;
		}

		public async Task<IEnumerable<CourseDTO>> GetTeacherCourses( )
		{
			return courses;
		}

		public async Task<IEnumerable<StudentDTO>> GetStudentsOnCourse( string courseId )
		{
			return students;
		}

		public async Task AddNewCourse( NewCourseDTO newCourseDTO )
		{
			
		}

		public async Task AddGrade( string userName, string courseId, EGrade grade )
		{
			
		}

		public async Task<bool> Login( string userName, string password )
		{
			UserName = "USER_MOCK";
			return true;
		}

		public async Task Logout( )
		{
			UserName = null;
		}
	}
}