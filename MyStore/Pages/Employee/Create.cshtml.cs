using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using MyStore.Pages.Models;

namespace MyStore.Pages.Employee
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public EmployeeInsertInfo employeeInsertInfo = new EmployeeInsertInfo();
        public string errorMessage = "";
        public string successMessage = "";

        //Queries
        public string insertQuery = "BEGIN TRANSACTION " +
                                        "INSERT INTO Employees(id, name, email, phone, address, salary, employeeID)" +
                                        "VALUES(@id, @name, @email, @phone, @address, @salary, @employeeID)" +
                                        "INSERT INTO EmployeeDates(employeeID, joiningDate, birthDate)" +
                                        "VALUES(@employeeID, @joiningDate, @birthDate)" +
                                    "COMMIT TRANSACTION";
		public void OnGet()
        {
        }

        public void OnPost() 
        {
            employeeInsertInfo.id = Request.Form["id"];
			employeeInsertInfo.name = Request.Form["name"];
			employeeInsertInfo.email = Request.Form["email"];
			employeeInsertInfo.phone = Request.Form["phone"];
			employeeInsertInfo.address = Request.Form["address"];
            employeeInsertInfo.salary = Convert.ToInt32(Request.Form["salary"]);
            employeeInsertInfo.employeeID = Convert.ToInt32(Request.Form["employeeID"]);
			
			try
            {
				employeeInsertInfo.joingingDate = DateTime.ParseExact(Request.Form["joiningDate"], "yyyy/MM/dd", null);
				employeeInsertInfo.birthdate = DateTime.ParseExact(Request.Form["birthDate"], "yyyy/MM/dd", null);
			}
            catch (Exception ex)
            {
                
            }

            if (employeeInsertInfo.name.Length == 0 || employeeInsertInfo.email.Length == 0 || employeeInsertInfo.phone.Length == 0
                || employeeInsertInfo.address.Length == 0 || employeeInsertInfo.salary.ToString().Length == 0 || employeeInsertInfo.employeeID.ToString().Length == 0 ||
                employeeInsertInfo.joingingDate.Date == DateTime.MinValue.Date || employeeInsertInfo.birthdate.Date == DateTime.MinValue.Date)
            {
				TempData["error"] = "All fields are required";
				errorMessage = "All fields are required!";
                return;
            }



            //Save the new client into database

            try
            {
				string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";
				
    //            using (SqlConnection connection = new SqlConnection(connectionString)) 
    //            {
    //                connection.Open();
    //                String sql = "INSERT INTO Employees " +
    //                             "(id, name, email, phone, address, salary, employeeID) VALUES " +
    //                             "(@id, @name, @email, @phone, @address, @salary, @employeeID);";

    //                using (SqlCommand command = new SqlCommand(sql, connection))
    //                {
    //                    command.Parameters.AddWithValue("id", employeeInfo.id);
    //                    command.Parameters.AddWithValue("@name", employeeInfo.name);
    //                    command.Parameters.AddWithValue("@email", employeeInfo.email);
    //                    command.Parameters.AddWithValue("@phone", employeeInfo.phone);
    //                    command.Parameters.AddWithValue("@address", employeeInfo.address);
    //                    command.Parameters.AddWithValue("@salary", employeeInfo.salary);
    //                    command.Parameters.AddWithValue("@employeeID", employeeInfo.employeeID);

    //                    command.ExecuteNonQuery();
    //                }
				//}


				// new
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(insertQuery, connection))
					{
						command.Parameters.AddWithValue("id", employeeInsertInfo.id);
						command.Parameters.AddWithValue("@name", employeeInsertInfo.name);
						command.Parameters.AddWithValue("@email", employeeInsertInfo.email);
						command.Parameters.AddWithValue("@phone", employeeInsertInfo.phone);
						command.Parameters.AddWithValue("@address", employeeInsertInfo.address);
						command.Parameters.AddWithValue("@salary", employeeInsertInfo.salary);
						command.Parameters.AddWithValue("@employeeID", employeeInsertInfo.employeeID);
                        command.Parameters.AddWithValue("@joiningDate", employeeInsertInfo.joingingDate);
                        command.Parameters.AddWithValue("@birthDate", employeeInsertInfo.birthdate);

						command.ExecuteNonQuery();
					}
				}
			}
            catch (Exception Ex)
            {
                errorMessage = Ex.Message;
                return;
            }

            employeeInsertInfo.id = "";
            employeeInsertInfo.name = "";
            employeeInsertInfo.email = "";
            employeeInsertInfo.phone = "";
            employeeInsertInfo.address = "";
            employeeInsertInfo.salary = 0;
            employeeInsertInfo.employeeID = 0;
            employeeInsertInfo.joingingDate = default;
            employeeInsertInfo.birthdate = default;

			TempData["success"] = "New employee added successfully!";
			successMessage = "New client added successfully";
            Response.Redirect("/Employee/Index");
        }
    }
}
