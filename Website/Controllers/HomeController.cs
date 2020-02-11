using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Website.Models;


namespace Website.Controllers
{
	public class HomeController : BaseController
	{
		private readonly UserManager<User> userManager;
		private const int pageSize = 20;


		public HomeController( ISysService sysService, UserManager<User> userManager ) : base( sysService )
		{
			this.userManager = userManager;
		}


		public IActionResult Index( )
		{
			return View( "Index", sysService.Trainings.ToList( ) );
		}

		[ValidateAntiForgeryToken]
		[Authorize( Roles = StaticData.Student )]
		public async Task<IActionResult> CourseAction( string cid, string courseAction )
		{
			User user = await userManager.GetUserAsync( User );
			if( user == null )
				return View( "CourseAction", "error" );

			string[] s = cid.Split( '/' );
			if( s.Length != 2 )
				return View( "CourseAction", "Invalid course ID." );

			if( courseAction == "Take" )
			{
				if( sysService.HasPassedCourse( user.Id, cid ) )
				{
					return View( "CourseAction", "Already passed the course." );
				}

				if( sysService.IsCourseFull( cid ) )
				{
					return View( "CourseAction", "Course is full." );
				}

				if( sysService.HasCourseChoice( user.Id, s[0] ) )
				{
					return View( "CourseAction", "Already took another course from this subject." );
				}

				if( sysService.AddCourseChoice( user.Id, cid ) )
				{
					return View( "CourseAction", "Success!" );
				}
				else
				{
					return View( "CourseAction", "Failed." );
				}
			}
			else if( courseAction == "Drop" )
			{
				if( sysService.RemoveCourseChoice( user.Id, cid ) )
				{
					return View( "CourseAction", "Success!" );
				}
				else
				{
					return View( "CourseAction", "Failed." );
				}
			}

			return View( "CourseAction", "error" );
		}

		public async Task<IActionResult> Details( string trainingId, string srchcmd, string curFilter, string searchString, int? page )
		{
			if( searchString != null ) page = 1;
			else searchString = curFilter;
			
			Training training = sysService.GetTraining( trainingId );

			if( training == null )
				return RedirectToAction( nameof( Index ) );


			User user = await userManager.GetUserAsync( User );
			
			List<CourseVM> courses = new List<CourseVM>( );
			foreach( Subject s in training.Subjects )
			{
				foreach( Course c in s.Courses )
				{
					User teacher = await userManager.FindByIdAsync( c.TeacherId.ToString( ) );

					if( !string.IsNullOrEmpty( srchcmd ) && !string.IsNullOrEmpty( searchString ) )
					{
						if( srchcmd.Equals( "Search Course" ) && !c.CourseId.Contains( searchString ) )
							continue;
						if( srchcmd.Equals( "Search Teacher" ) && !teacher.Name.Contains( searchString ) )
							continue;
					}
					
					CourseVM cvm = new CourseVM( );

					if( user != null )
					{
						cvm.Passed = sysService.HasPassedCourse( user.Id, c.CourseId );
						cvm.Grades = sysService.GetGradesForUser( user.Id, c.CourseId );
						if( !string.IsNullOrEmpty( srchcmd ) )
						{
							if( srchcmd.Equals( "Search Active" ) && (!sysService.IsUserOnCourse( user.Id, c.CourseId ) || cvm.Passed) )
								continue;
							if( srchcmd.Equals( "Search Finished" ) && (!cvm.Passed || cvm.Grades.Count == 0) )
								continue;
						}
					
						if( cvm.Passed )
						{
							cvm.CourseAction = "----";
						}
						else
						{
							if( sysService.IsUserOnCourse( user.Id, c.CourseId ) )
							{
								cvm.CourseAction = "Drop";
							}
							else
							{
								if( sysService.HasCourseChoice( user.Id, s.SubjectId ) )
								{
									cvm.CourseAction = "(Take)";
								}
								else
								{								
									cvm.CourseAction = "Take";
								}
							}
						}
					}
					
					cvm.CourseId = c.CourseId;
					cvm.SubjectId = c.Subject.Name;
					cvm.TeacherName = teacher.Name;
					cvm.Schedule = c.Schedule;
					cvm.CurStudents = sysService.GetStudentCount( c.CourseId );
					cvm.MaxStudents = c.MaxStudents;

					courses.Add( cvm );
				}
			}
		
			int count = courses.Count( );
			int lastPage = (int)Math.Ceiling( count / (double)pageSize );
			int curPage = 1;
			if( count == 0 ) curPage = 0;
			else if( page == null || page < 1 ) { } // (curPage = 1;)
			else if( page > lastPage ) curPage = lastPage;
			else curPage = (int)page;

			// Jelenleg az OrderBy( ) felesleges utasítás, az adatbázis rendezett id szerint.
			courses = courses.OrderBy( c => c.CourseId )
				.Skip( (curPage - 1) * pageSize )
				.Take( pageSize )
				.ToList( );

			DetailsVM dvm = new DetailsVM
			{
				CurFilter = searchString,
				CurCmd = srchcmd,
				CurPage = curPage,
				TrId = trainingId,
				FirstPage = 1,
				LastPage = lastPage,
				Courses = courses
			};
			dvm.PrevPage = dvm.CurPage > dvm.FirstPage ? dvm.CurPage - 1 : dvm.FirstPage;
			dvm.NextPage = dvm.CurPage < dvm.LastPage ? dvm.CurPage + 1 : dvm.LastPage;
			
			return View( "Details", dvm );
		}


		public IActionResult About( )
		{
			return View( );
		}

		public IActionResult Contact( )
		{
			return View( );
		}

		public IActionResult Privacy( )
		{
			return View( );
		}

		[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
		public IActionResult Error( )
		{
			return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
		}
	}
}