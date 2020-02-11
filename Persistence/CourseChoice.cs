using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Persistence
{
	public class CourseChoice
	{
		public CourseChoice( )
		{
			Timestamp = DateTime.Now;
		}

		public int CourseChoiceId { get; set; }
		
		public string CourseId { get; set; } // "IK-WAF_E+GY/4"
		[ForeignKey( "User" )]
		public int UserId { get; set; } // Melyik hallgato vette fel
		public DateTime Timestamp { get; set; } // Mikor vette fel
	}
}