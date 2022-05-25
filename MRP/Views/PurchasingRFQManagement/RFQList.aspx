<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RFQList.aspx.cs" Inherits="MRP.Views.RFQManagement.RFQList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/RFQ") %>
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

         #ulDraftList{
             padding:0px;
         }
         #ulDraftList li{
             list-style-type: none;
             height:94px;
             background-color: #D8D8D8;
             border-radius: 5px;
             color: #505050;
             margin-top:10px;
         }
         #ulDraftList li:hover{
             background-color:#7DD0FF;
             color: #FFFFFF;

         }
         #toggleMenu{
            background: #F2F2F2;
            box-shadow: inset 0px 1px 4px rgba(0, 0, 0, 0.15);
            border-radius: 30px;
            border: 1px solid #F2F2F2;
            padding: 10px 60px 15px 60px;
            border-radius: 25px;
            font-size: 18px;
            font-weight: 500;
            color: #C1C1C1;
         }

         a{
            text-decoration:none;
            color:#C1C1C1;
         }
         a:hover {
             color: #0F7E97;
             cursor: pointer;
             
         }

         .highlightText {
             color: #0F7E97;
             border-bottom:1px solid #0F7E97;
         }

         body {
             background-color: #F9F9F9;
         }
    </style>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>

<h4 class="_pageTitle"><%= Page.Title %></h4>
    <h2>RFQ List</h2>

    <div class="d-flex justify-content-center m">
      <div class="d-flex flex-row" id="toggleMenu">
          <div class="mx-4"><a class="category" id="btnPurchasing"><i class="fa-solid fa-cart-shopping"></i>&nbsp Purchasing</a></div>
          <div class="mx-4"><a class="category" id="btnLogistic"><i class="fa-solid fa-truck"></i>&nbsp Logistic</a></div>
      </div>
    </div>

    <div style="display: flex; flex-direction: row; margin-top:20px">
        <button class="solid-button normal" onclick="window.location.href='/Views/PurchasingRFQManagement/RaiseRFQ'" style="margin-right:20px">
            <i class="fa-solid fa-plus"></i>
            Raise RFQ
            </button>
        <button class="solid-button white" id="btnDraftItem">
            <i class="fa-solid fa-envelope-open"></i>
            Draft RFQ
        </button>  
    </div>  
    
    <h6 class="text-muted" style="margin: 20px 0px 20px 0px; font-weight:400">* Right click an item to open tools box</h6>

    <div class="d-flex flex-row">
        <div class="checkContainer">
            <div class="d-flex flex-row pt-2 px-2" style="">
                <input class="_checkbox-cont" type="checkbox" /><p style="padding-left:5px;">Show Closed/Rejected</p>
            </div>
        </div>
        <div class="checkContainer mb-2 mx-2">
            <div class="d-flex flex-row pt-2 px-2">
                <input class="_checkbox-cont" type="checkbox" /><p style="padding-left:5px;">Show My RFQ Only</p>
            </div>
        </div>
    </div>
    
    <table id="tblRFQData">
        <thead>
            <tr>
                <th>RFQNo.</th>
                <th>RFQ Tittle</th>
                <th>RFQ Description</th>
                <th>Status</th>
                <th>Requster</th>
                <th>Purchaser</th>
                <th>Created Date</th>
                <th>Last Update Date</th>
            </tr>
        </thead>
    </table>
    
    <%--draft Modal--%>
    <div class="drawer-container" title="Draft List" id="DraftModal">
        <form class="container-fluid">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom:10px; color:#767676;">* Draft item will permanently delete after 30 days</div>

                
                <input type="text" id="keywordSearch" placeholder="Search" style="border:none;border-bottom:2px solid #E9E9E9; color: #989898; font-size:16px; margin-bottom:10px;">
                
                <ul id="ulDraftList">
                    
                </ul>
        </form>
    </div>
    <div class="drawer-container" title="" id="CancelRFQModal">
        <form class="container-fluid">
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
                <button class="solid-button submit" style="width:100px; margin-right:20px;" type="button" id="btnCancelYes">
                        Yes
                </button>
                <button class="solid-button close" style="width:100px;" type="button" id="btnCancelNo">
                        No
                </button> 
            </div>                  
            
        </form>  
    </div>
        <div class="drawer-container" title="" id="DeleteDraftModal">
        <form class="container-fluid" style="background-color:#F9F9F9;">
            <h2>Delete Draft</h2>
            <br />
            <h4>Are you sure to delete the draft?</h4>
            <br />
            <div class="d-flex flex-row justify-content-end">
                <button class="solid-button submit" style="width:100px;" type="button" id="btnDeleteDraftYes">
                        Yes
                </button>
                <button class="solid-button close" style="width:100px;" type="button" id="btnDeleteDraftNo">
                        No
                </button> 
            </div>                  
            
        </form>  
    </div>
</div>   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">

    <%: Scripts.Render("~/scriptBundle/RFQ") %>
   
    <script>

        authToken = "<%= Session["accessToken"].ToString() %>";

        reqTableURL = "<%= ResolveUrl("~/api/RFQ/getActiveRFQList") %>";
        reqAddSubmitRFQUrl = "<%= ResolveUrl("~/api/RFQ/postAddSubmitRFQ")%>"
        reqAddDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/postAddDraftRFQ")%>"
        reqDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/getDraftRFQList")%>"
        reqDataUrl = "<%= ResolveUrl("~/api/RFQ/postRFQByID")%>"
        reqDeleteUrl = "<%: ResolveUrl("~/api/RFQ/postDeleteRFQ")%>"
        reqDeleteDraftUrl = "<%: ResolveUrl("~/api/RFQ/postDeleteRFQDraft")%>"

    </script>
</asp:Content>
