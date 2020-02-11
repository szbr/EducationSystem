using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Persistence;


namespace Website.Models
{
	public class SysService : ISysService
	{
		private readonly SysDbContext context;


		public SysService( SysDbContext context )
		{
			this.context = context;
		}


		public IEnumerable<Training> Trainings => context.Trainings;
		public IEnumerable<Subject> Subjects => context.Subjects;
		public IEnumerable<Course> Courses => context.Courses;

		public Training GetTraining( string trainingId )
		{
			if( string.IsNullOrEmpty( trainingId ) )
				return null;

			return context.Trainings
				.Include( t => t.Subjects )
				.ThenInclude( s => s.Courses )
				.FirstOrDefault( t => t.TrainingId == trainingId );
		}

		public List<EGrade> GetGradesForUser( int userId, string courseId )
		{
			return context.Grades
				.Where( g => g.UserId == userId && g.CourseId == courseId )
				.Select( g => g.GradeRecord )
				.ToList( );
		}

		public int GetStudentCount( string courseId )
		{
			return context.CourseChoices
				.Where( cc => cc.CourseId == courseId )
				.Count( );
		}

		public bool AddCourseChoice( int userId, string courseId )
		{
			CourseChoice old = context.CourseChoices
				.Where( cc => cc.UserId == userId && cc.CourseId == courseId )
				.SingleOrDefault( );

			if( old != null )
				return false; // Már felvette

			CourseChoice newCC = new CourseChoice
			{
				CourseId = courseId,
				UserId = userId
			};
			context.CourseChoices.Add( newCC );
			context.SaveChanges( );

			return true;
		}

		public bool RemoveCourseChoice( int userId, string courseId )
		{
			CourseChoice ccToRemove = context.CourseChoices
				.Where( cc => cc.UserId == userId && cc.CourseId == courseId )
				.SingleOrDefault( );

			if( ccToRemove == null )
				return false; // Nincs mit leadni

			// Ha teljesítette, akkor nem lehet már lejelentkezni
			if( HasPassedCourse( userId, courseId ) )
				return false;

			context.CourseChoices.Remove( ccToRemove );
			context.SaveChanges( );
			return true;
		}

		public bool IsUserOnCourse( int userId, string courseId )
		{
			return context.CourseChoices
				.Where( cc => cc.UserId == userId && cc.CourseId == courseId )
				.SingleOrDefault( ) != null;
		}

		public bool HasCourseChoice( int userId, string subjectId )
		{
			List<string> courseChoiceIds = context.CourseChoices
				.Where( cc => cc.UserId == userId )
				.Select( cc => cc.CourseId )
				.ToList( );

			foreach( string ccId in courseChoiceIds )
			{
				string[] s = ccId.Split( '/' );
				if( s[0] == subjectId )
					return true;
			}
			
			return false;
		}

		public bool HasPassedCourse( int userId, string courseId )
		{
			Course course = context.Courses
				.Where( c => c.CourseId == courseId )
				.SingleOrDefault( );

			if( course == null )
				return false;

			List<Course> courses = context.Courses
				.Where( c => c.SubjectId == course.SubjectId )
				.ToList( );

			foreach( Course c in courses )
			{
				List<EGrade> grades = GetGradesForUser( userId, c.CourseId );
				if( grades.Count > 0 && grades.Max( ) >= EGrade.PASS )
					return true;
			}

			return false;

			//List<EGrade> grades = GetGradesForUser( userId, courseId );
			//return grades.Count > 0 && grades.Max( ) >= EGrade.PASS;
		}

		public bool IsCourseFull( string courseId )
		{
			Course course = context.Courses
				.Where( c => c.CourseId == courseId )
				.SingleOrDefault( );

			if( course == null )
				return false;

			return GetStudentCount( courseId ) >= course.MaxStudents;
		}
	}
}