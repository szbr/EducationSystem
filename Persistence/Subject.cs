using System.Collections.Generic;


namespace Persistence
{
	public class Subject
	{
		public Subject( )
		{
			Courses = new HashSet<Course>( );
		}

		public string SubjectId { get; set; } // "IK-WAF_E+GY" <== TrainingId + "-" + ...

		public string Name { get; set; } // Tárgy neve, pl.: "Webes alk. fejl."

		public ICollection<Course> Courses { get; set; } // Tárgyhoz tartozó kurzusok

		public string TrainingId { get; set; } // Melyik képzéshez tartozik
		public Training Training { get; set; }
	}
}