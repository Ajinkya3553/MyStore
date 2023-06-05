using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MyStore.Pages.Auth
{
    public class SignupModel : PageModel
    {
        public SignupCreds signupCreds = new SignupCreds();
		public string errorMessage = "";
		public string successMessage = "";

        
		public void OnGet()
        {

        }
        public void OnPost()
        {
            signupCreds.username = Request.Form["username"];
            signupCreds.password = Request.Form["password"];
            signupCreds.confirmPassword = Request.Form["confirmPassword"];

            if(signupCreds.username.Length == 0 || signupCreds.password.Length == 0 || signupCreds.confirmPassword.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            if(signupCreds.password != signupCreds.confirmPassword)
            {
                errorMessage = "Passwords does not match!";
				signupCreds.password = "";
                signupCreds.confirmPassword = "";
				return;
            }

            try
            {
                signupCreds.password = signupCreds.HashPassword(signupCreds.password);
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyStore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO creds" +
                                "(username, password) VALUES " +
                                "(@username, @password);";

					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", signupCreds.username);
                        command.Parameters.AddWithValue("@password", signupCreds.password);

                        command.ExecuteNonQuery();
                    }
				}

			}
            catch (Exception Ex)
            {
                errorMessage = Ex.Message;
                return;
            }

            signupCreds.username = "";
            signupCreds.password = "";
			successMessage = "Sign in Successful!";
			//Response.Redirect("/Auth/Login");
		}
    }
}
