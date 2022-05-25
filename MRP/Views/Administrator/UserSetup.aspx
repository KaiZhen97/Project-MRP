<%@ Page Title="User Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserSetup.aspx.cs" Inherits="MRP.Views.Administrator.UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/UserSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <div class="mb-4">
        <button class="solid-button normal" id="btnAddNewUser">
            <i class="fa-solid fa-plus"></i>
            Add New User
        </button>
    </div>

    <table id="Table_UserList">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
                <th>Login ID</th>
                <th>Email</th>
                <th>Role</th>
                <th>Status</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="Add new User" id="Modal_AddNewUser">
        <form class="container-fluid" role="form" runat="server">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom: 10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>

            <div class="row">
                <div class="col-12">
                    <h5 class="_pageTitle">1 | Staff Info</h5>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_StaffNumber" class="required">Staff Number</label>
                    <input id="inputAddNewUser_StaffNumber" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_Name" class="required">Staff Name</label>
                    <input id="inputAddNewUser_Name" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_EmailAddress" class="required">Email Address</label>
                    <input type="email" id="inputAddNewUser_EmailAddress" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_PhoneNumber" class="required">Phone Number</label>
                    <input id="inputAddNewUser_PhoneNumber" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_Designation" class="required">Designation</label>
                    <input id="inputAddNewUser_Designation" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewUser_Department" class="required">Department</label>
                    <asp:DropDownList runat="server" ID="selectAddNewUser_Department" name="selectAddNewUser_Department" Native="true" required="true"></asp:DropDownList>
                </div>

                <div class="col-12">
                    <iv class="_seperator"></iv>
                </div>

                <div class="col-12">
                    <h5 class="_pageTitle">2 | Login Info</h5>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_LoginID" class="required">Login ID</label>
                    <input id="inputAddNewUser_LoginID" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewUser_Roles" class="required">Roles</label>
                    <asp:DropDownList runat="server" ID="selectAddNewUser_Roles" name="selectAddNewUser_Roles" Native="true" required="true"></asp:DropDownList>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_Password" class="required">Password</label>
                    <input type="password" id="inputAddNewUser_Password" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewUser_UserTeam">User Team</label>
                    <asp:DropDownList runat="server" ID="selectAddNewUser_UserTeam" name="selectAddNewUser_UserTeam" Native="true"></asp:DropDownList>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewUser_ConfirmPassword" class="required">Confirm Password</label>
                    <input type="password" id="inputAddNewUser_ConfirmPassword" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewUser_Status" class="required">Status</label>
                    <%--<select id="selectAddNewUser_Status"></select>--%>
                    <asp:DropDownList runat="server" ID="selectAddNewUser_Status" name="selectAddNewUser_Status" Native="true" required="true"></asp:DropDownList>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-md-12 flex flex-row-reverse">
                    <button class="solid-button submit" id="btnAddNewUser_Submit">
                        Save
                    </button>
                </div>
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/UserSetup") %>

    <script>
        var selectAddNewUser_Department = "<%= selectAddNewUser_Department.ClientID.ToString() %>";
        var selectAddNewUser_Roles = "<%= selectAddNewUser_Roles.ClientID.ToString() %>";
        var selectAddNewUser_UserTeam = "<%= selectAddNewUser_UserTeam.ClientID.ToString() %>";
        var selectAddNewUser_Status = "<%= selectAddNewUser_Status.ClientID.ToString() %>";

        var authToken = "<%= Session["accessToken"].ToString() %>"
        var reqTableUserListUrl = "<%= ResolveUrl("~/api/User/getUserList") %>";
        var reqSaveAddNewUserUrl = "<%= ResolveUrl("~/api/User/postAddUser") %>";

        $(document).ready(function () {
            loadTable_UserList(reqTableUserListUrl, authToken);

            $("#btnAddNewUser").click(function () {
                var targetDrawer = $("#Modal_AddNewUser");

                // Reset Input
                Drawer_ResetInput(targetDrawer);

                Drawer_Show(targetDrawer, false);
            });

            $("#Modal_AddNewUser").submit(function (e) {
                e.preventDefault();

                // Confirm password validation
                if ($("#inputAddNewUser_Password").val() !== $("#inputAddNewUser_ConfirmPassword").val()) {
                    ShowAlert("Form Validation Failed", "Password and Confirm Password not match!");
                    $("#inputNewUser_ConfirmPassword").focus()
                    return;
                }

                $("#btnAddNewUser_Submit").addClass('busy');

                addNewUser()
                    .then((data) => {
                        ShowPass("Add New User Success", data || "Add successfully!");
                        Drawer_Hide("#Modal_AddNewUser");
                    })
                    .catch((error) => {
                        ShowError("Fail To Add New User", error.responseText || error)
                    })
                    .finally(() => {
                        $("#btnAddNewUser_Submit").removeClass('busy');
                    })
            })
        })
    </script>
</asp:Content>
