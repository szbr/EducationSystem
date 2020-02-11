using System.ComponentModel.DataAnnotations;


namespace Website.Models
{
	public class LoginVM
	{
		[Required( ErrorMessage = "User Name is required." )]
		public string UserName { get; set; }

		[Required( ErrorMessage = "Password is required." )]
		[DataType( DataType.Password )]
		public string UserPassword { get; set; }

		public bool RememberLogin { get; set; }
	}
}