using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Employee;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace MyStore.Pages.Stats
{
    public class SalaryStatsModel : PageModel
    {
		public int highestSalary;
		public int averageSalary;
		public int lowestSalary;
		public List<string> startingWithA;
		public Dictionary<string, string> sameAddress;
		public List<MouseUserInfo> mouseUsers = new List<MouseUserInfo>();
		public string cityName = "Baramati";
		public string errorMessage = "";
		public string word = "A";
		public Dictionary<string, string> sameAddressUsingProcedure;
		public List<string> employeesWithHighestSalaryList;
		public List<string> employeesWitLowestSalaryList;
		public List<TotalYearsCompletedTableInfo> totalYearsCompletedTableInfoList = new List<TotalYearsCompletedTableInfo>();


		//Queries
		string highestSalaryQuery = "SELECT MAX(salary) from Employees";
		string lowestSalaryQuery = "SELECT MIN(salary) from Employees";
		string averageSalaryQuery = "SELECT AVG(salary) from Employees";

		string employeesWithHighestSalaryQuery = "SELECT name FROM Employees WHERE salary = (SELECT MAX(salary) from Employees)";
		string employeesWithLowestSalaryQuery = "SELECT name FROM Employees WHERE salary = (SELECT MIN(salary) from Employees)";

		string startingwithAQuery = "SELECT name from Employees where name like 'A%'";
		string sameAddressQuery = "SELECT name, email from Employees where address=@add";

		string updateQuery = "update Employees set address='baramati' where id=5";

		string totalYearsCompletedQuery = "SELECT e.employeeID, e.name, e.salary, DATEDIFF(yy, birthDate, CURRENT_TIMESTAMP) AS Age, DATEDIFF(yy, joiningDate, CURRENT_TIMESTAMP) AS YearsCompleted " +
									"FROM Employees e " +
									"INNER JOIN EmployeeDates ed " +
									"on e.employeeID = ed.employeeID " +
									"WHERE releaseDate is null " + 
									"order by e.employeeID asc";

		// Connection String
		string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";

		public SqlDataReader GetSqlDataReader(string sql, string oldValue, string newValue)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			SqlCommand command = new SqlCommand(sql, connection);

			if (oldValue != null && newValue != null) 
			{
				command.Parameters.AddWithValue(oldValue, newValue);
				command.ExecuteNonQuery();
			}

			SqlDataReader reader = command.ExecuteReader();

			return reader;
		}

		public int FindSalary(string sql)
		{
			try
			{
				using(SqlDataReader reader = GetSqlDataReader(sql, null, null))
				{
					if (reader.Read())
					{
						return reader.GetInt32(0);
					}
				}
				
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
				return 0;
			}
			return 0;
		}

		public List<string> GetEmployeesWithSalary(string sql)
		{
			List<string> list = new List<string>();
			using(SqlDataReader reader = GetSqlDataReader(sql, null, null))
			{
				while (reader.Read()) 
				{
					list.Add(reader.GetString(0));
				}
				return list;
			}
		}
		public List<string> Find(string sql)
		{
			try
			{
				List<string> list = new List<string>();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						//command.Parameters.AddWithValue("@temp", letter);
						//command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while(reader.Read())
							{
								list.Add(reader.GetString(0));
							}
							return list;


							//if (reader.Read())
							//{
							//	foreach (var item in list)
							//	{
							//		list.Add(item.ToString());
							//	}
							//	return list;
							//}


							//IEnumerable<IDataRecord> GetFromReader(IDataReader reader)
							//{
							//	while (reader.Read()) yield return reader;
							//}

							//foreach (IDataRecord record in GetFromReader(reader))
							//{
							//	list.Add(record.ToString());
							//}
							//return list;

						}
					}
				}
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
				return null;
			}
		}



		//public Dictionary<string, string> FindSameAddress(string sql, string address)
		//{
		//	try
		//	{
		//		Dictionary<string, string> dict = new Dictionary<string, string>();

		//		using(SqlDataReader reader = GetSqlDataReader(sql, "@add", address))
		//		{
		//			while (reader.Read())
		//			{
		//				dict.Add(reader.GetString(0), reader.GetString(1));
		//			}
		//			return dict;
		//		}
		//	}
		//	catch (Exception Ex)
		//	{
		//		errorMessage = Ex.Message;
		//		return null;
		//	}
		//}

		public Dictionary<string, string> FindSameAddress(string sql, string address)
		{
			try
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();

				using (SqlDataReader reader = GetSqlDataReader(sql, "@add", address))
				{
					while (reader.Read())
					{
						dict.Add(reader.GetString(0), reader.GetString(1));
					}
					return dict;
				}
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
				return null;
			}
		}
		public void Update(string sql)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
			}
		}

		public Dictionary<string, string> GetSameAddressInfo(string address)
		{
			try
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();
				
				SqlConnection connection = new SqlConnection(connectionString);
				connection.Open();

				SqlCommand command = new SqlCommand("SameAddressInfo", connection);
				command.CommandType = CommandType.StoredProcedure;
				//command.Parameters.AddWithValue("@ADD", SqlDbType.VarChar).Value=address;
				command.Parameters.AddWithValue("@ADD", address);
				command.ExecuteNonQuery();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					dict.Add(reader.GetString(0), reader.GetString(1));
				}
				connection.Close();
				return dict;
			}
			catch (Exception Ex)
			{
				errorMessage = Ex.Message;
				return null;
			}
		}

		public List<TotalYearsCompletedTableInfo> GetTotalYearsCompletedTableInfo(string sql)
		{
			List<TotalYearsCompletedTableInfo> list = new List<TotalYearsCompletedTableInfo>();

			SqlDataReader reader = GetSqlDataReader(sql, null, null);
			while(reader.Read()) 
			{
				TotalYearsCompletedTableInfo totalYearsCompletedTableInfo = new TotalYearsCompletedTableInfo();
				totalYearsCompletedTableInfo.employeeID = reader.GetInt32(0);
				totalYearsCompletedTableInfo.name = reader.GetString(1);
				totalYearsCompletedTableInfo.salary = reader.GetInt32(2);
				totalYearsCompletedTableInfo.age = reader.GetInt32(3);
				totalYearsCompletedTableInfo.years = reader.GetInt32(4);

				list.Add(totalYearsCompletedTableInfo);
			}
			return list;
		}

		public void OnGet()
        {
			highestSalary = FindSalary(highestSalaryQuery);
			lowestSalary = FindSalary(lowestSalaryQuery);
			averageSalary = FindSalary(averageSalaryQuery);

			startingWithA = Find(startingwithAQuery);
			sameAddress = FindSameAddress(sameAddressQuery, cityName);

			sameAddressUsingProcedure = GetSameAddressInfo(cityName);
			employeesWithHighestSalaryList = GetEmployeesWithSalary(employeesWithHighestSalaryQuery);
			employeesWitLowestSalaryList = GetEmployeesWithSalary(employeesWithLowestSalaryQuery);

			totalYearsCompletedTableInfoList = GetTotalYearsCompletedTableInfo(totalYearsCompletedQuery);
			Update(updateQuery);
		}

		public void OnPost(string mouseName)
		{

		}
    }

	public class MouseUserInfo
	{
		public string name;
		public string phone;
		public string address;
		public int prn;
		public string mouse;
	}

	public class LaptopUserInfo
	{
		public string name;
		public string phone;
		public string address;
		public int prn;
		public string laptop;
	}

	public class ResourceUserInfo
	{
		public string name;
		public string phone;
		public string address;
		public int prn;
		public string reource;
	}

	public class TotalYearsCompletedTableInfo
	{
		public int employeeID;
		public string name;
		public int salary;
		public int age;
		public int years;
	}
}


// Working code, Without using common function
//public int FindSalary(string sql)
//{
//	try
//	{
//		string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyStore;Integrated Security=True";
//		using (SqlConnection connection = new SqlConnection(connectionString))
//		{
//			connection.Open();

//			using (SqlCommand command = new SqlCommand(sql, connection))
//			{
//				using (SqlDataReader reader = command.ExecuteReader())
//				{
//					if (reader.Read())
//					{
//						return reader.GetInt32(0);
//					}
//				}
//			}
//		}
//	}
//	catch (Exception Ex)
//	{
//		errorMessage = Ex.Message;
//		return 0;
//	}
//	return 0;
//}

// Working code, Without using common Function
//public Dictionary<string, string> FindSameAddress(string sql, string address)
//{
//	try
//	{
//		Dictionary<string, string> dict = new Dictionary<string, string>();
//		string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyStore;Integrated Security=True";
//		using (SqlConnection connection = new SqlConnection(connectionString))
//		{
//			connection.Open();

//			using (SqlCommand command = new SqlCommand(sql, connection))
//			{
//				command.Parameters.AddWithValue("@add", address);
//				command.ExecuteNonQuery();
//				using (SqlDataReader reader = command.ExecuteReader())
//				{
//					while (reader.Read())
//					{
//						dict.Add(reader.GetString(0), reader.GetString(1));
//					}
//					return dict;
//				}
//			}
//		}
//	}
//	catch (Exception Ex)
//	{
//		errorMessage = Ex.Message;
//		return null;
//	}
//}


// Creating Stored Procedure in SSMS

//CREATE PROC SameAddressInfo @ADD VARCHAR(30)
//AS
//BEGIN
//SELECT name, email from Employees where address=@ADD
//END
//Go

//Executing Stored Procedure

//EXEC SameAddressInfo @ADD='Baramati';