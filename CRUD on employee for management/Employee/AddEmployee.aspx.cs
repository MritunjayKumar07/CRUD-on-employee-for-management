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
    public partial class AddEmployee : System.Web.UI.Page
    {
        private static string db = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdownData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string department = DropDownList1.SelectedValue;
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(department) || department == "--Select--")
            {
                lblMessage1.Text = "Please fill in all fields.";
                lblMessage1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string query = "INSERT INTO Employees (FirstName, LastName, Email, Phone, Department) VALUES (@FirstName, @LastName, @Email, @Phone, @Department)";

            try
            {
                using (SqlConnection con = new SqlConnection(db))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Department", department);

                        con.Open();
                        int rowAffect = cmd.ExecuteNonQuery();
                        if (rowAffect > 0)
                        {
                            lblMessage1.Text = "Employee added successfully!";
                            Response.Redirect("ViewEmployees.aspx");
                            return;
                        }
                        else
                        {
                            lblMessage1.Text = "Error adding employee.";
                            lblMessage1.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage1.Text = "An error occurred: " + ex.Message;
                lblMessage1.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void LoadDropdownData()
        {
            string query = "SELECT DISTINCT Department FROM Employees";
            using(SqlConnection con = new SqlConnection(db))
            {
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    DropDownList1.DataSource = rd;
                    DropDownList1.DataValueField = "Department"; // The stored value & shown to the user same as text in this case
                    DropDownList1.DataBind();
                }
            }
            DropDownList1.Items.Insert(0, new ListItem("---Select---", ""));
        }
    }
}