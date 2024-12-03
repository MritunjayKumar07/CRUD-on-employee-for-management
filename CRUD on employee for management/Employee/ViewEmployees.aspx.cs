using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace CRUD_on_employee_for_management.Employee
{
    public partial class Display : System.Web.UI.Page
    {
        private int TotalRecords = 0; // Total number of records
        private int PageSize = 5; // Number of records per page
        private int PageIndex
        {
            get
            {
                return (ViewState["PageIndex"] != null) ? (int)ViewState["PageIndex"] : 1;
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        private static string db = ConfigurationManager.ConnectionStrings["EmployeesDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PageIndex"] != null)
                {
                    PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
                }
                GetTotalRecords(null);
                LoadData(null);
            }
        }



        // Calculate the total records
        private int GetTotalRecords(string searchQuery)
        {
            string query = "SELECT COUNT(*) FROM Employees";
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = "SELECT COUNT(*) FROM Employees WHERE FirstName LIKE @search OR LastName LIKE @search";
            }

            using (SqlConnection con = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                    }
                    con.Open();
                    TotalRecords = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return TotalRecords;
        }

        // Generate pagination links
        private void GeneratePaginationLinks()
        {
            if (TotalRecords == 0)
            {
                Pagination.Text = "<span>No records found.</span>";
                return;
            }

            int totalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
            string paginationHtml = "<nav aria-label='Page navigation example'><ul class='pagination'>";

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == PageIndex)
                {
                    // Active page
                    paginationHtml += $"<li class='page-item active'><a class='page-link' href='#'>{i}</a></li>";
                }
                else
                {
                    // Other pages
                    paginationHtml += $"<li class='page-item'><a class='page-link' href='?PageIndex={i}'>{i}</a></li>";
                }
            }

            paginationHtml += "</ul></nav>";
            Pagination.Text = paginationHtml;
        }



        public void LoadData(string searchQuery)
        {
            string que;
            try
            {
                using (SqlConnection con = new SqlConnection(db))
                {
                    que = "SELECT * FROM Employees ORDER BY EmployeeID OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        GetTotalRecords(searchQuery);
                        que = "SELECT * FROM Employees WHERE FirstName LIKE @search OR LastName LIKE @search ORDER BY EmployeeID OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    }
                    using (SqlCommand cmd = new SqlCommand(que, con))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                        }
                        cmd.Parameters.AddWithValue("@Offset", (PageIndex - 1) * PageSize);
                        cmd.Parameters.AddWithValue("@PageSize", PageSize);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        rpt.DataSource = reader;
                        rpt.DataBind();
                    }
                }
                GeneratePaginationLinks();
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Event handler to change the page
        protected void ChangePage(int pageIndex)
        {
            Response.Write("alert('Hello! ' + pageIndex);");
            PageIndex = pageIndex;
            LoadData(txtSearch.Text);
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int pageIndex = Convert.ToInt32(btn.CommandArgument); // Retrieve the CommandArgument
            PageIndex = pageIndex;
            LoadData(txtSearch.Text); // Load data based on the selected page index
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                PageIndex = 1;
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