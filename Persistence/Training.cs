using System.Collections.Generic;


namespace Persistence
{
	public class Training
	{
		public Training( )
		{
			Subjects = new HashSet<Subject>( );
		}

		public string TrainingId { get; set; } // Képzés azonosítója, pl.: "IK", "BG"

		public string Name { get; set; } // Képzés neve, pl.: "Programtervező informatikus", "Gyógypedagógus"

		public ICollection<Subject> Subjects { get; set; } // Képzéshez tartozó tárgyak
	}
}