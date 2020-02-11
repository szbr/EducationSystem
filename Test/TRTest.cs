using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebAPI.Controllers;
using Xunit;


namespace Test
{
	public class TRTest : IDisposable
	{
		private readonly SysDbContext context;
		private readonly List<string> trainingDTO;
		private readonly List<string>[] subjectDTOs;
		private readonly List<CourseDTO>[] courseDTOs;
		private readonly List<StudentDTO>[] studentDTOs;
		private readonly int[] teacherCourseCount;
		private readonly NewCourseDTO[] ncDTOs;
		private readonly AddGradeDTO[] agDTOs;

	
		public TRTest( )
		{
			var options = new DbContextOptionsBuilder<SysDbContext>( )
				.UseInMemoryDatabase( "TestDB" )
				.Options;

			context = new SysDbContext( options );
			context.Database.EnsureCreated( );

			TDbInitializer.Initialize( context );

			trainingDTO = TDbInitializer.trainings.Select( t => t.TrainingId ).ToList( );
			subjectDTOs = new List<string>[]
			{
				TDbInitializer.subjects.Where( s => s.TrainingId == "IK" ).Select( s => s.SubjectId ).ToList( ),
				TDbInitializer.subjects.Where( s => s.TrainingId == "BG" ).Select( s => s.SubjectId ).ToList( )
			};

			int[] teacherIdList = new int[]{ 7, 8, 9, 10 };
			courseDTOs = new List<CourseDTO>[teacherIdList.Length];
			for( int i = 0; i < teacherIdList.Length; i++ )
			{
				courseDTOs[i] =
				TDbInitializer.courses.Where( c => c.TeacherId == teacherIdList[i] )
				.Select( c => new CourseDTO
				{
					CourseId = c.CourseId,
					SubjectName = c.Subject.Name,
					Schedule = c.Schedule
				}).ToList( );
			}

			string[] courseList = new string[]
			{
				"IK-CPP_E+GY/2",
				"IK-NM1_E+GY/3",
				"BG-PSZ_E+GY/2",
				"BG-KV_E+GY/4"
			};

			studentDTOs = new List<StudentDTO>[courseList.Length];
			for( int i = 0; i < studentDTOs.Length; i++ )
			{
				studentDTOs[i] = new List<StudentDTO>( );
				
				List<CourseChoice> courseChoices = context.CourseChoices
					.Where( cc => cc.CourseId == courseList[i] )
					.ToList( );

				foreach( CourseChoice cc in courseChoices )
				{
					User student = TDbInitializer.users
						.Where( u => u.Id == cc.UserId )
						.SingleOrDefault( );

					List<EGrade> grades = context.Grades
						.Where( g => g.CourseId == courseList[i] && g.UserId == student.Id )
						.Select( g => g.GradeRecord )
						.ToList( );

					studentDTOs[i].Add
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
			}

			teacherCourseCount = new int[4]
			{
				TDbInitializer.courses.Where( c => c.TeacherId == 7 ).Count( ),
				TDbInitializer.courses.Where( c => c.TeacherId == 8 ).Count( ),
				TDbInitializer.courses.Where( c => c.TeacherId == 9 ).Count( ),
				TDbInitializer.courses.Where( c => c.TeacherId == 10 ).Count( )
			};

			ncDTOs = new NewCourseDTO[]
			{
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 20, SelectedWeekdayIdx = 2, SelectedHourIdx = 4, SelectedMinuteIdx = 0, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FPxkeung3267hGY", MaxStudents = 20, SelectedWeekdayIdx = 2, SelectedHourIdx = 4, SelectedMinuteIdx = 0, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 345633, SelectedWeekdayIdx = 2, SelectedHourIdx = 4, SelectedMinuteIdx = 0, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 20, SelectedWeekdayIdx = 2523, SelectedHourIdx = 4, SelectedMinuteIdx = 0, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 20, SelectedWeekdayIdx = 2, SelectedHourIdx = 4235, SelectedMinuteIdx = 0, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 20, SelectedWeekdayIdx = 2, SelectedHourIdx = 4, SelectedMinuteIdx = 40523, SelectedDurationIdx = 2 },
				new NewCourseDTO { SelectedSubject = "IK-FP_E+GY", MaxStudents = 20, SelectedWeekdayIdx = 2, SelectedHourIdx = 4, SelectedMinuteIdx = 0, SelectedDurationIdx = 2234 }
			};

			agDTOs = new AddGradeDTO[]
			{
				new AddGradeDTO	{ UserName = "ABCXYZ", CourseId = "IK-FP_E+GY/2",  Grade = EGrade.VERYGOOD },
				new AddGradeDTO	{ UserName = "ABCXYZ", CourseId = "IK-NM1_E+GY/3", Grade = EGrade.VERYGOOD },
				new AddGradeDTO	{ UserName = "ABCXYZ", CourseId = "IK-OAF_E+GY/1", Grade = EGrade.MEDIOCRE },
				new AddGradeDTO	{ UserName = "ABCXYZ", CourseId = "IK-CPP_E+GY/2", Grade = EGrade.GOOD }
			};
		}

		public void Dispose( )
		{
			context.Database.EnsureDeleted( );
			context.Dispose( );
		}


		[Fact]
		public void GetTrainingIdsTest( )
		{
			var controller = new SystemController( context, null );
			var result = controller.GetTrainingIds( );

			var model = Assert.IsAssignableFrom<IEnumerable<string>>( result.Value );
			Assert.Equal( trainingDTO, model );
		}

		[Theory]
		[InlineData( "IK", 0 )]
		[InlineData( "BG", 1 )]
		public void GetSubjectsTest( string trainingId, int idx )
		{
			var controller = new SystemController( context, null );
			var result = controller.GetSubjects( trainingId );

			var model = Assert.IsAssignableFrom<IEnumerable<string>>( result.Value );
			Assert.Equal( subjectDTOs[idx], model );
		}

		[Theory]
		[InlineData( "IK-CPP_EpGYs2", 0 )]
		[InlineData( "IK-NM1_EpGYs3", 1 )]
		[InlineData( "BG-PSZ_EpGYs2", 2 )]
		[InlineData( "BG-KV_EpGYs4",  3 )]
		public void GetStudentsOnCourse( string courseId, int idx )
		{
			var mUserStore = new Mock<IUserStore<User>>( );
			var mUserManager = new Mock<UserManager<User>>( mUserStore.Object, null, null, null, null, null, null, null, null );

			mUserManager.Setup( um => um.FindByIdAsync( It.IsAny<string>( ) ) )
				.ReturnsAsync( ( string userId ) => TDbInitializer.users[int.Parse( userId ) - 1] );

			var controller = new SystemController( context, mUserManager.Object );
			var result = controller.GetStudentsOnCourse( courseId ).Result;

			List<StudentDTO> model = Assert.IsAssignableFrom<List<StudentDTO>>( result.Value );
			Assert.Equal( studentDTOs[idx].Count, model.Count );
			Assert.Equal( studentDTOs[idx], model );
		}

		[Theory]
		[InlineData( 6 )]
		[InlineData( 7 )]
		[InlineData( 8 )]
		[InlineData( 9 )]
		public async void GetTeacherCoursesTest( int idx )
		{
			var mUserStore = new Mock<IUserStore<User>>( );
			var mUserManager = new Mock<UserManager<User>>( mUserStore.Object, null, null, null, null, null, null, null, null );

			mUserManager.Setup( um => um.GetUserAsync( It.IsAny<ClaimsPrincipal>( ) ) )
				.ReturnsAsync( TDbInitializer.users[idx] );

			var controller = new SystemController( context, mUserManager.Object );
			var result = await controller.GetTeacherCourses( );

			List<CourseDTO> model = Assert.IsAssignableFrom<List<CourseDTO>>( result.Value );
			Assert.Equal( teacherCourseCount[idx - 6], model.Count( ) );
			Assert.Equal( courseDTOs[idx - 6].Count, model.Count );
			Assert.Equal( courseDTOs[idx - 6], model );
		}

		[Theory]
		[InlineData( 0, 1, true )] // Megfelelő kurzus
		[InlineData( 1, 0, false )] // SelectedSubject nem létezik
		[InlineData( 2, 0, false )] // MaxStudents túl sok
		[InlineData( 3, 0, false )] // SelectedWeekdayIdx index nem jó
		[InlineData( 4, 0, false )] // SelectedHourIdx index nem jó
		[InlineData( 5, 0, false )] // SelectedMinuteIdx index nem jó
		[InlineData( 6, 0, false )] // SelectedDurationIdx index nem jó
		public async void AddCourseTest( int idx, int delta, bool okResult )
		{
			var mUserStore = new Mock<IUserStore<User>>( );
			var mUserManager = new Mock<UserManager<User>>( mUserStore.Object, null, null, null, null, null, null, null, null );

			mUserManager.Setup( um => um.GetUserAsync( It.IsAny<ClaimsPrincipal>( ) ) )
				.ReturnsAsync( TDbInitializer.users[5] );

			int c = context.Courses.Count( );

			var controller = new SystemController( context, mUserManager.Object );
			var result = await controller.AddCourse( ncDTOs[idx] );

			if( okResult ) Assert.IsType<OkResult>( result );
			else Assert.IsNotType<OkResult>( result );
			Assert.Equal( c + delta, context.Courses.Count( ) );
		}

		[Theory]
		[InlineData( 0, 1, true )] // Még nincs jegye
		[InlineData( 1, 0, false )] // Már van jó jegye
		[InlineData( 2, 1, true )] // Még nincs jó jegye
		[InlineData( 3, 0, false )] // Már van jó jegye, de volt rossz is
		public async void AddGradeTest( int idx, int delta, bool okResult )
		{
			var mUserStore = new Mock<IUserStore<User>>( );
			var mUserManager = new Mock<UserManager<User>>( mUserStore.Object, null, null, null, null, null, null, null, null );

			mUserManager.Setup( um => um.GetUserAsync( It.IsAny<ClaimsPrincipal>( ) ) )
				.ReturnsAsync( TDbInitializer.users[5] );
			mUserManager.Setup( um => um.FindByNameAsync( It.IsAny<string>( ) ) )
				.ReturnsAsync( TDbInitializer.users[0] );

			int c = context.Grades.Count( );

			var controller = new SystemController( context, mUserManager.Object );
			var result = await controller.AddGrade( agDTOs[idx] );

			if( okResult ) Assert.IsType<OkResult>( result );
			else Assert.IsNotType<OkResult>( result );
			Assert.Equal( c + delta, context.Grades.Count( ) );
		}
	}
}