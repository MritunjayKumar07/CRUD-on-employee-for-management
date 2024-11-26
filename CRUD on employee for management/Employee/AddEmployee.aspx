<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="CRUD_on_employee_for_management.Employee.AddEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Employee</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</head>
<body>
    <section class="h-100 h-custom">
        <div class="container py-5 h-100">
            <form id="form1" runat="server" class="px-md-2">
                <h2>Add Employee</h2>
                <table>
                    <tr class="form-group">
                        <td>First Name:</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="form-group">
                        <td>Last Name:</td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required." ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="form-group">
                        <td>Email:</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ValidationExpression="\w+@\w+\.\w+" ForeColor="Red" Text="*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="form-group">
                        <td>Phone:</td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number." ValidationExpression="^\d{10}$" ForeColor="Red" Text="*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="form-group">
                        <td>Department:</td>
                        <td>
                            <div class="row">
                                <div class="col-12">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="select form-control-lg">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Department is required." ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage1" runat="server" ForeColor="Green"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="#f2f1f0" />
            </form>
        </div>
    </section>
</body>
</html>
