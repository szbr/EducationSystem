using Microsoft.AspNetCore.Identity;


namespace Persistence
{
	public class User : IdentityUser<int>
	{
		/* IdentityUser<int>
		public virtual TKey=int Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual string PasswordHash { get; set; }
		...
		*/

		public string Name { get; set; } // Teljes név
	}
}