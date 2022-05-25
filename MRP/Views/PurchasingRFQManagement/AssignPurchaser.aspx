<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignPurchaser.aspx.cs" Inherits="MRP.Views.PurchasingRFQManagement.AssignPurchaser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/AssignPurchaser") %>
    <style>
        a, a:hover, a:focus, a:active {
            text-decoration: none;
            color: inherit;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h4 class="_pageTitle"><%= Page.Title %></h4>
    <h2 style="margin-bottom:30px">Assign Purchaser</h2>
    
    <table id="tblAssignPurchaser">
        <thead>
            <tr>
                <th><input type="checkbox" id="checkAll" /> </th>
                <th>RFQ No.</th>
                <th>RFQ Tittle</th>
                <th>RFQ Description</th>
                <th>Status</th>
                <th>Requester</th>
                <th>Created Date</th>
                <th>Purchaser</th>
                <th>Purchaser 2</th>
            </tr>
        </thead>
    </table>
    <div class="d-flex flex-row justify-content-center" >
        <button class="solid-button reset mx-3" id="btnResetField" style="width:165px"><i class="fa-solid fa-rotate-right"></i>Reset</button>
        <button class="solid-button submit" id="btnSubmitPurchaser" style="width:165px"><i class="fa-solid fa-paper-plane-top"></i>Submit</button>
    </div>
    
    <%--Assign confirmation Modal--%>
    <div class="drawer-container" title="Assign Purchaser" id="AssignConfirmModal">
        <form class="container-fluid" style="background-color:#F9F9F9;" id="AssignmentForm">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom:10px;">* Draft item will permanently delete after 30 days</div>
            <h2>Assign Purchaser</h2>
            <br />
            <h5>Are you confirm to assign purchaser for the selected item?</h5>
            <br />
            <div class="input-container">
                <label for="txtRemarks">Remarks</label>
                 <input type="text" id="txtRemarks" placeholder="Remarks">
            </div>
            <div class="d-flex flex-row">
                <button class="solid-button normal ml-1" type="submit" id="btnConfirmAssign">
                        Yes
                </button>            
                <button class="solid-button close ml-1" style="width:200px" type="button" id="btnCloseModal">
                        No
                </button>
            </div>
        </form>
    </div>  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/AssignPurchaser") %>
    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>";
        var reqTableUrl = "<%= ResolveUrl("~/api/RFQ/getPendingAssigmentList") %>";
        var reqUserList = "<%= ResolveUrl("~/api/User/getUserList") %>";
        var reqSubmitPurchaserUrl = "<%= ResolveUrl("~/api/RFQ/postSubmitAssignmentRFQ")%>"
        var reqAssignedPurchaser = "<%= ResolveUrl("~/api/RFQ/getAssignedPurchaser") %>"

    </script>

</asp:Content>
