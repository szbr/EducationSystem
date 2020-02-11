using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;


namespace Test
{
	public class TStartup
	{
		public TStartup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices( IServiceCollection services )
		{
			services.AddDbContext<SysDbContext>( options =>
				 options.UseInMemoryDatabase( "TestDB" ) );

			services.AddMvc( );
		}

		public void Configure( IApplicationBuilder app, IHostingEnvironment env )
		{
			app.UseMvc( );
		}
	}
}