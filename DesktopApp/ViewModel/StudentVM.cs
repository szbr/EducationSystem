using System.Collections.Generic;
using System.Linq;
using Persistence;
using Persistence.DTO;


namespace DesktopApp.ViewModel
{
	public class StudentVM : ViewModelBase
	{
		public StudentVM( StudentDTO dto )
		{
			Data = (StudentDTOBase)dto;

			gradeList = new List<EGrade>( );
			gradeList.AddRange( dto.Grades );
		}


		public void AddGrade( EGrade g )
		{
			gradeList.Add( g );

			OnPropertyChanged( "Grades" );
			OnPropertyChanged( "Enabled" );
		}

		public StudentDTOBase Data { get; set; }

		private List<EGrade> gradeList;

		public string Grades
		{
			get
			{
				string ret = "";

				if( gradeList.Count != 0 )
				{
					for( int i = 0; i < gradeList.Count - 1; i++ )
					{
						ret += ((int)gradeList[i]).ToString( ) + ", ";
					}
					ret += ((int)gradeList.Last( )).ToString( );
				}

				return ret;
			}
		}

		public bool Enabled
		{
			get
			{
				return !(gradeList.Count > 0 && gradeList.Max( ) >= EGrade.PASS);
			}
		}
	}
}