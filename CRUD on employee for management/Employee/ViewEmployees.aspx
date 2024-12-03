<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ViewEmployees.aspx.cs" Inherits="CRUD_on_employee_for_management.Employee.Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ChangePage(pageIndex) {
            alert('Hello! Page Index: ' + pageIndex); // Debugging the page index
            __doPostBack('<%= Page.ClientID %>', 'ChangePage_' + pageIndex); // Postback to the server
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="navbar navbar-light bg-light">
        <div class="form-inline">
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text" id="basic-addon1">@</span>
                </div>
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search by Name" aria-label="Search by Name" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="Search" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Employee/AddEmployee.aspx">Add new</asp:HyperLink>
    </nav>
    <br />
    <br />
    <table class="table">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">Name</th>
                <th scope="col">Email</th>
                <th scope="col">Phone</th>
                <th scope="col">Department</th>
                <th scope="col">Action</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("EmployeeID") %></th>
                        <td><%# Eval("FirstName") %> <%# Eval("LastName") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("Phone") %></td>
                        <td><%# Eval("Department") %></td>
                        <td>
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-warning" NavigateUrl='<%# "~/Employee/EditEmployee.aspx?EmployeeID=" + Eval("EmployeeID") %>'>Edit</asp:HyperLink>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-danger" NavigateUrl='<%# "~/Employee/DeleteEmployee.aspx?EmployeeID=" + Eval("EmployeeID") %>'>Delete</asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="6">
                    <asp:Literal ID="Pagination" runat="server"></asp:Literal>
                </td>
            </tr>
        </tfoot>
    </table>

</asp:Content>
