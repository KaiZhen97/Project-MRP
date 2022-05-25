<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RFQDetails.aspx.cs" Inherits="MRP.Views.PurchasingRFQManagement.RFQDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/RFQDetails") %>
    <style>
        a:link { text-decoration:none; }
        a:visited { text-decoration: none; }
        a:hover { text-decoration: none; }
        a:active { text-decoration: none; }
        .box{
            background: #FFFFFF;
            border: 1px solid #BEBEBE;
            border-radius: 10px;
            margin-top:20px; 
        }
        .full{
            -ms-flex: 1;  /* IE 10 */  
            flex: 1;
        }

        input {
            line-height: 2em;
            width:70%;
        }

        .sectionTitle {
            font-weight: 600;
            font-size: 18px;
            line-height: 27px;
        }

        body{
            background-color:#F9F9F9;
        }

        ul:before{
            content:attr(data-header);
            font-size:120%;
            font-weight:bold;        
        }

        ul {
            list-style:none;
            padding:15px 0px 0px 0px; 
        }

        #ulBasicInfo span{
            word-break: break-word;
            word-wrap:break-word;
            white-space: normal;
        }

        .fileBox {
          display: flex;
          flex-wrap:  wrap;
          margin-top: 20px;
        }  

        .fileBox .addButton {
          background-color: #F5F7FA;
          align-self: center;
          text-align: center;
          padding: 40px 0;
          text-transform: uppercase;
          color: #848EA1;
          font-size: 12px;
          cursor: pointer;
          flex-basis: 31%;
          margin-bottom: 10px;
          border-radius: 4px;
        }

        .attachment2{
        }

        .attachment-upload {
          background-color: #ECECEC;
          width: 70%;
          padding: 20px;
          border-radius: 10px;
          box-shadow: inset -2px -2px 4px rgba(255, 255, 255, 0.45), inset 2px 2px 4px rgba(0, 0, 0, 0.15);
        }

        .attachment-upload-content {
          text-align: center;
        }

        .attachment-title-wrap {
          padding: 5px;
          width:100px;
          /*overflow: hidden;
          text-overflow: ellipsis;*/
          border:1px dashed black;
          border-radius:10px;
          color: #222;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex flex-column p-3">
        <div class="d-flex flex-row">
            <a href="/Views/PurchasingRFQManagement/RFQList">
                <i class="fa-solid fa-arrow-left fa-2x" style="color:black"></i>
            </a>
            <p style="font-weight: 500; font-size: 24px;margin-left: 20px">RFQ Details</p>
        </div>
        <div class="d-flex flex-row mt-4">
            <input type="hidden" id="hdnRFQID" value ="<%= Page.RouteData.Values["ID"] %>"/>
            <i class="fa-solid fa-circle-exclamation fa-3x align-self-center"></i>
            <div class="mx-4" style="border-left:2px solid grey;"></div>
            <div class="d-flex flex-column">
                <p class="sectionTitle" style="margin-bottom:7px">Request For Quotation</p>
                    <p style="margin-bottom:7px" id="txtRFQNo"></p>
                    <p id="txtRaisedBy"></p>
            </div>
            <div class="d-flex justify-content-end full m-3" >
                <button class="solid-button normal justify-content-center" style="height:45px; width:45px;"><i class="fa-solid fa-arrow-up-from-bracket"></i></button>
                <button class="solid-button normal justify-content-center mx-3" style="height:45px; width:45px"><i class="fa-solid fa-circle-xmark"></i></button>

                <div class="d-flex flex-row p-2" style="border:1px solid #BEBEBE; box-sizing: border-box; border-top-left-radius: 10px; border-bottom-left-radius:10px; height:45px; background: #FFFFFF;">
                    <p>99 Watchers </p>   
                </div>
                <div class="p-2" style="border:1px solid #BEBEBE; box-sizing: border-box; border-bottom-right-radius: 10px; border-top-right-radius:10px; height: 45px; background: #FFFFFF;" id="addWatcherIcon">
                    <a style="height:45px; width:45px"><i class="fa-solid fa-user-plus"></i></a>
                </div>
            </div>
            
        </div>
        <div class="d-flex flex-row">
            <div class="flex-column col-8">
                <div class="box p-3">
                    <p class="sectionTitle">Description</p>
                    <hr />  
                    <input type="hidden" id="txtTitle"/>
                    <p id="txtDescription"></p>
                </div>
                <div class="box p-3" style="overflow:scroll;">
                    <p class="sectionTitle">Update History</p>
                    <hr />
                    <table id="tblRFQUpdateHistory">
                        <thead>
                            <tr>
                                <th>Time Stamp</th>
                                <th>Updated By</th>
                                <th>Event Description</th>
                                <th>Remark</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="d-flex flex-column box p-3">
                    <p class="sectionTitle">Update</p>
                    <hr />
                    <div class="d-flex flex-row ">
                        <div class="flex-column full">

                                   
                            <div class="d-flex flex-row justify-content-between input-container" >
                                <label style="font-weight: 400;font-size: 18px;" for="selStatus">Status</label>
                                <select style="width:70%" id="selStatus" name="selStatus">
                                    <option value="">select..</option>
                                    <option value="1">On Going</option>
                                    <option value="1">Completed</option>
                                </select>
                            </div>

                            <div class="d-flex flex-row justify-content-between input-container">
                                <label style="font-weight: 400;font-size: 18px;" for="txtRemark">Remark</label>
                                <textarea class="input-container" style="width:70%" id="txtRemark" rows="12"></textarea>
                            </div>

                            <%--<div class=" d-flex flex-row  justify-content-between input-container">
                                <label style="font-weight: 400;font-size: 18px;" for="txtAttachment">Attachment</label>
                                <input id="txtAttachment" /> 
                            </div>--%>

                            <div class="d-flex flex-row  justify-content-between input-container">
                                <label style="font-weight: 400;font-size: 18px;" for="txtAttachment">Attachment</label>
                                <div class="attachment-upload">
                                     <%--file upload wrap--%>
                                    <div class="attachment-upload-content">
                                        <p style="font-size:30px;">Drag File or Click Here To Upload</p>
                                        <div class="fileBox">
                                            <div class="fileContainer">

                                            </div>
                                            <div class="addButton">
                                                add
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="d-flex flex-row justify-content-center" >
                                <button class="solid-button reset mx-3" id="btnResetField" style="width:165px">Reset</button>
                                <button class="solid-button submit" id="btnConfirmEdit" style="width:165px">Confirm</button>
                            </div>                       
                          </div>
                        
                    </div>
                </div>
            </div>
            <div class="d-flex flex-column full mx-3">
                <div class="box">
                    <ul data-header="&nbsp;&nbsp;&nbsp;  Basic Information" id="ulBasicInfo"> 
                         
                    </ul>
                </div>
                <div class="box">
                    <ul data-header="&nbsp;&nbsp;&nbsp;Attachments" id="ulAttachments"> 
<%--                        <li>
                            <div class="d-flex flex-column text-right" style="padding: 15px 0px 10px 15px;border-top:solid 1px #BEBEBE;">
                                <p style="color:#333333; font-weight: 400; font-size: 16px;">Quotation.pdf</p>
                                <p style="color:#8A8A8A; font-weight: 400; font-size: 14px;">Name | 12 Dec 2021 00:00</p>
                            </div>
                        </li>
                       <li>
                            <div class="d-flex flex-column text-right" style="padding: 15px 0px 10px 15px;border-top:solid 1px #BEBEBE;">
                                <p style="color:#333333; font-weight: 400; font-size: 16px;">Quotation.pdf</p>
                                <p style="color:#8A8A8A; font-weight: 400; font-size: 14px;">Name | 12 Dec 2021 00:00</p>
                            </div>
                        </li>
                        <li>
                            <div class="d-flex flex-column text-right" style="padding: 15px 0px 10px 15px;border-top:solid 1px #BEBEBE;">
                                <p style="color:#333333; font-weight: 400; font-size: 16px;">Quotation.pdf</p>
                                <p style="color:#8A8A8A; font-weight: 400; font-size: 14px;">Name | 12 Dec 2021 00:00</p>
                            </div>
                        </li>
                        <li>
                            <div class="d-flex flex-column text-right" style="padding: 15px 0px 10px 15px;border-top:solid 1px #BEBEBE;">
                                <p style="color:#333333; font-weight: 400; font-size: 16px;">Quotation.pdf</p>
                                <p style="color:#8A8A8A; font-weight: 400; font-size: 14px;">Name | 12 Dec 2021 00:00</p>
                            </div>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>
        <div class="drawer-container" title="Watchers" id="watcherModal">
        <form class="container-fluid input-container">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom:10px; color:#767676;">Please enter Watcher email</div>
            
            <div class="d-flex">
                <input type="text" id="txtWatcher" class="input-container"><button class="solid-button normal" id="btnAddWatcher"><i class="fa-solid fa-plus"></i>Add</button>
            </div>
                
                
                <ul id="ulWatcherList">

                    
                </ul>
            
        </form>
    </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
      
    <%: Scripts.Render("~/scriptBundle/RFQDetails") %>
    <script>
    authToken = "<%= Session["accessToken"].ToString() %>";

    reqTableUrl = "<%= ResolveUrl("~/api/RFQ/postUpdateHistoryTrace") %>";
    reqAddSubmitRFQUrl = "<%= ResolveUrl("~/api/RFQ/postAddSubmitRFQ")%>"
    reqAddDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/postAddDraftRFQ")%>"
    reqEditDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/postEditRFQ")%>"
    reqEditRFQUrl = "<%= ResolveUrl("~/api/RFQ/postEditRFQ")%>"
    reqSubmitDraftRFQUrl = "<%=ResolveUrl("~/api/RFQ/postSubmitDraftRFQ")%>"
    reqDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/getDraftRFQList")%>"
    reqDataUrl = "<%= ResolveUrl("~/api/RFQ/postRFQByID")%>"
    reqAttachmentUrl = "<%= ResolveUrl("~/api/RFQ/postAttachmentByID")%>"
    reqEditRFQWithUploadUrl = "<%= ResolveUrl("~/api/RFQ/postEditRFQWithUpload") %>"
    reqAddNewWatcher = "<%= ResolveUrl("~/api/RFQ/PostAddWatcher") %>"
    reqWatcherEmailUrl = "<%= ResolveUrl("~/api/RFQ/postWatcherEmailByID") %>"



    </script>
</asp:Content>
