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
    public partial class Display : System.Web.UI.Page
    {
        private static string db = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData(null);
        }

        public void LoadData(string searchQuery)
        {
            string que;
            try
            {
                using (SqlConnection con = new SqlConnection(db))
                {
                    que = "SELECT * FROM Employees";
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        que = "SELECT * FROM Employees WHERE FirstName LIKE @search OR LastName LIKE @search";
                    }
                    using (SqlCommand cmd = new SqlCommand(que, con))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        }
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        rpt.DataSource = reader;
                        rpt.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                LoadData(searchText);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string x = e.ToString();
            Response.Write(x);
        }

    }
}