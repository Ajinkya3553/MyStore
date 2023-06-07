using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using MyStore.Pages.Models;

namespace MyStore.Pages.Employee
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
				string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";
				
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Employees WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.email = reader.GetString(2);
                                employeeInfo.phone = reader.GetString(3);
                                employeeInfo.address = reader.GetString(4);
                                employeeInfo.salary = reader.GetInt32(5);
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

        public void OnPost()
        {
            employeeInfo.id = Request.Form["id"];
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.phone = Request.Form["phone"];
            employeeInfo.address = Request.Form["address"];
            if (Request.Form["salary"].ToString() != "")
                employeeInfo.salary = Convert.ToInt32(Request.Form["salary"]);


            if (employeeInfo.id.Length == 0 || employeeInfo.name.Length == 0 || employeeInfo.email.Length == 0 ||
                employeeInfo.phone.Length == 0 || employeeInfo.address.Length == 0 || employeeInfo.salary.ToString().Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

			try
			{
				string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";
				
                using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
                    String sql = "UPDATE Employees " +
                        "SET name=@name, email=@email, phone=@phone, address=@address, salary=@salary " +
                        "WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", employeeInfo.name);
						command.Parameters.AddWithValue("@email", employeeInfo.email);
						command.Parameters.AddWithValue("@phone", employeeInfo.phone);
						command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@salary", employeeInfo.salary);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message + "Stack trace" + Ex.StackTrace;
				return;
			}

            Response.Redirect("/Employee/Index");
		}
    }
}
