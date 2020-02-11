using System.Collections.Generic;
using Persistence;


namespace Website.Models
{
	public interface ISysService
	{
		IEnumerable<Training> Trainings { get; } // Képzések lekérdezése
		IEnumerable<Subject> Subjects { get; } // Tárgyak lekérdezése
		IEnumerable<Course> Courses { get; } // Kurzusok lekérdezése

		Training GetTraining( string trainingId ); // Adott azonosítóval rendelkező képzés megkeresése
		List<EGrade> GetGradesForUser( int userId, string courseId ); // Egy hallgató jegyeinek lekérdezése egy kurzusra vonatkozóan
		int GetStudentCount( string courseId ); // Adott kurzuson hány hallgató van jelenleg
		bool AddCourseChoice( int userId, string courseId ); // Kurzus felvétele
		bool RemoveCourseChoice( int userId, string courseId ); // Kurzus leadása
		bool IsUserOnCourse( int userId, string courseId ); // Adott hallgató a kurzuson van-e
		bool HasCourseChoice( int userId, string subjectId ); // Vett-e fel a hallgató a tárgy kurzusai közül
		bool HasPassedCourse( int userId, string courseId ); // Teljesítette-e egy adott hallgató a kurzust
		bool IsCourseFull( string courseId ); // Tele van-e a kurzus
	}
}