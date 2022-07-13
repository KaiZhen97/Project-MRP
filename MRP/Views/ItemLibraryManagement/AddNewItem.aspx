<%@ Page Title="Add New Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewItem.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.AddNewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/AddNewItem") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex flex-row m-1">
        <h4 class="_pageTitle">
            <a href="/Views/ItemLibraryManagement/ItemLibraryList" style="color: black;">
                <i class="fa-solid fa-arrow-left me-3"></i>
            </a>
            <span id="txtTitle">Add New Item</span>
        </h4>
    </div>

    <p> * Field with " <span style="color: red;">*</span> " is compulsory  * New Item with existed IPN will replace "Key Tech Spec" value</p>
    <hr />

    <input type="hidden" id="hdnID" value="<%= Page.RouteData.Values["ID"] %>" />
    <input type="hidden" id="hdnMode" value="<%= Page.RouteData.Values["Mode"] %>" />

    <h5>1 | Basic Information</h5>
    <br />
    <div class="input-container">
        <div class="row ms-0 mb-2">
            <label>Item Description <span style="color: red;">*</span></label>
            <input id="inputItemDescription" style="width: 99%" required/>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">Category <span style="color: red;">*</span></label>
                    <select id="inputCategory" style="width: 95%;" required>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label" id="labelIPN">Internal Part Number &nbsp; <i class="fa-solid fa-lock" id="lock"></i> </label>
                    <input id="inputIPN" style="width: 95%; background:#D2D2D2" disabled/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Manufacturer</label>
                    <input id="inputManufacturer" style="width: 95%" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Manufacturer Part Number</label>
                    <input id="inputMPN" style="width: 95%" required/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">Tariff / HS Code</label>
                    <input id="inputTariff" style="width: 95%;" />
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Serial Number</label>
                    <select id="inputRequiredSN" style="width: 95%;">
                        <option value="1">Yes</option>
                        <option value="0" selected>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Calibration</label>
                    <select id="inputRequiredCalibration" style="width: 95%;">
                        <option value="1">Yes</option>
                        <option value="0" selected>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Is Default</label>
                    <select id="inputIsDefault" style="width: 95%;" required>
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
                    <textarea id="inputMoreDetails" style="width: 49%; height: 120px"></textarea>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <div class="flex flex-row">
        <div class="flex flex-col me-5">
            <h5>2 | Supplier & Pricing</h5> 
        </div>
        <div class="flex flex-col">
            <button class="solid-button normal p-0 ps-3 pe-3" style="color: black; font-size: 15px; padding: 5px" id="btnAddDefault">
                <span class="ms-1 me-2" style="font-size: 20px; font-weight: 600"> + </span>
                <span class="me-1" style="font-weight: 500">
                    ADD
                </span>
            </button>
        </div>
    </div>
    <br />

    <div class="input-container tab">
        <span class="center p-3" style="background-color: #E5C860;  border-radius: 5px 5px 0px 0px; font-weight: 500">D E F A U L T</span>
        <br />
        <div class="row ms-3 mb-3" style="width: 99%">
            <div class="col me-1">
                <div class="row">
                    <label class="label">Supplier <span style="color: red;">*</span></label>
                    <select id="inputSupplierNameD" style="width: 95%;" required>
                    </select>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Delivery Term</label>
                    <input id="inputDeliveryTermD" style="width: 95%"/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Quotation Date</label>
                    <input type="date" id="inputQuotationDateD" style="width: 95%;"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Quotation Validity</label>
                    <input id="inputQuotationValidityD" style="width: 95%"/>
                </div>
            </div>
        </div>
        <div class="row ms-3 mb-3" style="width: 99%">
            <div class="col me-1">
                <div class="row">
                    <label class="label">UOM <span style="color: red;">*</span></label>
                    <input id="inputUOMD" style="width: 95%" required/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Unit Price <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="0.01" id="inputUnitPriceD" style="width: 95%" placeholder="RM" required/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Unit Price Discount</label>
                    <input type="number" min="0" step="0.01" id="inputUnitPriceDiscountD" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Final Unit Price</label>
                    <input type="number" min="0" step="0.01" id="inputFinalUnitPriceD" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
        </div>
        <div class="row ms-3 mb-3" style="width: 49%;">
            <div class="col me-1">
                <div class="row">
                    <label class="label">Minimum Amount Per Order <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="any" id="inputMinAmountPerOrderD" style="width: 95%;" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Standard LeadTime</label>
                    <input type="number" min="0" id="inputStdLeadTimeD" style="width: 96%;" placeholder:"W.Weeks"/>
                </div>
            </div>
        </div>
    </div>

    <br />

    <div class="input-container tab" style="">
        <span class="center p-3" style="background-color: #E6E6E6;  border-radius: 5px 5px 0px 0px; font-weight: 500"> A L T E R N A T I V E </span>
        <br />
        <div class="row ms-3 mb-3" style="width: 99%">
            <div class="col me-1">
                <div class="row">
                    <label class="label">Supplier <span style="color: red;">*</span></label>
                    <select id="inputSupplierName" style="width: 95%;" required>
                    </select>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Delivery Term</label>
                    <input id="inputDeliveryTerm" style="width: 95%"/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Quotation Date</label>
                    <input type="date" id="inputQuotationDate" style="width: 95%;"/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Quotation Validity</label>
                    <input id="inputQuotationValidity" style="width: 95%"/>
                </div>
            </div>
        </div>
        <div class="row ms-3 mb-3" style="width: 99%">
            <div class="col me-1">
                <div class="row">
                    <label class="label">UOM <span style="color: red;">*</span></label>
                    <input id="inputUOM" style="width: 95%" required/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Unit Price <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="0.01" id="inputUnitPrice" style="width: 95%" placeholder="RM" required/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Unit Price Discount</label>
                    <input type="number" min="0" step="0.01" id="inputUnitPriceDiscount" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Final Unit Price</label>
                    <input type="number" min="0" step="0.01" id="inputFinalUnitPrice" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
        </div>
        <div class="row ms-3 mb-3" style="width: 49.5%;">
            <div class="col me-1">
                <div class="row">
                    <label class="label">Minimum Amount Per Order <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="any" id="inputMinAmountPerOrder" style="width: 95%;" required/>
                </div>
            </div>
            <div class="col me-1">
                <div class="row">
                    <label class="label">Standard LeadTime</label>
                    <input type="number" min="0" id="inputStdLeadTime" style="width: 96%;" placeholder:"W.Weeks"/>
                </div>
            </div>
        </div>
    </div>

    <br />

    <hr />

    <h5>3 | Other Attachments</h5>
    <br />
    <div class="input-container">
        <div class="row">
            <div class="col">
                <div class="row ms-0 mb-3">
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="input-container label">Default Purchaser / Agent 1 </label>
                            <select id="inputPurchaser1" required>
                            </select>
                        </div>
                    </div>
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="label">Default Purchaser / Agent 2 </label>
                            <select id="inputPurchaser2" required>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="row ms-0">
                    <label class="label">Remark</label>
                    <textarea id="inputRemark" style="width: 98%; height: 120px"></textarea> 
                </div>
            </div>

            <div class="col">
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label class="label">Attachments</label>
                    <input type="file" id="inputAttachments" style="width: 99%; height: 90px;"/>
                </div>
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label class="label">Confidential Attachments</label>
                    <input type="file" id="inputConfidential" style="width: 99%; height: 90px" />
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h5>4 | Key Tech Specification</h5>
    <br />
    <div class="box">
        <div class="container-fluid ms-2">
            <div class="row" id="KeyTechSpec">

            </div>
        </div>
        <div class="row ms-2 mt-1">
            <button id="btnAdd" style="background: #BCBCBC; border: 1px solid #B2B2B2; width: 48%;"><i class="fa-solid fa-plus"></i></button>
        </div>
    </div>

    <hr />

    <h5>5 | PWP Bundle</h5>
    
    <button class="" id="btnAddPWPItem"><i class="fa-solid fa-plus me-2"></i> Add New</button>
    <button class="" id="btnRemovePWPItem"><i class="fa-solid fa-minus me-2"></i> Remove</button>

    <br />
    
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

    <br /><br />
    <div class="flex flex-row mt-3 center">
        <button class="solid-button reset me-3" id="btnReset">
            <i class="fa-solid fa-rotate-right"></i>
            Reset
        </button>
        <button class="solid-button close me-3" id="btnClose" data-bs-toggle="modal" data-bs-target="#Close">
            <i class="fa-solid fa-xmark"></i>
            Close
        </button>
        <button class="solid-button normal me-3" style="color: white" id="btnSaveDraft">
            <i class="fa-solid fa-inbox"></i>
            Save Draft
        </button>
        <button class="solid-button submit me-3" id="btnConfirm">
            <i class="fa-solid fa-check"></i>
            Confirm
        </button>
    </div>

    <div class="modal fade" id="ExistAdd" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Item - Found Existed IPN</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <p>Found IPN already exists in system, continue adding will replace some value of existing item</p>
                        <p>Are you sure want to proceed?</p>
                    </div>
                </div>
                <div class="flex flex-row end">
                    <button class="solid-button submit m-2" id="btnYesExistAdd" style="color: white;" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoExistAdd">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ExistEdit" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Item - Found Existed IPN</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <p>Found IPN already exists in system, continue adding will replace some value of existing item</p>
                        <p>Are you sure want to proceed?</p>
                        <label class="ms-1">Remark</label>
                        <input id="ExistEditRemark" />
                    </div>
                </div>
                <div class="flex flex-row end">
                    <button class="solid-button submit m-2" id="btnYesExistEdit" style="color: white;" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoExistEdit">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ConfirmEdit" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Item</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <p>Are you sure confirm to edit the item?</p>
                        <label class="ms-1">Remark</label>
                        <input id="ConfirmEditRemark" />
                    </div>
                </div>
                <div class="flex flex-row end">
                    <button class="solid-button submit m-2" id="btnYesConfirmEdit" style="color: white;" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoConfirmEdit">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="Close" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Close</h5>
                    <button class="btn-close""></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <p></p>
                        <p>Are you sure want to discard the changes or save to draft?</p>
                    </div>
                </div>
                <div class="flex flex-row end">
                    <button class="solid-button normal m-2" style="color: white" id="btnSaveDraftClose" onclick="">
                        Save Draft
                    </button>
                    <button class="solid-button reset m-2" id="btnDiscardClose" onclick="">
                        Discard
                    </button>
                    <button class="solid-button close m-2" id="btnCancelClose" onclick="">
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="drawer-container" title="Add PWP Item From Library" id="AddPWPItem">
        <div class="center">
            1 Filter Items  >  2 Select Items
        </div>
        <br /><br />
        <div class="input-container center">
            <label style="margin-right: 30%;">Category</label>
            <select id="inputCategoryPWP" style="width: 40%">
                <option>1</option>
                <option>2</option>
            </select>
        </div>
        <div class="input-container center">
            <label style="margin-right: 25%">Internal Part Number</label>
            <input id="inputIPNPWP" style="width: 40%"/>
        </div>
        <div class="input-container center">
            <label style="margin-right: 22%">Manufacturer Part Number</label>
            <input id="inputMPNPWP" style="width: 40%"/>
        </div>
        <br />
        <div class="flex flex-row center">
            <button class="solid-button submit center">
                <i class="fa-solid fa-check-double"></i>
                Next
            </button>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/AddNewItem") %>
    
    <script>

        var authToken = "<%= Session["accessToken"].ToString() %>";
        var reqItemLibraryIdUrl = "<%= ResolveUrl("~/api/ItemLibrary/postItemLibraryByID")%>";

        var reqAddItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddItemLibrary")%>";
        var reqEditItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postEditItemLibrary")%>";
        var reqDeleteItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteItemLibrary")%>";
        var reqAddDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddDraftItem")%>";
        var reqSaveDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postSaveDraftItem")%>";
        var reqDeleteDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteDraftItem")%>";

        var reqPurchaserListUrl = "<%= ResolveUrl("~/api/User/getPurchaserList") %>";
        var reqCategoryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveCategoryList")%>";

        var reqSupplierListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getSupplierNameList")%>";

        <%--var reqSupplierListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getSupplierList")%>";--%>
        <%--var reqPurchaserListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getPurchaserNameList")%>";--%>
    </script>
</asp:Content>

