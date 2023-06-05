using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Security.Cryptography;

namespace MyStore.Pages.Auth
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }

    public class SignupCreds
    {
        public string username;
        public string password;
        public string confirmPassword;
        public string lastLogin;

		public string HashPassword(string password)
		{
			var sha = SHA256.Create();
			var asByteArray = Encoding.Default.GetBytes(password);
			var hashedPassword = sha.ComputeHash(asByteArray);
			return Convert.ToBase64String(hashedPassword);
		}
	}

    public class LoginCreds
    {
		public string username;
		public string password;

		public string HashPassword(string password)
		{
			var sha = SHA256.Create();
			var asByteArray = Encoding.Default.GetBytes(password);
			var hashedPassword = sha.ComputeHash(asByteArray);
			return Convert.ToBase64String(hashedPassword);
		}
	}
}
