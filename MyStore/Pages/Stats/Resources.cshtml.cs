using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Configuration;
using MyStore.Pages.Models;

namespace MyStore.Pages.Stats
{
    public class ResourcesModel : PageModel
    {
		public bool isLpatopListEmpty = false;

		// Connection String
		public string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";

		// Queries
		public string getAllMouseUsersQuery = "SELECT e.name, e.phone, e.address, er.prn, er.mouse " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID";

		public string getAllLaptopUsersQuery = "SELECT e.name, e.phone, e.address, er.prn, er.laptop " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID";

		public string getDropDownMouseListQuery = "SELECT DISTINCT(mouse) from EmployeeResources";

		public string mouseUsersQuery = string.Empty;
		public List<MouseUserInfo> mouseUsers = new List<MouseUserInfo>();

		public string laptopUsersQuery = string.Empty;
		public List<LaptopUserInfo> laptopUsers = new List<LaptopUserInfo>();

		//public List<string> DropDownList1 = new List<string>();

		public string selectMouseQuery(string name)
		{
			string selectedMouseQuery;
			switch (name)
			{
				case "Logitech g102":
					selectedMouseQuery = "SELECT e.name, e.phone, e.address, er.prn, er.mouse " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE mouse = 'Logitech g102'";
					break;

				case "Viper Mini":
					selectedMouseQuery = "SELECT e.name, e.phone, e.address, er.prn, er.mouse " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE mouse = 'Viper Mini'";
					break;

				case "HP 650":
					selectedMouseQuery = "SELECT e.name, e.phone, e.address, er.prn, er.mouse " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE mouse = 'HP 650'";
					break;

				default:
					selectedMouseQuery = string.Empty;
					break;
			}
			return selectedMouseQuery;
		}

		public string selectLaptopQuery(string name)
		{
			string selectedLaptopQuery;
			switch (name)
			{
				case "DELL LATITUDE 4340":
					selectedLaptopQuery = "SELECT e.name, e.phone, e.address, er.prn, er.laptop " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE laptop = 'DELL LATITUDE 4340'";
					break;

				case "DELL LATITUDE 4110":
					selectedLaptopQuery = "SELECT e.name, e.phone, e.address, er.prn, er.laptop " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE laptop = 'DELL LATITUDE 4110'";
					break;

				case "HP 403":
					selectedLaptopQuery = "SELECT e.name, e.phone, e.address, er.prn, er.laptop " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE laptop = 'HP 403'";
					break;

				case "Chromebook 1":
					selectedLaptopQuery = "SELECT e.name, e.phone, e.address, er.prn, er.laptop " +
										"from Employees e " +
										"INNER JOIN EmployeeResources er " +
										"ON e.employeeID = er.employeeID " +
										"WHERE laptop = 'Chromebook 1'";
					break;

				default:
					selectedLaptopQuery = string.Empty;
					break;
			}
			return selectedLaptopQuery;
		}

		public List<MouseUserInfo> GetMouseUsers(string mouseName)
		{
			if (string.IsNullOrEmpty(mouseName))
			{
				return null;
			}
			try
			{
				mouseUsersQuery = selectMouseQuery(mouseName);

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(mouseUsersQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								MouseUserInfo mouseUserInfo = new MouseUserInfo();
								mouseUserInfo.name = reader.GetString(0);
								mouseUserInfo.phone = reader.GetString(1);
								mouseUserInfo.address = reader.GetString(2);
								mouseUserInfo.prn = reader.GetInt32(3);
								mouseUserInfo.mouse = reader.GetString(4);

								mouseUsers.Add(mouseUserInfo);
							}
							return mouseUsers;
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



		public List<MouseUserInfo> GetAllMouseUsers()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(getAllMouseUsersQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								MouseUserInfo mouseUserInfo = new MouseUserInfo();
								mouseUserInfo.name = reader.GetString(0);
								mouseUserInfo.phone = reader.GetString(1);
								mouseUserInfo.address = reader.GetString(2);
								mouseUserInfo.prn = reader.GetInt32(3);
								mouseUserInfo.mouse = reader.GetString(4);

								mouseUsers.Add(mouseUserInfo);
							}
							return mouseUsers;
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

		public List<LaptopUserInfo> GetLaptopUsers(string laptopName)
		{
			if (string.IsNullOrEmpty(laptopName))
			{
				return null;
			}
			try
			{
				laptopUsersQuery = selectLaptopQuery(laptopName);

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(laptopUsersQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								LaptopUserInfo laptopUserInfo = new LaptopUserInfo();
								laptopUserInfo.name = reader.GetString(0);
								laptopUserInfo.phone = reader.GetString(1);
								laptopUserInfo.address = reader.GetString(2);
								laptopUserInfo.prn = reader.GetInt32(3);
								laptopUserInfo.laptop = reader.GetString(4);

								laptopUsers.Add(laptopUserInfo);
							}
							return laptopUsers;
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

		public List<LaptopUserInfo> GetAllLaptopUsers()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(getAllLaptopUsersQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								LaptopUserInfo laptopUserInfo= new LaptopUserInfo();
								laptopUserInfo.name = reader.GetString(0);
								laptopUserInfo.phone = reader.GetString(1);
								laptopUserInfo.address = reader.GetString(2);
								laptopUserInfo.prn = reader.GetInt32(3);
								laptopUserInfo.laptop= reader.GetString(4);

								laptopUsers.Add(laptopUserInfo);
							}
							return laptopUsers;
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

		public List<string> GetDropDownMouseList()
		{
			try
			{
				List<string> list = new List<string>();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(getDropDownMouseListQuery, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								list.Add(reader.GetString(0));
							}
							return list;
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

		public void OnGet()
        {
			mouseUsers = GetAllMouseUsers();
			laptopUsers = GetAllLaptopUsers();
        }

		public void OnPost(string mouseName, string laptopName)
		{
			if (mouseName == null)
				mouseUsers = GetAllMouseUsers();
			else
				mouseUsers = GetMouseUsers(mouseName);

			if (laptopName == null)
				laptopUsers = GetAllLaptopUsers();
			else
			{
				laptopUsers = GetLaptopUsers(laptopName);
				if(laptopUsers.Count == 0)
				{
					isLpatopListEmpty = true;	
				}
			}
				
		}
	}
}