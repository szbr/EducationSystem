

namespace Persistence.DTO
{
	public class AddGradeDTO
	{
		public string UserName { get; set; }
		public string CourseId { get; set; }
		public EGrade Grade { get; set; }
	}
}