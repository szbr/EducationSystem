

namespace Persistence
{
	public static class StaticData
	{
		static StaticData( )
		{
			Roles	  = new string[]{ Admin, Teacher, Student };

			Weekdays  = new string[]{ "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
			Hours	  = new string[]{ "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
			Minutes	  = new string[]{ "00", "30" };
			Durations = new string[]{ "0.5", "1.0", "1.5", "2.0", "2.5", "3.0" };
		}
		

		public static string GetData( string[] data, int idx )
		{
			if( idx >= 0 && data != null && idx < data.Length )
			{
				return data[idx];
			}

			return null;
		}

		
		public static string[] Roles { get; private set; }
		public const string Admin   = "Admin";
		public const string Teacher = "Teacher";
		public const string Student = "Student";

		public static string[] Weekdays { get; private set; }
		public static string[] Hours { get; private set; }
		public static string[] Minutes { get; private set; }
		public static string[] Durations { get; private set; }
	}
}