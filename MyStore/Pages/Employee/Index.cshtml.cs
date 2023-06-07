using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using MyStore.Pages.Models;
namespace MyStore.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public List<EmployeeInfo> listEmployeesInfo = new List<EmployeeInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";

				//string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyStore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //string sql = "SELECT * FROM clients";
					string sql = "SELECT * FROM Employees order by id asc";

					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                employeeInfo.name = reader.GetString(1);
                                employeeInfo.email = reader.GetString(2);
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.phone = reader.GetString(3);
                                employeeInfo.address = reader.GetString(4);
                                employeeInfo.salary = reader.GetInt32(5);
                                employeeInfo.created_at = reader.GetDateTime(6).ToString();

                                listEmployeesInfo.Add(employeeInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Exception : " + Ex.Message + "Stack trace : " + Ex.StackTrace);
                throw;
            }
        }

    }

    
}
