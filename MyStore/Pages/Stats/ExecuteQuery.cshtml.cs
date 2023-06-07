using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Models;
using System.Data.SqlClient;

namespace MyStore.Pages.Stats
{
    public class ExecuteQueryModel : PageModel
    {
		string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";

		public List<SampleEmployeeInfo> listEmployeesInfo = new List<SampleEmployeeInfo>();
		public List<SampleEmployeeResources> listSampleEmployeeResources = new List<SampleEmployeeResources>();

		//Queries
		public string getSampleEmployeesInfoQuery = "SELECT * FROM SampleEmployees order by employeeID asc";
		public string getSampleEmployeeResourcesInfoQuery = "SELECT * FROM SampleEmployeeResources order by employeeID asc";

		public void OnGet()
        {
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(getSampleEmployeesInfoQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								SampleEmployeeInfo employeeInfo = new SampleEmployeeInfo();
								employeeInfo.name = reader.GetString(1);
								employeeInfo.email = reader.GetString(2);
								employeeInfo.id = "" + reader.GetInt32(0);
								employeeInfo.phone = reader.GetString(3);
								employeeInfo.address = reader.GetString(4);
								employeeInfo.salary = reader.GetInt32(5);
								employeeInfo.created_at = reader.GetDateTime(6).ToString();
								employeeInfo.employeeID = reader.GetInt32(7);

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

			// GetSampleEmployeeResources Table
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(getSampleEmployeeResourcesInfoQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								SampleEmployeeResources sampleEmployeeResources = new SampleEmployeeResources();
								sampleEmployeeResources.employeeID = reader.GetInt32(0);
								sampleEmployeeResources.prn = reader.GetInt32(1);
								sampleEmployeeResources.laptop = reader.GetString(2);
								sampleEmployeeResources.mouse = reader.GetString(3);

								listSampleEmployeeResources.Add(sampleEmployeeResources);
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
