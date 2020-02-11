

namespace Persistence.DTO
{
	public class CourseDTO
	{
		public string CourseId { get; set; }
		public string SubjectName { get; set; }
		public string Schedule { get; set; }

		public override bool Equals( object obj )
		{
			return (obj is CourseDTO dto) && CourseId == dto.CourseId;
		}

		public override int GetHashCode( )
		{
			return CourseId.GetHashCode( );
		}
	}
}