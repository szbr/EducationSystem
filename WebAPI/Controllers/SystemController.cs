using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DTO;


namespace WebAPI.Controllers
{
	[Route( "api/[controller]" )]
	[Authorize( Roles = StaticData.Teacher )]
    public class SystemController : ControllerBase
    {
        private readonly SysDbContext context;
		private readonly UserManager<User> userManager;

		
		public SystemController( SysDbContext context, UserManager<User> userManager )
		{
			this.context = context;
			this.userManager = userManager;
		}


		[HttpGet( "[action]" )]
		public ActionResult<IEnumerable<string>> GetTrainingIds( )
		{
			return context.Trainings
				.Select( t => t.TrainingId )
				.ToList( );
		}

		[HttpGet( "[action]/{trainingId}" )]
		public ActionResult<IEnumerable<string>> GetSubjects( string trainingId )
		{
			return context.Subjects
				.Where( s => s.TrainingId == trainingId )
				.Select( s => s.SubjectId )
				.ToList( );
		}

		[HttpGet( "[action]" )]
		public async Task<ActionResult<IEnumerable<CourseDTO>>> GetTeacherCourses( )
		{
			User teacher = await userManager.GetUserAsync( User );
			if( teacher == null )
				return BadRequest( );

			List<Course> courses = context.Courses
				.Where( c => c.TeacherId == teacher.Id )
				.Include( c => c.Subject )
				.ToList( );

			List<CourseDTO> ret = new List<CourseDTO>( courses.Count );
			foreach( Course c in courses )
			{
				ret.Add
				(
					new CourseDTO
					{
						CourseId = c.CourseId,
						SubjectName = c.Subject.Name,
						Schedule = c.Schedule
					}
				);
			}

			return ret;
		}

		[HttpGet( "[action]/{courseId}" )]
		public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsOnCourse( string courseId )
		{
			List<CourseChoice> courseChoices = context.CourseChoices
				.Where( cc => cc.CourseId == courseId )
				.ToList( );

			List<StudentDTO> ret = new List<StudentDTO>( );
			foreach( CourseChoice cc in courseChoices )
			{
				User student = await userManager.FindByIdAsync( cc.UserId.ToString( ) );

				List<EGrade> grades = context.Grades
					.Where( g => g.CourseId == courseId && g.UserId == student.Id )
					.Select( g => g.GradeRecord )
					.ToList( );

				ret.Add
				(
					new StudentDTO
					{
						UserName = student.UserName,
						Name = student.Name,
						Timestamp = cc.Timestamp,
						Grades = grades.ToArray( )
					}
				);
			}

			return ret;
		}

		[HttpPost( "[action]" )]
		public async Task<IActionResult> AddCourse( [FromBody] NewCourseDTO nc )
		{
			User teacher = await userManager.GetUserAsync( User );
			if( teacher == null )
				return BadRequest( );
			
			Subject subject = context.Subjects
				.Where( s => s.SubjectId == nc.SelectedSubject )
				.SingleOrDefault( );

			// Nem létező tárgyra akar hirdetni.
			if( subject == null )
				return BadRequest( );

			List<string> courseIds = context.Courses
				.Where( c => c.SubjectId == nc.SelectedSubject )
				.Select( c => c.CourseId )
				.ToList( );

			int max = 0;
			foreach( string id in courseIds )
			{
				string[] s = id.Split( '/' );
				int n = Convert.ToInt32( s[1] );
				if( n > max )
					max = n;
			}

			string newCourseId = nc.SelectedSubject + "/" + (max + 1).ToString( );


			string startHour = StaticData.GetData( StaticData.Hours, nc.SelectedHourIdx );
			string startMinute = StaticData.GetData( StaticData.Minutes, nc.SelectedMinuteIdx );
			string duration = StaticData.GetData( StaticData.Durations, nc.SelectedDurationIdx );

			if( startHour == null || startMinute == null || duration == null )
				return BadRequest( );

			DateTime endTime = new DateTime( 1, 1, 1, Convert.ToInt32(startHour), Convert.ToInt32(startMinute), 0 );
			endTime = endTime.AddHours( double.Parse( duration, CultureInfo.InvariantCulture ) );

			string startDay = StaticData.GetData( StaticData.Weekdays, nc.SelectedWeekdayIdx );
			if( startDay == null )
				return BadRequest( );

			string schedule = startDay + " " + startHour + ":" + startMinute + "-"
				+ endTime.Hour.ToString( "00" ) + ":" + endTime.Minute.ToString( "00" );

			
			if( nc.MaxStudents < 1 || nc.MaxStudents > 999 )
				return BadRequest( );

			Course newCourse = new Course
			{
				CourseId = newCourseId,
				TeacherId = teacher.Id,
				Schedule = schedule,
				MaxStudents = nc.MaxStudents,
				SubjectId = nc.SelectedSubject
			};

			context.Courses.Add( newCourse );
			context.SaveChanges( );

			return Ok( );
		}

		[HttpPost( "[action]" )]
		public async Task<IActionResult> AddGrade( [FromBody] AddGradeDTO ag )
		{
			User student = await userManager.FindByNameAsync( ag.UserName );
			if( student == null )
				return BadRequest( );

			User teacher = await userManager.GetUserAsync( User );
			if( teacher == null )
				return BadRequest( );

			Course course = context.Courses
				.Where( c => c.CourseId == ag.CourseId )
				.SingleOrDefault( );

			// Nem létező kurzus vagy más kurzusára akar beírni.
			if( course == null || (course.TeacherId != teacher.Id) )
				return BadRequest( );

			CourseChoice courseChoice = context.CourseChoices
				.Where( cc => cc.CourseId == ag.CourseId && cc.UserId == student.Id )
				.SingleOrDefault( );

			// A hallgató nincs a kurzuson fent.
			if( courseChoice == null )
				return BadRequest( );

			// Ha már van kettes vagy jobb jegye, 
			// akkor nem lehet jegyet beirni.
			List<EGrade> grades = context.Grades
					.Where( g => g.UserId == student.Id && g.CourseId == ag.CourseId )
					.Select( g => g.GradeRecord )
					.ToList( );
			
			if( grades.Count > 0 && grades.Max( ) >= EGrade.PASS )
			{
				return BadRequest( );
			}

			Grade newGrade = new Grade
			{
				CourseId = ag.CourseId,
				UserId = student.Id,
				TeacherId = teacher.Id,
				Timestamp = DateTime.Now,
				GradeRecord = ag.Grade			
			};

			context.Grades.Add( newGrade );
			context.SaveChanges( );

			return Ok( );
		}
    }
}