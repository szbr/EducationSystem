using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Persistence;


namespace WebAPI
{
	public class Startup
	{
		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddDbContext<SysDbContext>( options => options.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ) ) );

			services.AddIdentity<User, IdentityRole<int>>( )
				.AddEntityFrameworkStores<SysDbContext>( );

			services.Configure<IdentityOptions>( options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 3;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 30 );
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;
				options.User.RequireUniqueEmail = true;
			} );

			services.AddMvc( ).SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider )
		{
			if( env.IsDevelopment( ) )
			{
				app.UseDeveloperExceptionPage( );
			}
			else
			{
				app.UseHsts( );
			}

			app.UseAuthentication( );
			app.UseHttpsRedirection( );
			app.UseMvc( );

			DbInitializer.Initialize( serviceProvider );
		}
	}
}