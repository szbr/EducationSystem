using Persistence;


namespace Test
{
	public static class TDbInitializer
	{
		public static void Initialize( SysDbContext context )
		{
			context.Trainings.AddRange( trainings );
			context.Subjects.AddRange( subjects );
			context.Courses.AddRange( courses );
			context.CourseChoices.AddRange( courseChoices );
			context.Grades.AddRange( grades );

			context.SaveChanges( );
		}


		public static Training[] trainings = new Training[]
		{
			new Training { TrainingId = "IK", Name = "Programtervező informatikus" },
			new Training { TrainingId = "BG", Name = "Gyógypedagógus" }
		};

		public static Subject[] subjects = new Subject[]
		{
			new Subject { SubjectId = "IK-WAF_E+GY",  Name = "Webes alk. fejl.", TrainingId = "IK" },
			new Subject { SubjectId = "IK-FP_E+GY",   Name = "Fordprog",         TrainingId = "IK" },
			new Subject { SubjectId = "IK-NM1_E+GY",  Name = "Nummod 1",         TrainingId = "IK" },
			new Subject { SubjectId = "IK-EVA2_E+GY", Name = "EVA 2",            TrainingId = "IK" },
			new Subject { SubjectId = "IK-CPP_E+GY",  Name = "C++",              TrainingId = "IK" },
			new Subject { SubjectId = "IK-OAF_E+GY",  Name = "OAF",              TrainingId = "IK" },

			new Subject { SubjectId = "BG-PSZ_E+GY",  Name = "Pszich.",          TrainingId = "BG" },
			new Subject { SubjectId = "BG-KV_E+GY",   Name = "Komplex Vizsga",   TrainingId = "BG" },
			new Subject { SubjectId = "BG-GY_E+GY",   Name = "Gyakorlat",        TrainingId = "BG" }
		};

		public static Course[] courses = new Course[]
		{
			new Course { CourseId = "IK-WAF_E+GY/1",  TeacherId = 6, Schedule = "Thursday 10:00-12:00", MaxStudents = 16, SubjectId = "IK-WAF_E+GY" },
			new Course { CourseId = "IK-WAF_E+GY/2",  TeacherId = 6, Schedule = "Thursday 12:00-14:00", MaxStudents = 24, SubjectId = "IK-WAF_E+GY" },
			new Course { CourseId = "IK-WAF_E+GY/3",  TeacherId = 6, Schedule = "Thursday 14:00-16:00", MaxStudents = 1,  SubjectId = "IK-WAF_E+GY" },
			new Course { CourseId = "IK-WAF_E+GY/4",  TeacherId = 6, Schedule = "Thursday 16:00-18:00", MaxStudents = 2,  SubjectId = "IK-WAF_E+GY" },

			new Course { CourseId = "IK-FP_E+GY/1",   TeacherId = 6, Schedule = "Wednesday 10:30-12:30", MaxStudents = 12, SubjectId = "IK-FP_E+GY" },
			new Course { CourseId = "IK-FP_E+GY/2",   TeacherId = 6, Schedule = "Wednesday 12:30-14:30", MaxStudents = 5,  SubjectId = "IK-FP_E+GY" },
			new Course { CourseId = "IK-FP_E+GY/3",   TeacherId = 7, Schedule = "Wednesday 10:30-12:30", MaxStudents = 1,  SubjectId = "IK-FP_E+GY" },
			new Course { CourseId = "IK-FP_E+GY/4",   TeacherId = 7, Schedule = "Wednesday 12:30-14:30", MaxStudents = 2,  SubjectId = "IK-FP_E+GY" },

			new Course { CourseId = "IK-NM1_E+GY/1",  TeacherId = 6, Schedule = "Monday 10:00-12:30", MaxStudents = 24, SubjectId = "IK-NM1_E+GY" },
			new Course { CourseId = "IK-NM1_E+GY/2",  TeacherId = 7, Schedule = "Monday 10:00-12:30", MaxStudents = 25, SubjectId = "IK-NM1_E+GY" },
			new Course { CourseId = "IK-NM1_E+GY/3",  TeacherId = 6, Schedule = "Monday 12:30-15:00", MaxStudents = 32, SubjectId = "IK-NM1_E+GY" },
			new Course { CourseId = "IK-NM1_E+GY/4",  TeacherId = 7, Schedule = "Monday 12:30-15:00", MaxStudents = 24, SubjectId = "IK-NM1_E+GY" },
			new Course { CourseId = "IK-NM1_E+GY/5",  TeacherId = 8, Schedule = "Monday 10:00-12:30", MaxStudents = 12, SubjectId = "IK-NM1_E+GY" },

			new Course { CourseId = "IK-EVA2_E+GY/1", TeacherId = 6, Schedule = "Tuesday 10:00-12:00", MaxStudents = 90, SubjectId = "IK-EVA2_E+GY" },
			new Course { CourseId = "IK-EVA2_E+GY/2", TeacherId = 6, Schedule = "Tuesday 12:00-14:00", MaxStudents = 12, SubjectId = "IK-EVA2_E+GY" },
			new Course { CourseId = "IK-EVA2_E+GY/3", TeacherId = 6, Schedule = "Tuesday 14:00-16:00", MaxStudents = 25, SubjectId = "IK-EVA2_E+GY" },
			new Course { CourseId = "IK-EVA2_E+GY/4", TeacherId = 6, Schedule = "Tuesday 16:00-18:00", MaxStudents = 12, SubjectId = "IK-EVA2_E+GY" },

			new Course { CourseId = "IK-CPP_E+GY/1",  TeacherId = 6, Schedule = "Friday 10:00-12:00", MaxStudents = 12, SubjectId = "IK-CPP_E+GY" },
			new Course { CourseId = "IK-CPP_E+GY/2",  TeacherId = 6, Schedule = "Friday 12:00-14:00", MaxStudents = 80, SubjectId = "IK-CPP_E+GY" },
			new Course { CourseId = "IK-CPP_E+GY/3",  TeacherId = 7, Schedule = "Friday 10:00-12:00", MaxStudents = 12, SubjectId = "IK-CPP_E+GY" },
			new Course { CourseId = "IK-CPP_E+GY/4",  TeacherId = 7, Schedule = "Friday 12:00-14:00", MaxStudents = 25, SubjectId = "IK-CPP_E+GY" },
			new Course { CourseId = "IK-CPP_E+GY/5",  TeacherId = 8, Schedule = "Friday 10:00-12:00", MaxStudents = 12, SubjectId = "IK-CPP_E+GY" },

			new Course { CourseId = "IK-OAF_E+GY/1",  TeacherId = 6, Schedule = "Monday 08:30-10:00", MaxStudents = 70, SubjectId = "IK-OAF_E+GY" },
			new Course { CourseId = "IK-OAF_E+GY/2",  TeacherId = 7, Schedule = "Monday 08:30-10:00", MaxStudents = 12, SubjectId = "IK-OAF_E+GY" },
			new Course { CourseId = "IK-OAF_E+GY/3",  TeacherId = 8, Schedule = "Monday 08:30-10:00", MaxStudents = 12, SubjectId = "IK-OAF_E+GY" },


			new Course { CourseId = "BG-PSZ_E+GY/1",  TeacherId = 9,  Schedule = "Tuesday 10:00-11:00", MaxStudents = 32, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/2",  TeacherId = 10, Schedule = "Tuesday 11:00-12:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/3",  TeacherId = 9,  Schedule = "Tuesday 11:00-12:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/4",  TeacherId = 10, Schedule = "Tuesday 12:00-13:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/5",  TeacherId = 9,  Schedule = "Tuesday 12:00-13:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/6",  TeacherId = 9,  Schedule = "Tuesday 13:00-14:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/7",  TeacherId = 10, Schedule = "Tuesday 13:00-14:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },
			new Course { CourseId = "BG-PSZ_E+GY/8",  TeacherId = 9,  Schedule = "Tuesday 14:00-15:00", MaxStudents = 26, SubjectId = "BG-PSZ_E+GY" },

			new Course { CourseId = "BG-KV_E+GY/1",   TeacherId = 10, Schedule = "Monday 09:00-10:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/2",   TeacherId = 9,  Schedule = "Monday 11:00-12:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/3",   TeacherId = 10, Schedule = "Monday 10:00-11:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/4",   TeacherId = 10, Schedule = "Monday 11:00-12:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/5",   TeacherId = 9,  Schedule = "Monday 12:00-13:00", MaxStudents = 32, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/6",   TeacherId = 10, Schedule = "Monday 12:00-13:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/7",   TeacherId = 9,  Schedule = "Monday 13:00-14:00", MaxStudents = 64, SubjectId = "BG-KV_E+GY" },
			new Course { CourseId = "BG-KV_E+GY/8",   TeacherId = 10, Schedule = "Monday 13:00-14:00", MaxStudents = 26, SubjectId = "BG-KV_E+GY" },

			new Course { CourseId = "BG-GY_E+GY/1",   TeacherId = 10, Schedule = "Friday 08:00-10:00", MaxStudents = 26, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/2",   TeacherId = 10, Schedule = "Friday 10:00-12:00", MaxStudents = 26, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/3",   TeacherId = 9,  Schedule = "Friday 10:00-12:00", MaxStudents = 16, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/4",   TeacherId = 9,  Schedule = "Friday 12:00-14:00", MaxStudents = 32, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/5",   TeacherId = 10, Schedule = "Friday 12:00-14:00", MaxStudents = 26, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/6",   TeacherId = 10, Schedule = "Friday 14:00-16:00", MaxStudents = 24, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/7",   TeacherId = 9,  Schedule = "Friday 14:00-16:00", MaxStudents = 32, SubjectId = "BG-GY_E+GY" },
			new Course { CourseId = "BG-GY_E+GY/8",   TeacherId = 10, Schedule = "Friday 16:00-18:00", MaxStudents = 26, SubjectId = "BG-GY_E+GY" },
		};

		public static CourseChoice[] courseChoices = new CourseChoice[]
		{
			new CourseChoice { CourseId = "IK-CPP_E+GY/2", UserId = 1 }, // 1, 5 (kapott jegyek)
			new CourseChoice { CourseId = "IK-NM1_E+GY/3", UserId = 1 }, // 3
			new CourseChoice { CourseId = "IK-OAF_E+GY/1", UserId = 1 }, // 1
			new CourseChoice { CourseId = "IK-FP_E+GY/2",  UserId = 1 }, // .

			new CourseChoice { CourseId = "BG-PSZ_E+GY/2", UserId = 2 }, // 4
			new CourseChoice { CourseId = "BG-KV_E+GY/3",  UserId = 2 }, // 1, 4

			new CourseChoice { CourseId = "IK-FP_E+GY/2",  UserId = 3 }, // 5
			new CourseChoice { CourseId = "IK-OAF_E+GY/1", UserId = 3 }, // 5
			new CourseChoice { CourseId = "IK-NM1_E+GY/3", UserId = 3 }, // .

			new CourseChoice { CourseId = "BG-PSZ_E+GY/2", UserId = 4 }, // .
			new CourseChoice { CourseId = "BG-KV_E+GY/4",  UserId = 4 }, // 4

			new CourseChoice { CourseId = "IK-FP_E+GY/2",  UserId = 5 }, // 4
			new CourseChoice { CourseId = "IK-NM1_E+GY/3", UserId = 5 }, // 5
			new CourseChoice { CourseId = "IK-CPP_E+GY/2", UserId = 5 }, // 5
			new CourseChoice { CourseId = "IK-OAF_E+GY/1", UserId = 5 }  // .
		};

		public static Grade[] grades = new Grade[]
		{
			new Grade { CourseId = "IK-CPP_E+GY/2", UserId = 1, TeacherId = 6, GradeRecord = EGrade.FAIL },
			new Grade { CourseId = "IK-CPP_E+GY/2", UserId = 1, TeacherId = 6, GradeRecord = EGrade.VERYGOOD },
			new Grade { CourseId = "IK-NM1_E+GY/3", UserId = 1, TeacherId = 6, GradeRecord = EGrade.MEDIOCRE },
			new Grade { CourseId = "IK-OAF_E+GY/1", UserId = 1, TeacherId = 6, GradeRecord = EGrade.FAIL },

			new Grade { CourseId = "BG-PSZ_E+GY/2", UserId = 2, TeacherId = 10, GradeRecord = EGrade.GOOD },
			new Grade { CourseId = "BG-KV_E+GY/3",  UserId = 2, TeacherId = 10, GradeRecord = EGrade.FAIL },
			new Grade { CourseId = "BG-KV_E+GY/3",  UserId = 2, TeacherId = 10, GradeRecord = EGrade.GOOD },

			new Grade { CourseId = "IK-FP_E+GY/2",  UserId = 3, TeacherId = 6, GradeRecord = EGrade.VERYGOOD },
			new Grade { CourseId = "IK-OAF_E+GY/1", UserId = 3, TeacherId = 6, GradeRecord = EGrade.VERYGOOD },

			new Grade { CourseId = "BG-KV_E+GY/4",  UserId = 4, TeacherId = 10, GradeRecord = EGrade.GOOD },

			new Grade { CourseId = "IK-FP_E+GY/2",  UserId = 5, TeacherId = 6, GradeRecord = EGrade.GOOD },
			new Grade { CourseId = "IK-NM1_E+GY/3", UserId = 5, TeacherId = 6, GradeRecord = EGrade.VERYGOOD },
			new Grade { CourseId = "IK-CPP_E+GY/2", UserId = 5, TeacherId = 6, GradeRecord = EGrade.VERYGOOD }
		};

		public static User[] users = new User[]
		{
			new User { Id = 1, UserName = "ABCXYZ", Email = "kzsolt@gmail.com", Name = "Király Zsolt" },				
			new User { Id = 2, UserName = "KRZMEP", Email = "nesztr@gmail.com", Name = "Németh Eszter" },
			new User { Id = 3, UserName = "TUSMRE", Email = "ftamas@gmail.com", Name = "Fehér Tamás" },
			new User { Id = 4, UserName = "KFITPE", Email = "pbalin@gmail.com", Name = "Pap Bálint" },
			new User { Id = 5, UserName = "GKEITP", Email = "feadam@gmail.com", Name = "Fekete Ádám" },

			new User { Id = 6,  UserName = "TEACHR", Email = "koadam@gmail.com", Name = "Kovács Ádám" },
			new User { Id = 7,  UserName = "TEAFJR", Email = "npeter@gmail.com", Name = "Nagy Péter" },
			new User { Id = 8,  UserName = "TEAKDO", Email = "szsolt@gmail.com", Name = "Szabó Zsolt" },
			new User { Id = 9,  UserName = "TEAFIR", Email = "vkinga@gmail.com", Name = "Varga Kinga" },
			new User { Id = 10, UserName = "TEAFKR", Email = "mannna@gmail.com", Name = "Molnár Anna" }
		};
	}
}