using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence
{
	public enum EGrade
	{
		FAIL = 1,
		PASS,
		MEDIOCRE,
		GOOD,
		VERYGOOD
	}

	public class Grade
	{
		public Grade( )
		{
			Timestamp = DateTime.Now;
		}

		public int GradeId { get; set; }
		
		public string CourseId { get; set; } // Melyik kurzusra lesz a jegy, pl.: "IK-WAF_E+GY/4"
		[ForeignKey( "User" )]
		public int UserId { get; set; } // Hallgató
		[ForeignKey( "User" )]
		public int TeacherId { get; set; } // Bejegyző tanár
		public DateTime Timestamp { get; set; } // Mikor
		public EGrade GradeRecord { get; set; } // Jegy 1-5
	}
}