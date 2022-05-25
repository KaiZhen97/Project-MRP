<%@ Page Title="Tab Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TabSetup.aspx.cs" Inherits="MRP.Views.Administrator.TabSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/TabSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <form runat="server">
        <asp:TextBox runat="server" ID="hiddenTabID" ClientInstanceName="hiddenTabID"></asp:TextBox>
    </form>

    <div class="_button-container mb-4">
        <button class="solid-button normal" id="btnAddNewTab">
            <i class="fa-solid fa-plus"></i>
            Add New Tab
        </button>
    </div>
    
    <table id="Table_TabList">
        <thead>
            <tr>
                <th></th>
                <th>Tab Name</th>
                <th>Tab Link</th>
                <th>Sequence</th>
                <th>Status</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="Add New Tab" id="Modal_AddNewTab">
        <form class="container-fluid">
            <div class="_remark">All fields with denoted "<span class="_required">*</span>" are required.</div>

            <div class="row">
                <div class="col-md-6 input-container">
                    <label for="inputAddNewTab_TabName" class="required">Tab Name</label>
                    <input id="inputAddNewTab_TabName" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewTab_HTMLTagId" class="required">HTML Tag ID</label>
                    <input id="inputAddNewTab_HTMLTagId" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewTab_DisplaySequence" class="required">Display Sequence</label>
                    <input id="inputAddNewTab_DisplaySequence" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewTab_TabLink" class="required">Tab Link</label>
                    <input id="inputAddNewTab_TabLink" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAddNewTab_HTMLIcon">HTML Icon</label>
                    <input id="inputAddNewTab_HTMLIcon"/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectAddNewTab_Status" class="required">Status</label>
                    <select id="selectAddNewTab_Status" required></select>
                </div>
            </div>

            <div class="flex flex-row-reverse mt-3">
                <button class="solid-button submit" id="btnSaveAddNewTab">
                    Save
                </button>
            </div>
        </form>
    </div>

    <div class="drawer-container" title="Edit Modal" id="Modal_EditTab">
        <form class="container-fluid">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom: 10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>

            <div class="row">
                <div class="col-md-6 input-container">
                    <label for="inputEditTab_TabName" class="required">Tab Name</label>
                    <input id="inputEditTab_TabName" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputEditTab_HTMLTagId" class="required">HTML Tag ID</label>
                    <input id="inputEditTab_HTMLTagId" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputEditTab_DisplaySequence" class="required">Display Sequence</label>
                    <input id="inputEditTab_DisplaySequence" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputEditTab_TabLink" class="required">Tab Link</label>
                    <input id="inputEditTab_TabLink" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputEditTab_HTMLIcon">HTML Icon</label>
                    <input id="inputEditTab_HTMLIcon"/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="selectEditTab_Status" class="required">Status</label>
                    <select id="selectEditTab_Status" required></select>
                </div>
            </div>

            <div class="flex flex-row-reverse mt-3">
                <button class="solid-button submit" id="btnSaveEditTab">
                    Save
                </button>
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/TabSetup") %>

    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>"
        var reqTableTabListUrl = "<%= ResolveUrl("~/api/UAM/getTabList")%>";
        var reqUserStatusUrl = "<%= ResolveUrl("~/api/UAM/getUserStatus") %>";
        var reqAddNewTabUrl = "<%= ResolveUrl("~/api/UAM/postAddTab") %>";
        var reqLoadTabDetailsUrl = "<%= ResolveUrl("~/api/UAM/postTabDetailsByID") %>";
        var reqEditTabUrl = "<%= ResolveUrl("~/api/UAM/postEditTab") %>"

        var hiddenTabID_ID = "<%= hiddenTabID.ClientID%>"
        $(`#${hiddenTabID_ID}`).css('display', 'none');

        $(document).ready(function () {
            loadUserStatus(reqUserStatusUrl, authToken)
            .then(() => {
                loadTableRoleList(reqTableTabListUrl, authToken);
            })

            $("#btnAddNewTab").click(function () {
                var t = "#Modal_AddNewTab";

                Drawer_ResetInput(t);
                Drawer_Show(t);
            })

            $("#Modal_AddNewTab").submit(function (e) {
                e.preventDefault();

                var button = $("#btnSaveAddNewTab");
                var dataStr = {
                    tabName: $("#inputAddNewTab_TabName").val(),
                    htmlID: $("#inputAddNewTab_HTMLTagId").val(),
                    sequence: $("#inputAddNewTab_DisplaySequence").val(),
                    tabLink: $("inputAddNewTab_TabLink").val(),
                    htmlIcon: $("#inputAddNewTab_HTMLIcon").val(),
                    status: $("#selectAddNewTab_Status").val()
                }

                button.addClass('busy');

                saveAddNewTab(reqAddNewTabUrl, authToken, dataStr)
                    .then((data) => {
                        ShowPass('Add New Tab Success', data || 'Add successfully!');
                        Drawer_Hide(this);
                    })
                    .catch((error) => {
                        ShowError('Fail To Add New Tab', error.responseText || error)
                    })
                    .finally(() => {
                        tableTabList.ajax.reload();
                        button.removeClass('busy');
                    })
            });

            $("#Modal_EditTab").submit(function (e) {
                e.preventDefault();

                var button = $("#btnSaveEditTab");
                var dataStr = {
                    moduleID: $(`#${hiddenTabID_ID}`).val(),
                    tabName: $("#inputEditTab_TabName").val(),
                    htmlID: $("#inputEditTab_HTMLTagId").val(),
                    sequence: $("#inputEditTab_DisplaySequence").val(),
                    tabLink: $("#inputEditTab_TabLink").val(),
                    htmlIcon: $("#inputEditTab_HTMLIcon").val(),
                    status: $("#selectEditTab_Status").val()
                }

                console.log(dataStr);

                button.addClass('busy');

                saveEditTab(reqEditTabUrl, authToken, dataStr)
                    .then((data) => {
                        ShowPass('Edit Tab Success', data || 'Edit successfully!');
                        Drawer_Hide(this);
                    })
                    .catch((error) => {
                        ShowError('Fail To Edit Tab', error.ResponseText || error);
                    })
                    .finally(() => {
                        tableTabList.ajax.reload();
                        button.removeClass('busy');
                    })
            });
        })

        $.contextMenu({
            selector: '#Table_TabList tbody tr',
            className: 'css-title-toolbox',
            items: {
                'edit': {
                    name: 'Edit',
                    icon: 'fa-edit',
                    callback: function (key, options) {
                        var targetDrawer = $("#Modal_EditTab");
                        var rowData = tableTabList.row(options.$trigger).data();

                        var dataStr = {
                            moduleID: rowData.ModuleID
                        }

                        $(`#${hiddenTabID_ID}`).val(rowData.ModuleID);

                        Drawer_ResetInput(targetDrawer);
                        Drawer_Show(targetDrawer, true);

                        loadTabDetails(reqLoadTabDetailsUrl, authToken, dataStr)
                            .then(() => {
                                Drawer_Show(targetDrawer, false);
                            })
                            .catch((error) => {
                                Drawer_Hide(targetDrawer);
                                ShowError('Failt To Load Tab Details', error.responseText || error)
                            })
                    }
                }
            }
        })
    </script>
</asp:Content>