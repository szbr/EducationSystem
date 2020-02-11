using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Website.Models;


namespace Website.Controllers
{
	public class AccountController : BaseController
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;


		public AccountController( ISysService sysService, UserManager<User> userManager, SignInManager<User> signInManager ) : base( sysService )
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}


		public IActionResult Index( )
		{
			return RedirectToAction( "Login" );
		}

		[HttpGet]
		public IActionResult Login( )
		{
			return View( "Login" );
		}

		[HttpGet]
		public IActionResult Register( )
		{
			return View( "Register" );
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login( LoginVM loginData )
		{
			const string invalidMsg = "Invalid User Name or Password, or not a student account.";

			if( !ModelState.IsValid )
				return View( "Login", loginData );

			User user = await userManager.FindByNameAsync( loginData.UserName );
			if( user == null || (! await userManager.IsInRoleAsync(user, StaticData.Student)) )
			{
				ModelState.AddModelError( "", invalidMsg );
				return View( "Login", loginData );
			}

			var result = await signInManager.PasswordSignInAsync( loginData.UserName, loginData.UserPassword, loginData.RememberLogin, false );
			if( !result.Succeeded )
			{
				ModelState.AddModelError( "", invalidMsg );
				return View( "Login", loginData );
			}

			return RedirectToAction( "Index", "Home" );
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register( RegisterVM user )
		{
			if( !ModelState.IsValid )
				return View( "Register", user );

			User newUser = new User
			{
				UserName = user.UserName,
				Email = user.Email,
				Name = user.FullName
			};

			var result = await userManager.CreateAsync( newUser, user.UserPassword );
			if( !result.Succeeded )
			{
				foreach( var error in result.Errors )
				{
					ModelState.AddModelError( "", error.Description );
				}
				return View( "Register", user );
			}

			await signInManager.SignInAsync( newUser, false );
			return RedirectToAction( "Index", "Home" );
		}

		[Authorize( Roles = StaticData.Student )]
		public async Task<IActionResult> Logout( )
		{
			await signInManager.SignOutAsync( );
			return RedirectToAction( "Index", "Home" );
		}
	}
}