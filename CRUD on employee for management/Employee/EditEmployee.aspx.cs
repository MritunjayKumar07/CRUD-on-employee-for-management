using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_on_employee_for_management.Employee
{
    public partial class EditEmployee1 : System.Web.UI.Page
    {
        private static string db = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["EmployeeID"];
                if (!string.IsNullOrEmpty(id))
                {
                    LoadUser(id);
                }
            }
        }

        public void LoadUser(string id)
        {
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            using (SqlConnection con = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtEmployeeID.Text = reader["EmployeeID"].ToString();
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                        LoadDropdownData(reader["Department"].ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Employee not found.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        public void LoadDropdownData(string oldDepartment)
        {
            string query = "SELECT DISTINCT Department FROM Employees";
            using (SqlConnection con = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    DropDownList1.DataSource = rd;
                    DropDownList1.DataValueField = "Department"; // The stored value & shown to the user same as text in this case
                    DropDownList1.DataBind();
                }
            }
            DropDownList1.Items.Insert(0, new ListItem(oldDepartment, ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string employeeID = txtEmployeeID.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string department = DropDownList1.SelectedValue;

            try
            {
                string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Department = @Department WHERE EmployeeID = @EmployeeID";
                using (SqlConnection con = new SqlConnection(db))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Department", department);

                        con.Open();
                        int rowAffect = cmd.ExecuteNonQuery();
                        if (rowAffect > 0)
                        {
                            lblMessage.Text = "Employee added successfully!";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                            Server.Transfer("ViewEmployees.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Error: Employee not updated. Please check the Employee ID.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}