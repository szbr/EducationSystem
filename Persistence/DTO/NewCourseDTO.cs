

namespace Persistence.DTO
{
	public class NewCourseDTO
	{
		public string SelectedSubject { get; set; }
		public int MaxStudents { get; set; }
		public int SelectedWeekdayIdx { get; set; }
		public int SelectedHourIdx { get; set; }
		public int SelectedMinuteIdx { get; set; }
		public int SelectedDurationIdx { get; set; }
	}
}