using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopApp.Model;
using Persistence;
using Persistence.DTO;


namespace DesktopApp.ViewModel
{
	public class NewCourseVM : ViewModelBase
	{
		private readonly ITRService model;


		public NewCourseVM( ITRService model )
		{
			this.model = model;

			Task.Run( LoadTrainingDataAsync ).Wait( );

			SubmitNewCourseCmd = new DelegateCommand( p => SubmitNewCourse( ) );

			weekdays  = new ObservableCollection<string>( StaticData.Weekdays );
			hours	  = new ObservableCollection<string>( StaticData.Hours );
			minutes	  = new ObservableCollection<string>( StaticData.Minutes );
			durations = new ObservableCollection<string>( StaticData.Durations );
	
			MaxStudents = "32";

			SelectedWeekdayIdx = 0;
			SelectedHourIdx = 0;
			SelectedMinuteIdx = 0;
			SelectedDurationIdx = 1;
		}

		private async Task LoadTrainingDataAsync( )
		{
			try
			{
				Trainings = new ObservableCollection<string>( await model.GetTrainingIds( ) );
				if( Trainings.Count == 0 )
					throw new Exception( "Trainings.Count == 0" );

				SelectedTraining = Trainings[0];

				// Nem kell mivel TrainingSelectionChangedAsync( ) kezdetben is hivodik.
				//Subjects = new ObservableCollection<string>( await model.GetSubjects(SelectedTraining) );
				//SelectedSubject = Subjects.Count > 0 ? Subjects[0] : null;
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
			}
		}

		public async void TrainingSelectionChangedAsync( string newTrainingId )
		{
			Subjects = new ObservableCollection<string>( await model.GetSubjects(newTrainingId) );
			SelectedSubject = Subjects.Count > 0 ? Subjects[0] : null;
		}


		private void SubmitNewCourse( )
		{
			int maxStudents = 0;
			try
			{
				maxStudents = Convert.ToInt32( MaxStudents );
			}
			catch( Exception ex )
			{
				OnMessage( ex.Message );
				return;
			}

			SubmitNewCourseEventArgs e = new SubmitNewCourseEventArgs
			{
				SelectedTraining = SelectedTraining,
				NewCourse = new NewCourseDTO
				{
					SelectedSubject = SelectedSubject,
					MaxStudents = maxStudents,
					SelectedWeekdayIdx = SelectedWeekdayIdx,
					SelectedHourIdx = SelectedHourIdx,
					SelectedMinuteIdx = SelectedMinuteIdx,
					SelectedDurationIdx = SelectedDurationIdx
				}
			};

			SubmitNewCourseEvent?.Invoke( this, e );
		}


		#region Bindings
		private ObservableCollection<string> trainings;
		public ObservableCollection<string> Trainings
		{
			get => trainings;
			set	{ trainings = value; OnPropertyChanged( ); }
		}
		public string SelectedTraining { get; set; }


		private ObservableCollection<string> subjects;
		public ObservableCollection<string> Subjects
		{
			get => subjects;
			set	{ subjects = value; OnPropertyChanged( ); }
		}
		private string selectedSubject;
		public string SelectedSubject
		{
			get => selectedSubject;
			set { selectedSubject = value; OnPropertyChanged( ); }
		}


		public string MaxStudents { get; set; }


		private readonly ObservableCollection<string> weekdays;
		public ObservableCollection<string> Weekdays
		{
			get => weekdays;
		}
		public int SelectedWeekdayIdx { get; set; }


		private readonly ObservableCollection<string> hours;
		public ObservableCollection<string> Hours
		{
			get => hours;
		}
		public int SelectedHourIdx { get; set; }


		private readonly ObservableCollection<string> minutes;
		public ObservableCollection<string> Minutes
		{
			get => minutes;
		}
		public int SelectedMinuteIdx { get; set; }

		
		private readonly ObservableCollection<string> durations;
		public ObservableCollection<string> Durations
		{
			get => durations;
		}
		public int SelectedDurationIdx { get; set; }
		#endregion


		#region Delegates
		public DelegateCommand SubmitNewCourseCmd { get; }
		#endregion


		#region Events
		public event EventHandler<SubmitNewCourseEventArgs> SubmitNewCourseEvent;
		#endregion
	}
}