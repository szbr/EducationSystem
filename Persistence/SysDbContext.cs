using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
	public class SysDbContext : IdentityDbContext<User, IdentityRole<int>, int>
	{
		public SysDbContext( DbContextOptions<SysDbContext> options ) : base( options )
		{
				
		}

		public DbSet<Training> Trainings { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseChoice> CourseChoices { get; set; }
		public DbSet<Grade> Grades { get; set; }
	}
}