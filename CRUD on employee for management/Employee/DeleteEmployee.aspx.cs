using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_on_employee_for_management.Employee
{
    public partial class DeleteEmployee : System.Web.UI.Page
    {
        private static string db = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string employeeID = Request.QueryString["EmployeeID"];
            try
            {
                string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlConnection con = new SqlConnection(db))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                        con.Open();
                        int rowAffect = cmd.ExecuteNonQuery();
                        if (rowAffect > 0)
                        {
                            Response.Write("<script>alert('Employee deleted successfully!'); window.location='ViewEmployees.aspx';</script>");
                            Response.Redirect("ViewEmployees.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('No employee found with this ID.');</script>");
                            Response.Redirect("ViewEmployees.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
                Response.Redirect("ViewEmployees.aspx");
            }
        }
    }
}