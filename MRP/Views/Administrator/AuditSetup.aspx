<%@ Page Title="Audit Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditSetup.aspx.cs" Inherits="MRP.Views.Administrator.AuditSetup" %>

<%--------------------------------------------------------------------%>
<%------------------------- CSS Style Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/AuditSetup") %>
</asp:Content>

<%--------------------------------------------------------------------%>
<%------------------------- HTML Body Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4 class="_pageTitle"><%= Page.Title %></h4>

    <form runat="server">
        <asp:HiddenField ID="antiforgery" runat="server" />
        <asp:HiddenField ID="hiddenChkBoxValue" runat="server" />

        <div class="_table-wrapper">
            <h4 class="_table-title">Audit Table</h4>
            <table>
                <thead>
                    <tr>
                        <th>
                            <div class="_checkbox-cont">
                                <input type="checkbox" id="SelectAll"/>
                                <i class="fa-solid fa-check _checkbox-checkmark"></i>
                            </div>

                        </th>
                        <th>Select All</th>
                    </tr>
                </thead>
                <tbody runat="server" id="tableBody_AuditTable">
                    <tr>
                        <td>
                            <input type="checkbox"/>
                        </td>tableBody_AuditTable
                        <td>Dummy Item 1</td>
                    </tr>

                    <tr>
                        <td><input type="checkbox"/></td>
                        <td>Dummy Item 2</td>
                    </tr>

                    <tr>
                        <td><input type="checkbox"/></td>
                        <td>Dummy Item 3</td>
                    </tr>

                    <tr>
                        <td><input type="checkbox"/></td>
                        <td>Dummy Item 4</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="flex flex-row-reverse" style="margin-top: 25px;">
            <button id="btnSave" class="solid-button submit button" runat="server" onserverclick="btnSave_Click">Save</button>
            <button id="btnReset" class="solid-button reset button" style="margin-right: 15px;">Reset</button>
        </div>
    </form>
</asp:Content>

<%--------------------------------------------------------------------%>
<%---------------------------- JS Render -----------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ContentPlaceHolderID="JsContent" runat="server">
    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>";
        var reqListUrl = "<%= ResolveUrl("~/api/Audit/getSelectedTableNameList") %>";
        var hiddenChkBoxID = "<%= hiddenChkBoxValue.ClientID%>";

        $("#<%= hiddenChkBoxValue.ClientID%>").css("display", "none");
    </script>

    <%: Scripts.Render("~/scriptBundle/AuditSetup") %>
</asp:Content>
