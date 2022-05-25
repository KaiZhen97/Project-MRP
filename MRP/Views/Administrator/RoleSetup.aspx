<%@ Page Title="Role Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleSetup.aspx.cs" Inherits="MRP.Views.Administrator.RoleSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/RoleSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <form runat="server">
        <asp:TextBox runat="server" ID="hiddenRoleID" ClientInstanceName="hiddenRoleID"></asp:TextBox>
        <asp:TextBox runat="server" ID="hiddenChkBoxValue" ClientInstanceName="hiddenChkBoxValue"></asp:TextBox>
    </form>

    <div class="_button-container">
        <button class="solid-button normal" id="btnAddNewRole">
            <i class="fa-solid fa-plus"></i>
            Add New Role
        </button>
    </div>

    <table id="Table_RoleList">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
                <th>Description</th>
                <th>Status</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="Add New Role" id="Modal_AddNewRole">
        <form class="container-fluid">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom: 10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>

            <div class="row">
                <div class="col-md-6 input-container">
                    <label for="inputAddNewRole_RoleName" class="required">Role Name</label>
                    <input id="inputAddNewRole_RoleName" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewRole_Description" class="required">Description</label>
                    <input id="inputAddNewRole_Description" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewRole_Status" class="required">Description</label>
                    <select id="selectAddNewRole_Status" required></select>
                </div>

                <div class="flex flex-row-reverse mt-3">
                    <button class="solid-button submit" id="btnSaveAddNewRole">
                        Save
                    </button>
                </div>
            </div>
        </form>
    </div>

    <div class="drawer-container" title="Edit Role" id="Modal_EditRole">
        <form class="container-fluid">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom: 10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>

            <div class="row">
                <div class="col-md-6 input-container">
                    <label for="inputEditRole_RoleName" class="required">Role Name</label>
                    <input id="inputEditRole_RoleName" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputEditRole_Description" class="required">Description</label>
                    <input id="inputEditRole_Description" required />
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectEditRole_Status" class="required">Description</label>
                    <select id="selectEditRole_Status" required></select>
                </div>

                <div class="flex flex-row-reverse mt-3">
                    <button class="solid-button submit" id="btnSaveEditRole">
                        Save
                    </button>
                </div>
            </div>
        </form>
    </div>

    <div class="drawer-container" title="Role Module Setup" id="Modal_RoleModuleSetup">
        <table>
            <thead>
                <tr>
                    <th>
                        <div class="_checkbox-cont">
                            <input type="checkbox" id="chkBoxRoleModule_SelectAll"/>
                            <i class="fa-solid fa-check _checkbox-checkmark"></i>
                        </div>

                    </th>
                    <th colspan="3">Select All</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <div class="flex flex-row-reverse mt-3">
            <button class="solid-button submit" id="btnSaveRoleModule">
                Save
            </button>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/RoleSetup") %>

    <script>
        var hiddenRoleID_ID = "<%= hiddenRoleID.ClientID%>"
        var hiddenChkBoxValue_ID = "<%= hiddenChkBoxValue.ClientID%>"
        $(`#${hiddenRoleID_ID}, #${hiddenChkBoxValue_ID}`).css('display', 'none');

        var authToken = "<%= Session["accessToken"].ToString() %>";
        var reqTableRoleListUrl = "<%= ResolveUrl("~/api/UAM/getRoleList") %>";
        var reqUserStatusUrl = "<%= ResolveUrl("~/api/UAM/getUserStatus") %>";
        var reqAddNewRoleUrl = "<%= ResolveUrl("~/api/UAM/postAddRole") %>";
        var reqRoleDetailsByIdUrl = "<%= ResolveUrl("~/api/UAM/postRoleDetailsByID")%>";
        var reqEditRoleUrl = "<%= ResolveUrl("~/api/UAM/postEditRole") %>";
        var reqTableRoleModuleUrl = "<%= ResolveUrl("~/api/UAM/getRoleModuleList")%>";
        var reqCheckedRoleModuleUrl = "<%= ResolveUrl("~/api/UAM/postRoleModuleDetailsByID")%>";
        var reqSaveRoleModuleUrl = "<%= ResolveUrl("~/api/UAM/postEditRoleModule")%>";

        $(document).ready(function () {
            loadTable_RoleList(reqTableRoleListUrl, authToken);
            loadUserStatus(reqUserStatusUrl, authToken)

            $("#btnAddNewRole").on('click', function () {
                // Reset Modal
                $("#inputAddNewRole_RoleName").val("");
                $("#inputAddNewRole_Description").val("");
                $("selectAddNewRole_Status").val("");
                
                Drawer_Show("#Modal_AddNewRole");
            })

            $("#Modal_AddNewRole").submit(function (e) {
                e.preventDefault();

                $("btnSaveAddNewRole").addClass("busy");

                var dataStr = {
                    roleName: $("#inputAddNewRole_RoleName").val(),
                    roleDesc: $("#inputAddNewRole_Description").val(),
                    roleStatus: $("#selectAddNewRole_Status").val()
                }

                addNewRole(reqAddNewRoleUrl, authToken, dataStr)
                    .then((data) => {
                        ShowPass("Add New Role Success", data.Message || "Add successfully!");

                        // Reload Table
                        tableRoleList.ajax.reload();
                        ShowInfo("Data Table List Reload", "Table list has been reloaded with latest data.");

                        // Close Drawer
                        Drawer_Hide(this);
                    })
                    .catch((error) => {
                        ShowError("Fail To Add New Role", error.responseText || error);
                    })
            })

            $("#Modal_EditRole").submit(function (e) {
                e.preventDefault();

                $("btnSaveEditRole").addClass("busy");

                var dataStr = {
                    roleID: $(`#${hiddenRoleID_ID}`).val(),
                    roleName: $("#inputEditRole_RoleName").val(),
                    roleDesc: $("#inputEditRole_Description").val(),
                    status: $("#selectEditRole_Status").val()
                };

                editRole(reqEditRoleUrl, authToken, dataStr)
                .then((data) => {
                    ShowPass("Edit Role Success", data.Message || "Add successfully!");

                    // Reload Table
                    tableRoleList.ajax.reload();
                    ShowInfo("Data Table List Reload", "Table list has been reloaded with latest data.");

                    // Close Drawer
                    Drawer_Hide(this);
                })
                .catch((error) => {
                    ShowError("Fail To Edit Role", error.responseText || error);
                })
            })

            $("#btnSaveRoleModule").on('click', function () {
                $(this).addClass('busy');

                var dataStr = {
                    roleID: $(`#${hiddenRoleID_ID}`).val(),
                    moduleStr: $(`#${hiddenChkBoxValue_ID}`).val(),
                }

                saveRoleModule(reqSaveRoleModuleUrl, authToken, dataStr)
                    .then((data) => {
                        ShowPass('Save Role Module Successful', data || 'Save successfully!');
                        Drawer_Hide("#Modal_RoleModuleSetup");
                    })
                    .catch((error) => {
                        ShowError('Fail To Save Role Module', error.responseText || error)
                    })
                    .finally(() => {
                        $(this).removeClass('busy');
                    })
            })
        })

        $.contextMenu({
            selector: '#Table_RoleList tbody tr',
            className: 'css-title-toolbox',
            items: {
                "edit": {
                    name: "Edit",
                    icon: "fa-edit",
                    callback: function (key, options) {
                        var targetDrawer = $("#Modal_EditRole");
                        var rowData = tableRoleList.row(options.$trigger).data();

                        $(`#${hiddenRoleID_ID}`).val(rowData.RoleID);

                        var dataStr = {
                            roleID: rowData.RoleID
                        }
                        
                        Drawer_Show(targetDrawer, true);

                        loadRoleDetailsByID(reqRoleDetailsByIdUrl, authToken, dataStr)
                            .catch((error) => {
                                Drawer_Hide(targetDrawer);
                                ShowError("Load Role Details Failed", error.responseText || error);
                            })
                            .finally(() => {
                                targetDrawer.removeClass("loading");
                            })
                    }
                },
                "module": {
                    name: "Role Module Setup",
                    icon: "fa-gear",
                    callback: function (key, options) { 
                        var targetDrawer = $("#Modal_RoleModuleSetup");
                        var rowData = tableRoleList.row(options.$trigger).data();

                        $(`#${hiddenRoleID_ID}`).val(rowData.RoleID);

                        var dataStr = {
                            roleID: rowData.RoleID
                        }

                        // Reset Modal
                        $('#chkBoxRoleModule_SelectAll').prop('checked', false);
                        targetDrawer.find('tbody').html('');

                        Drawer_Show(targetDrawer, true);

                        loadTable_RoleModule(reqTableRoleModuleUrl, authToken)
                            .then(() => {
                                initTable_RoleModule();

                                return loadCheckedItem_RoleModule(reqCheckedRoleModuleUrl, authToken, dataStr);
                            })
                            .then(() => {
                                generateValueData();
                            })
                            .catch((error) => {
                                Drawer_Hide(targetDrawer);
                                ShowError('Fail To Role Module Table', error.responseText || error.toString())
                            })
                            .finally(() => {
                                Drawer_Show(targetDrawer, false);
                            })
                    }
                }
            }
        })
    </script>
</asp:Content>
