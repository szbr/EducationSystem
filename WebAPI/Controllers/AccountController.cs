using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.DTO;


namespace WebAPI.Controllers
{
	[Route( "api/[controller]" )]
    public class AccountController : ControllerBase
    {
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;


		public AccountController( UserManager<User> userManager, SignInManager<User> signInManager )
		{
			this.userManager = userManager;
			this.signInManager = signInManager;	
		}


		[HttpPost( "[action]" )]
		public async Task<IActionResult> Login( [FromBody] LoginDTO loginData )
		{
			if( signInManager.IsSignedIn( User ) )
				await signInManager.SignOutAsync( );

			if( string.IsNullOrEmpty( loginData.UserName ) || string.IsNullOrEmpty( loginData.UserPassword ) )
				return Unauthorized( );

			User user = await userManager.FindByNameAsync( loginData.UserName );
			if( user == null || (!await userManager.IsInRoleAsync( user, StaticData.Teacher )) )
			{
				return Unauthorized( );
			}

			var result = await signInManager.PasswordSignInAsync( loginData.UserName, loginData.UserPassword, false, false );
			if( !result.Succeeded )
			{
				return Unauthorized( );
			}

			return Ok( );
		}

		[HttpPost( "[action]" )]
		[Authorize( Roles = StaticData.Teacher )]
		public async Task<IActionResult> Logout( )
		{
			await signInManager.SignOutAsync( );
			return Ok( );
		}
    }
}