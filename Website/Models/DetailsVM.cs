using System.Collections.Generic;


namespace Website.Models
{
	public class DetailsVM
	{
		public string CurFilter { get; set; }
		public string CurCmd { get; set; }
		public string TrId { get; set; }
		public int CurPage { get; set; }	
		public int FirstPage { get; set; }
		public int LastPage { get; set; }
		public int PrevPage { get; set; }
		public int NextPage { get; set; }
		public List<CourseVM> Courses { get; set; }
	}
}