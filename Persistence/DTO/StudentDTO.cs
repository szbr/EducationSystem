using System;


namespace Persistence.DTO
{
	public class StudentDTOBase
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public class StudentDTO : StudentDTOBase
	{
		public EGrade[] Grades { get; set; }

		public override bool Equals( object obj )
		{
			return (obj is StudentDTO dto) && UserName == dto.UserName;
		}

		public override int GetHashCode( )
		{
			return UserName.GetHashCode( );
		}
	}
}