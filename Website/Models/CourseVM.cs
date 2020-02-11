using System.Collections.Generic;
using Persistence;


namespace Website.Models
{
	public class CourseVM
	{
		public string CourseId { get; set; } // "IK-WAF_E+GY/4" - 4. kurzus
		public string SubjectId { get; set; } // Tárgy azonosító

		public string TeacherName { get; set; } // Tanár neve
		public string Schedule { get; set; } // Órarend
		public int CurStudents { get; set; } // Jelenleg ennyi hallgató van rajta
		public int MaxStudents { get; set; } // Max ennyi hallgató lehet
		public ICollection<EGrade> Grades { get; set; } // Jegyek
		public string CourseAction { get; set; } // Felvétel vagy Leadás vagy semmi
		public bool Passed { get; set; } // Teljesítve van-e
	}
}