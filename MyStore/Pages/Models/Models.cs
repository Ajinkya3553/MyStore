using System.Diagnostics.Contracts;

namespace MyStore.Pages.Models
{
	public class Models
	{
	}

	public class EmployeeInfo
	{
		public string id;
		public string name;
		public string email;
		public string phone;
		public string address;
		public int salary;
		public int employeeID;
		public string created_at;
	}

	public class EmployeeInsertInfo
	{
		public string id;
		public string name;
		public string email;
		public string phone;
		public string address;
		public int salary;
		public int employeeID;
		public string created_at;
		public DateTime joingingDate;
		public DateTime birthdate;
	}

	public class ResourceAllocationInfo
	{
		public int employeeID;
		public int prn;
		public string mouse;
		public string laptop;
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

	public class SampleEmployeeInfo
	{
		public string id;
		public string name;
		public string email;
		public string phone;
		public string address;
		public int salary;
		public string created_at;
		public int employeeID;
	}

	public class SampleEmployeeResources
	{
		public int employeeID;
		public int prn;
		public string laptop;
		public string mouse;
	}

	public class SampleEmployeeDates
	{
		public int employeeID;
		public DateTime joiningDate;
		public DateTime releaseDate;
		public DateTime birthDate;
	}
}
