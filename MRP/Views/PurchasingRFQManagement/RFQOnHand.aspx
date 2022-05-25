<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RFQOnHand.aspx.cs" Inherits="MRP.Views.PurchasingRFQManagement.RFQOnHand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
     <%: Styles.Render("~/styleBundle/RFQOnHand") %>
    <style>
        .noteText{
            font-weight: 400;
            font-size: 12px;
            color: #767676;
        }

        .checkContainer{
            border: 1px solid #BBBBBB;
            border-radius: 4px;
            height:40px;
            display: flex; 
            flex-direction: row;
        }
         .flexrow{
            display: flex; 
            flex-direction: row;
            align-items: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css">

<!-- jQuery library -->
<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>

<!-- Popper JS -->
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>


<h4 class="_pageTitle"><%= Page.Title %></h4>
    <h2>RFQ On Hand</h2>
    <br />
    <h6 class="text-muted">* Right click an item to open tools box</h6>
    <br />
    <div class="d-flex flex-row">
        <div class="checkContainer mr-2 mb-2">
            <div class="d-flex flex-row pt-2 pl-3 pr-3" style="">
                <input class="_checkbox-cont" type="button" /><p style="padding-left:5px;">Show Closed/Rejected</p>
            </div>
        </div>
    </div>
    
    <table id="tblRFQOnHandData">
        <thead>
            <tr>
                <th>RFQNo.</th>
                <th>RFQ Tittle</th>
                <th>RFQ Description</th>
                <th>Status</th>
                <th>Requester</th>
                <th>Purchaser</th>
                <th>Created Date</th>
                <th>Last Update Date</th>
            </tr>
        </thead>
    </table>

    <div class="drawer-container" title="" id="CancelRFQModal">
        <form class="container-fluid" style="background-color:#F9F9F9;">
            <h2>Cancel RFQ</h2>
            <br />
            <br />
            <h5 id="txtRFQNo"></h5>
            <br />
            <div class="input-container">
                <label for="txtRemarks">Remarks</label>
                 <input type="text" id="txtRemarks" placeholder="Remarks">
            </div>
            <div class="d-flex flex-row justify-content-end">
                <button class="solid-button submit" style="width:100px;" type="button" id="btnCancelYes">
                        Yes
                </button>
                <button class="solid-button close" style="width:100px;" type="button" id="btnCancelNo">
                        No
                </button> 
            </div>                  
            
        </form>  
    </div>
    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    
    <%: Scripts.Render("~/scriptBundle/RFQOnHand") %>
    <script>

        authToken = "<%= Session["accessToken"].ToString() %>";

        reqTableURL = "<%= ResolveUrl("~/api/RFQ/getRFQOnHandList") %>";
        reqDataUrl = "<%= ResolveUrl("~/api/RFQ/postRFQByID")%>"
        reqDeleteUrl = "<%: ResolveUrl("~/api/RFQ/postDeleteRFQ")%>"
    </script>
</asp:Content>
