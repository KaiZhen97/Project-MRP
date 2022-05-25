<%@ Page Title="Module Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModuleSetup.aspx.cs" Inherits="MRP.Views.Administrator.ModuleSetup" %>

<%--------------------------------------------------------------------%>
<%------------------------- CSS Style Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ModuleSetup") %>
</asp:Content>

<%--------------------------------------------------------------------%>
<%------------------------- HTML Body Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <div class="_button-container">
        <button class="solid-button normal" id="btnShowAddNewModule" onclick="ShowDrawer_ModuleSetupModal('new')">
            <i class="fa-solid fa-plus"></i>
            Add New Module
        </button>
    </div>

    <table id="ModuleSetupTable">
        <thead>
            <tr>
                <th></th>
                <th></th>
                <th>Parent Tab</th>
                <th>Module Name</th>
                <th>Module Link</th>
                <th>Sequence</th>
                <th>Group Name</th>
                <th>Group Sequence</th>
                <th>Status</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="Dummy Title" id="ModuleSetupModal">
        <form class="container-fluid">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom:10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>

            <div class="row">
                <div class="col-md-6 input-container">
                    <label for="inputModuleName" class="required">Module Name</label>
                    <input id="inputModuleName" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputParentTab" class="required">Parent Tab</label>
                    <select id="inputParentTab" required>
                        <option value="">Select</option>
                    </select>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputHtmlTagId" class="required">HTML Tag ID</label>
                    <input id="inputHtmlTagId" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputHtmlIcon" class="input-label">HTML Icon (FontAwesome6 Class)</label>
                    <input id="inputHtmlIcon"/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputModuleLink" class="input-label required">Module Link</label>
                    <input id="inputModuleLink" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputSequence" class="input-label required">Sequence</label>
                    <input id="inputSequence" required/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputStatus" class="input-label required">Status</label>
                    <select id="inputStatus" required>
                        <option value="">Select</option>
                        <option value="1">Active</option>
                        <option value="0">Inactive</option>
                    </select>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputAllowGrouping" class="input-label">Allow Grouping</label>
                    <input type="checkbox" id="AllowGrouping"/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputGroupName" class="input-label">Group Name</label>
                    <input id="inputGroupName"/>
                </div>

                <div class="col-md-6 input-container">
                    <label for="inputGroupSequence" class="input-label">Group Sequence</label>
                    <input id="inputGroupSequence"/>
                </div>
            </div>

            <div class="d-flex flex-row-reverse" style="margin-top: 15px;">
                <button class="solid-button submit" type="submit">
                    Save
                </button>

                <button class="solid-button close ml-1" type="button" id="btnCloseAddNewModule" onclick="CloseDrawer_ModuleSetupModal()">
                    Close
                </button>
            </div>
        </form>
    </div>
</asp:Content>

<%--------------------------------------------------------------------%>
<%---------------------------- JS Render -----------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ModuleSetup") %>

    <script>
        authToken = "<%= Session["accessToken"].ToString() %>";

        reqTableURL = "<%= ResolveUrl("~/api/UAM/getModuleList") %>";
        reqLoadStatusUrl = "<%= ResolveUrl("~/api/UAM/getModuleStatus") %>"
        reqLoadModuleNameUrl = "<%= ResolveUrl("~/api/UAM/getTabList") %>"
        reqAddNewModuleUrl = "<%= ResolveUrl("~/api/UAM/postAddModule")%>"
        reqEditModuleUrl = "<%= ResolveUrl("~/api/UAM/")%>"

        $(document).ready(function () {
            loadTable(reqTableURL, authToken);
        });
    </script>

</asp:Content>
