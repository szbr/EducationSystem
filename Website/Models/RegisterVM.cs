using System.ComponentModel.DataAnnotations;


namespace Website.Models
{
	public class RegisterVM
	{
		[Required( ErrorMessage = "Name is required." )]
		[StringLength( 64, ErrorMessage = "Max name length is 64." )]
		public string FullName { get; set; }

		[Required( ErrorMessage = "E-Mail is required." )]
		[EmailAddress( ErrorMessage = "E-Mail format is invalid." )]
		[DataType( DataType.EmailAddress )]
		public string Email { get; set; }

		[Required( ErrorMessage = "User Name is required." )]
		[RegularExpression( "^[A-Z0-9]{6}$", ErrorMessage = "User Name format is invalid. (Must be 6 len uppercase)" )]
		public string UserName { get; set; }

		[Required( ErrorMessage = "Password is required." )]
		[DataType( DataType.Password )]
		public string UserPassword { get; set; }

		[Required( ErrorMessage = "Password again is required." )]
		[Compare( nameof( UserPassword ), ErrorMessage = "Passwords do not match." )]
		[DataType( DataType.Password )]
		public string UserConfirmPassword { get; set; }
	}
}