using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Auth
{
    public class LoginModel : PageModel
    {
		public LoginCreds loginCreds = new LoginCreds();
		
		public string errorMessage = "";
		public string successMessage = "";

		// Queries
		//public string queryUserExists = "SELECT COUNT(username) FROM creds WHERE username = '@username';";
		public string queryUserExists = "SELECT password FROM creds WHERE username = 'Sachin';";
		public string queryGetPassword = "SELECT password FROM creds WHERE username = '@username';";
		public void OnGet()
        {
        }

		public void OnPost()
		{
			loginCreds.username = Request.Form["username"];
			loginCreds.password = Request.Form["password"];

			if (loginCreds.username.Length == 0 || loginCreds.password.Length == 0)
			{
				errorMessage = "All fields are required!";
				return;
			}

			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyStore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(queryUserExists, connection))
					{
						//command.Parameters.AddWithValue("@username", loginCreds.username);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							
							var temp2 = reader.Read();
							var temp3 = reader.GetValue(0);
							if (reader.Read())
							{
								var temp = reader.GetString(0);
								//var temp = reader.GetInt32(0);
								if (reader.GetInt32(0) == 0)
								{
									errorMessage = "User does not exist!";
									return;
								}
								else
								{
									using (SqlCommand getPassword = new SqlCommand(queryGetPassword, connection))
									{
										getPassword.Parameters.AddWithValue("@username", loginCreds.username);
										using (SqlDataReader passwordReader = getPassword.ExecuteReader())
										{
											if(passwordReader.Read()) 
											{
												var passtemp = passwordReader.GetString(0);
												if (loginCreds.HashPassword(loginCreds.password) == passwordReader.GetString(0))
												{
													loginCreds.username = "";
													loginCreds.password = "";
													successMessage = "Login successful!";
													Response.Redirect("/Clients/Index");
												}
												else
												{
													errorMessage = "Incorrect password!";
													loginCreds.password = "";
													return;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
				return;
			}
		}
    }
}
