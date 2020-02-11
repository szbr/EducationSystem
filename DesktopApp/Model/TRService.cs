using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Persistence;
using Persistence.DTO;


namespace DesktopApp.Model
{
	public class TRService : ITRService
	{
		private readonly HttpClient httpClient;


		public TRService( string baseAddr )
		{
			httpClient = new HttpClient
			{
				BaseAddress = new Uri( baseAddr ),
				//Timeout = TimeSpan.FromSeconds( 10 )
			};
		}

		
		public string UserName { get; private set; }


		#region GET
		public async Task<IEnumerable<string>> GetTrainingIds( )
		{
			HttpResponseMessage response = await httpClient.GetAsync( "api/System/GetTrainingIds" );

			if( response.IsSuccessStatusCode )
			{
				return await response.Content.ReadAsAsync<IEnumerable<string>>( );
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task<IEnumerable<string>> GetSubjects( string trainingId )
		{
			HttpResponseMessage response = await httpClient.GetAsync( "api/System/GetSubjects/" + trainingId );

			if( response.IsSuccessStatusCode )
			{
				return await response.Content.ReadAsAsync<IEnumerable<string>>( );
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task<IEnumerable<CourseDTO>> GetTeacherCourses( )
		{
			HttpResponseMessage response = await httpClient.GetAsync( "api/System/GetTeacherCourses" );

			if( response.IsSuccessStatusCode )
			{
				return await response.Content.ReadAsAsync<IEnumerable<CourseDTO>>( );
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task<IEnumerable<StudentDTO>> GetStudentsOnCourse( string courseId )
		{
			HttpResponseMessage response = await httpClient.GetAsync( "api/System/GetStudentsOnCourse/" + courseId );

			if( response.IsSuccessStatusCode )
			{
				return await response.Content.ReadAsAsync<IEnumerable<StudentDTO>>( );
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}
		#endregion


		#region POST
		public async Task AddNewCourse( NewCourseDTO newCourseDTO )
		{
			HttpResponseMessage response = await httpClient.PostAsJsonAsync( "api/System/AddCourse", newCourseDTO );

			if( response.IsSuccessStatusCode )
			{
				return;
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task AddGrade( string userName, string courseId, EGrade grade )
		{
			AddGradeDTO agDTO = new AddGradeDTO
			{
				UserName = userName,
				CourseId = courseId,
				Grade = grade
			};

			HttpResponseMessage response = await httpClient.PostAsJsonAsync( "api/System/AddGrade", agDTO );

			if( response.IsSuccessStatusCode )
			{
				return;
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task<bool> Login( string userName, string password )
		{
			string errors = "";
			if( string.IsNullOrEmpty(userName) ) errors += "UserName is required.\n";
			if( string.IsNullOrEmpty(password) ) errors += "Password is required.\n";

			if( errors != "" )
			{
				throw new Exception( $"Error:\n\n{errors}" );
			}

			LoginDTO loginDTO = new LoginDTO
			{
				UserName = userName,
				UserPassword = password
			};

			HttpResponseMessage response = await httpClient.PostAsJsonAsync( "api/Account/Login", loginDTO );

			if( response.IsSuccessStatusCode )
			{
				UserName = userName;
				return true;
			}

			if( response.StatusCode == HttpStatusCode.Unauthorized )
			{
				return false;
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}

		public async Task Logout( )
		{
			HttpResponseMessage response = await httpClient.PostAsJsonAsync( "api/Account/Logout", "" );

			if( response.IsSuccessStatusCode )
			{
				UserName = null;
				return;
			}

			throw new Exception( $"Response: {response.StatusCode}" );
		}
		#endregion
	}
}