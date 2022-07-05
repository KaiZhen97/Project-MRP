<%@ Page Title="Item Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.ItemDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/ItemDetails") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex flex-row m-1">
        <h4 class="_pageTitle">
            <a href="/Views/ItemLibraryManagement/ItemLibraryList" style="color: black;">
                <i class="fa-solid fa-arrow-left me-3"></i>
            </a>
            <span><%= Page.Title %></span>
        </h4>
    </div>

    <input type="hidden" id="hdnID" value="<%= Page.RouteData.Values["ID"] %>" />
    <input type="hidden" id="hdnMode" value="<%= Page.RouteData.Values["Mode"] %>" />

    <div class="div-row flexbox m-1">
        <div class="div-row m-1">
            <div class="m-1" id="info">
                <i class="fa-solid fa-circle-exclamation me-2 mb-4"></i>
            </div>
            <div class="m-1 ms-2 me-3" id="seperator"></div>
            <div class="div-col m-1">
                <div class="m-1" id="detailItemDescription" style="font-size: 20px; color: black; font-weight: 600;"></div>
                <div class="m-1" id="detailIPN" style="font-size: 16px; color: #505050;"></div>
                <div class="m-1" style="font-size: 14px; color: #A7A7A7;">
                    <span id="detailCreatedBy">Created by </span>
                    <span id=""> &#9679; Created since <span id="detailCreatedDate"></span> days ago </span>
                    <span id=""> &#9679; Last updated since <span id="detailLastUpdatedDate"></span> days ago</span>
                </div>
            </div>
        </div>
        <div class="div-row m-1" style="align-items: center;">
            <button class="solid-button s normal m-1" id="btnDuplicate"><i class="fa-solid fa-copy ms-2"></i></button>
            <button class="solid-button s normal m-1" id="btnEdit"><i class="fa-solid fa-pen ms-2"></i></button>
            <button class="solid-button s normal m-1" id="btnDelete"><i class="fa-solid fa-trash ms-2"></i></button>
            <button class="solid-button s reset m-1" id="btnPrevious"><i class="fa-solid fa-angle-left ms-2 p-1"></i></button>
            <button class="solid-button s reset m-1" id="btnNext"><i class="fa-solid fa-angle-right ms-2 p-1"></i></button>
        </div>
    </div>

    <hr />

    <h5>1 | Basic Information</h5>
    <div class="input-container">
        <div class="row ms-0 mb-2">
            <label>Item Description <span style="color: red;">*</span></label>
            <input id="inputItemDescription" style="width: 99%" disabled/>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">Category <span style="color: red;">*</span></label>
                    <select id="inputCategory" style="width: 95%;" disabled>

                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Internal Part Number &nbsp; <i class="fa-solid fa-lock"></i> </label>
                    <input id="inputIPN" style="width: 95%; background:#D2D2D2" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Manufacturer</label>
                    <input id="inputManufacturer" style="width: 95%" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Manufacturer Part Number</label>
                    <input id="inputMPN" style="width: 95%" disabled/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">Tariff / HS Code</label>
                    <input id="inputTariff" style="width: 95%;" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Serial Number</label>
                    <select id="inputRequiredSN" style="width: 95%;" disabled>
                        <option value="1">Yes</option>
                        <option value="0" selected>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Calibration</label>
                    <select id="inputRequiredCalibration" style="width: 95%;" disabled>
                        <option value="1">Yes</option>
                        <option value="0" selected>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Is Default</label>
                    <select id="inputIsDefault" style="width: 95%;" disabled>
                        <option value="1">Yes</option>
                        <option value="0" selected>No</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">More Details</label>
                    <textarea id="inputMoreDetails" style="width: 49%; height: 120px" disabled></textarea>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h5>2 | Pricing & Shipping</h5>
    <div class="input-container">
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label>Supplier <span style="color: red;">*</span></label>
                    <select id="inputSupplierName" style="width: 95%;" disabled>

                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Delivery Term</label>
                    <input id="inputDeliveryTerm" style="width: 95%" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Quotation Date</label>
                    <input type="date" id="inputQuotationDate" style="width: 95%;" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label>Quotation Validity</label>
                    <input id="inputQuotationValidity" style="width: 95%" disabled/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">UOM <span style="color: red;">*</span></label>
                    <input id="inputUOM" style="width: 95%" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Unit Price <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="0.01" id="inputUnitPrice" style="width: 95%" placeholder="RM" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Unit Price Discount</label>
                    <input type="number" min="0" step="0.01" id="inputUnitPriceDiscount" style="width: 95%" placeholder="RM" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Final Unit Price</label>
                    <input type="number" min="0" step="0.01" id="inputFinalUnitPrice" style="width: 95%" placeholder="RM" disabled/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2" style="width: 50%;">
            <div class="col">
                <div class="row">
                    <label class="label">Minimum Amount Per Order <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="any" id="inputMinAmountPerOrder" style="width: 95%;" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Standard LeadTime</label>
                    <input type="number" min="0" id="inputStdLeadTime" style="width: 96%;" placeholder:"W.Weeks" disabled/>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h5>3 | Other Attachments</h5>
    <div class="input-container">
        <div class="row">
            <div class="col">
                <div class="row ms-0 mb-3">
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="input-container label">Default Purchaser / Agent 1 </label>
                            <select id="inputPurchaser1" disabled>
                                <%--<option selected hidden disabled>select</option>--%>
                            </select>
                        </div>
                    </div>
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="label">Default Purchaser / Agent 2 </label>
                            <select id="inputPurchaser2" disabled>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="row ms-0">
                    <label>Remark</label>
                    <textarea id="inputRemark" style="width: 98%; height: 120px" disabled></textarea>
                </div>
            </div>

            <div class="col">
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label>Attachments</label>
                    <input type="file" id="inputAttachments" style="width: 99%; height: 90px;" disabled/>
                    <%--<i class="fa-solid fa-file"></i>--%>
                </div>
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label>Confidential Attachments</label>
                    <input type="file" id="inputConfidential" style="width: 99%; height: 90px" disabled/>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h5>4 | Key Tech Specification</h5>
    <div class="box">

        <button id="btnAdd" style="background: #BCBCBC; width: 50%;"><i class="fa-solid fa-plus"></i></button>
    </div>

    <hr />

    <h5>5 | PWP Bundle</h5>
    <table id="PWP_Bundle">
        <thead>
            <tr>
                <th>IPN</th>
                <th>Item Desc</th>
                <th>Unit Price</th>
                <th>PWP U/Discount</th>
                <th>PWP .F.U/Price</th>
            </tr>
        </thead>
    </table>

    <br />
    <br />

    <h5>6 | History Trace</h5>
    <table id="Table_HistoryTrace">
        <thead>
            <tr>
                <th>Time Stamp</th>
                <th>Update By</th>
                <th>Status</th>
                <th>Event Description</th>
                <th>Remark</th>
            </tr>
        </thead>
    </table>

    <br />
    <br />

     <div class="modal fade" id="deleteItem" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Item</h5>
                    <button class="btn-close""></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete<span>Item Description</span></h6>
                        <label>Remark</label>
                        <input id="DeleteItemRemark" />
                    </div>
                </div>
                <div class="flex flex-row m-2 end" style="justify-content: flex-end">
                    <button class="solid-button submit m-2" id="btnYesDeleteItem" style="color: white" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoDeleteItem">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/ItemDetails") %>

    <script>

        var authToken = "<%= Session["accessToken"].ToString() %>";

        var reqTableItemLibraryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveItemLibraryList") %>";
        var reqItemLibraryIdUrl = "<%= ResolveUrl("~/api/ItemLibrary/postItemLibraryByID")%>";
        var reqAddItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddItemLibrary")%>";
        var reqEditItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postEditItemLibrary")%>";
        var reqDeleteItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteItemLibrary")%>";
        var reqAddDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddDraftItem")%>";
        var reqSaveDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postSaveDraftItem")%>";
        var reqDeleteDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteDraftItem")%>";

        var reqPurchasingListUrl = "<%= ResolveUrl("~/api/User/getPurchasingList")%>";

        var reqUserNameUrl = "<%= ResolveUrl("~/api/ItemLibrary/getUserNameList")%>";

        var reqPurchaserListUrl = "<%= ResolveUrl("~/api/User/getPurchaserList") %>";
        var reqCategoryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveCategoryList")%>";
        var reqSupplierListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getSupplierNameList")%>";

    </script>
</asp:Content>
