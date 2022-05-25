<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaiseRFQ.aspx.cs" Inherits="MRP.Views.PurchasingRFQManagement.RaiseRFQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/RaiseRFQ") %>
    <style>

        .upload-btn-wrapper {
          position: relative;
          overflow: hidden;
          display: inline-block;
        }

        .btn {
          border: 2px solid gray;
          color: gray;
          background-color: white;
          padding: 8px 20px;
          border-radius: 8px;
          font-size: 20px;
          font-weight: bold;
        }

        .upload-btn-wrapper input[type=file] {
          font-size: 100px;
          position: absolute;
          left: 0;
          top: 0;
          opacity: 0;
        }

        .fileBox {
          display: flex;
          flex-wrap:  wrap;
          margin-top: 20px;
        }
        
        .fileBox .addButton {
          flex-basis: 31%;
          margin-bottom: 10px;
          border-radius: 4px;
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
        }

        .attachment2{
        }

        .attachment-upload {
          background-color: #ECECEC;
          width: 600px;
          padding: 20px;
          border-radius: 10px;
          box-shadow: inset -2px -2px 4px rgba(255, 255, 255, 0.45), inset 2px 2px 4px rgba(0, 0, 0, 0.15);
        }

        .attachment-upload-content {
          text-align: center;
        }

        .attachment-upload-input {
          position: relative;
          margin: 0;
          padding: 0;
          width: 100%;
          height: 100%;
          outline: none;
          opacity: 50;
          cursor: pointer;
        }

        .attachment-upload-wrap {
          margin-top: 20px;
          position: relative;
        }

        .image-dropping,
        .attachment-upload-wrap:hover {
          background-color: #1FB264;
          border: 4px dashed #ffffff;
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

        body{
            background-color:#F9F9F9;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex flex-row">
        <a style="cursor:pointer" onclick="goPreviousPage()">
            <i class="fa-solid fa-arrow-left fa-2x" style="color:black"></i>
        </a>
        <p style="font-weight: 500; font-size: 24px;margin-left: 20px">RFQ Details</p>
    </div>
        <form class="container-fluid" name="RFQForm">
            <div style="font-size: .75rem; margin-top: 5px; margin-bottom:10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>
            <hr />
            <div><p style="font-size:20px; font-weight:600;">1 | Basic Information</p></div>

            <div class="row">
                <input type="hidden" id="hdnRFQID" value ="<%= Page.RouteData.Values["ID"] %>"/>
                <div class="col">
                    <div class="input-container">
                        <label for="txtTitle" class="required">RFQ Title</label>
                        <input id="txtTitle" required/>
                    </div>
                    <div class="input-container">
                        <label for="txtWatcher">Watchers Email / CC Email</label>
                        <input id="txtWatchers" />
                    </div>
                    <div class="input-container">
                        <label>File must be less than 2MB<b id="lblFileMessage"></b></label>
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
                            <%--<input class="attachment-upload-input" id="txtFileUpload" type='file' onchange="readURL(this);" multiple="multiple" accept=".png, .jpg, .gif, .jpeg, .pdf, .doc, .docx" />--%>
                            </div>
                        </div>

                     </div>
                    <div>
                        <input type="file" id="test"/>
                    </div>

                    

                        <%--file content wrap--%>
<%--                          <div class="attachment-upload-content">

                            <div class="attachment-title-wrap">
                                <span class="fa-stack fa-2x">
                                     <i class="fa-solid fa-file fa-stack-2x "></i><i class="fa-solid fa-circle-minus fa-2xs fa-stack" style="color:red; position:absolute;bottom:37px; right:-17px;"></i>
                                </span>
                                <span class="attachment-title"></span>
                            </div>

                          </div>--%>

<%--<button type="button" onclick="removeUpload()">Remove <span class="attachment-title">Uploaded File</span></button>--%>
                        <%--<div class="input-group mb-3" id="txtAttachment">
                            <input type="file" class="form-control" aria-describedby="basic-addon2" id="txtFileUpload"
                                accept=".png, .jpg, .gif, .jpeg, .pdf, .doc, .docx" multiple="multiple">
                            
                            <div class="input-group-append">
                                <span class="btn input-group-text" id="btnfileUploadReset" aria-hidden="true">×</span>
                            </div>
                        </div>--%>



                    
                </div>
                </div>
                <div class="col-md-6 input-container">
                    <label for="txtDescription">Description</label>
                    <textarea class="form-control" id="txtDescription" rows="12"></textarea>
                </div>

            <div class="d-flex flex-row justify-content-center" style="margin-top: 15px;">
                 <button class="solid-button reset mx-2" type="button" id="btnResetRFQ">
                    <i class="fa-solid fa-rotate-right"></i>
                    Reset
                </button>
                <button class="solid-button close mx-2" id="btnCancelRFQForm">
                    <i class="fa-solid fa-xmark-large"></i>
                    Cancel
                </button>
                <button class="solid-button normal mx-2" id="btnSubmitRFQDraft">
                    <i class="fa-solid fa-box-archive"></i>
                    Add draft
                </button>
                <button class="solid-button submit mx-2" id="btnSubmitRFQ">
                    <i class="fa-solid fa-check"></i>
                    Submit
                </button>
            </div>
        </form>

    <div class="drawer-container" title="" id="CloseRaiseRFQ">
        <form class="container-fluid">
            <h2>Close</h2>
            <br />
            <br />
            <h6>Are you sure you want to discard the changes or save to draft?</h6>
            <br />
            <div class="d-flex flex-row justify-content-center">
                <button class="solid-button normal mx-1" type="button" id="btnCloseRaiseRFQSaveDraft">
                        Save Draft
                </button>            
                <button class="solid-button reset mx-1" onclick="goPreviousPage()" type="button" id="btnCloseRaiseRFQDiscard">
                        Discard
                </button>
                <button class="solid-button close mx-1" type="button" id="btnCloseRaiseRFQCancel">
                        Cancel
                </button>
            </div>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    
    <%: Scripts.Render("~/scriptBundle/RaiseRFQ") %>
    <script>
    authToken = "<%= Session["accessToken"].ToString() %>";

    reqTableURL = "<%= ResolveUrl("~/api/RFQ/getActiveRFQList") %>";
    reqDraftRFQUrl = "<%= ResolveUrl("~/api/RFQ/getDraftRFQList")%>"
    reqDataUrl = "<%= ResolveUrl("~/api/RFQ/postRFQByID")%>"
    reqAddRFQWithUploadUrl = "<%= ResolveUrl("~/api/RFQ/postAddRFQWithUpload") %>"
    reqAddRFQDraftWithUploadUrl = "<%= ResolveUrl("~/api/RFQ/postAddRFQDraftWithUpload") %>"
    reqEditRFQDraftWithUploadUrl = "<%= ResolveUrl("~/api/RFQ/postEditRFQDraftWithUpload")%>"
    reqSubmitDraftRFQWithUploadUrl = "<%= ResolveUrl("~/api/RFQ/postSubmitDraftRFQWithUpload") %>"
    reqAttachmentUrl = "<%= ResolveUrl("~/api/RFQ/postAttachmentByID") %>"




    </script>
</asp:Content>
