using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Employee;
using System.Data.SqlClient;

namespace MyStore.Pages.Stats
{
    public class AllocateResourcesModel : PageModel
    {
        public ResourceAllocationInfo resourceAllocationInfo = new ResourceAllocationInfo();

		string connectionString = "Data Source=.\\sqlexpress01;Initial Catalog=MyStore;Integrated Security=True";
		public string errorMessage = string.Empty;
        public string successMessage = string.Empty;

        //Queries
        public string resourceAllocationQuery = "INSERT INTO EmployeeResources(employeeID, prn, laptop, mouse) " +
                                                "VALUES(@employeeID, @prn, @laptop, @mouse)";


		public void OnGet()
        {
        }

        public void OnPost() 
        {
            resourceAllocationInfo.employeeID = Convert.ToInt32(Request.Form["employeeID"]);
            resourceAllocationInfo.prn = Convert.ToInt32(Request.Form["prn"]);
            resourceAllocationInfo.mouse = Request.Form["mouse"];
            resourceAllocationInfo.laptop = Request.Form["laptop"];

            if(resourceAllocationInfo.employeeID == 0 || resourceAllocationInfo.prn == 0 || 
                resourceAllocationInfo.mouse.Length == 0 || resourceAllocationInfo.laptop.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(resourceAllocationQuery, connection))
                    {
						command.Parameters.AddWithValue("@employeeID", resourceAllocationInfo.employeeID);
                        command.Parameters.AddWithValue("@prn", resourceAllocationInfo.prn);
						command.Parameters.AddWithValue("@mouse", resourceAllocationInfo.mouse);
						command.Parameters.AddWithValue("@laptop", resourceAllocationInfo.laptop);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception Ex)
			{
                if (Ex.Message == "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Employee_ID\". The conflict occurred in database \"MyStore\", table \"dbo.Employees\", column 'employeeID'.\r\nThe statement has been terminated.")
                    errorMessage = "EmployeeID does not exist!";
                else    
                    errorMessage = Ex.Message;
				return;
			}

            resourceAllocationInfo.employeeID = 0;
            resourceAllocationInfo.prn = 0;
            resourceAllocationInfo.mouse = "";
            resourceAllocationInfo.laptop = "";

            successMessage = "Resources allocated successfully";
            Response.Redirect("/Stats/Resources");
		}

    }

    public class ResourceAllocationInfo
    {
        public int employeeID;
        public int prn;
        public string mouse;
        public string laptop;
    }
}
