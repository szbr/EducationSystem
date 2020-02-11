using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence
{
	public class Course
	{
		public string CourseId { get; set; } // "IK-WAF_E+GY/4" - 4. kurzus

		[ForeignKey( "User" )]
		public int TeacherId { get; set; } // Tanár

		public string Schedule { get; set; } // Órarend, pl.: "Monday 10:00-12:00"
		public int MaxStudents { get; set; } // Max hány hallgató lehet a kurzuson

		public string SubjectId { get; set; } // Melyik tárgyhoz tartozik
		public Subject Subject { get; set; }
	}
}