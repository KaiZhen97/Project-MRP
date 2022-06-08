﻿<%@ Page Title="Edit Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditItem.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.EditItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/EditItem") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex flex-row m-1">
        <h4 class="_pageTitle">
            <i class="fa-solid fa-arrow-left me-3"></i>
            <span><%= Page.Title %></span>
        </h4>
    </div>

    <%--<form runat="server">
        <asp:TextBox runat="server" ID="hiddenCategoryID" ClientInstanceName="hiddenCategoryID"></asp:TextBox>
    </form>--%>

    <p> * Field with " <span style="color: red;">*</span> " is compulsory  * New Item with existed IPN will replace "Key Tech Spe" value</p>
    <hr />

    <%--<form action="ItemLibraryList" method="post" id="list">--%>

    <h5>1 | Basic Information</h5>
    <div class="input-container">
        <div class="row ms-0 mb-2">
            <label>Item Description <span style="color: red;">*</span></label>
            <input id="inputItemDescription" style="width: 99%" required/>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">Category <span style="color: red;">*</span></label>
                    <select id="inputCategoryID" style="width: 95%;" required>
                        <option selected hidden disabled>select</option>
                        <option>cable</option>
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
                        <option selected hidden disabled>select</option>
                        <option value="null">null</option>
                        <option value="1">Yes</option>
                        <option value="0">No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Calibration</label>
                    <select id="inputRequiredCalibration" style="width: 95%;">
                        <option selected hidden disabled>select</option>
                        <option value="null">null</option>
                        <option value="1">Yes</option>
                        <option value="0">No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Is Default</label>
                    <select id="inputIsDefault" style="width: 95%;" required>
                        <option selected hidden disabled>select</option>
                        <option value="null">null</option>
                        <option value="1">Yes</option>
                        <option value="0">No</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">More Details</label>
                    <input id="inputMoreDetails" style="width: 49%; height: 120px" />
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
                    <select id="inputSupplierName" style="width: 95%;" required>
                        <option selected hidden disabled>select</option>
                        <option>Supplier</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Delivery Term</label>
                    <input id="inputDeliveryTerm" style="width: 95%"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Quotation Date</label>
                    <input type="date" id="inputQuotationDate" style="width: 95%;"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label>Quotation Validity</label>
                    <input id="inputQuotationValidity" style="width: 95%"/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">UOM <span style="color: red;">*</span></label>
                    <input id="inputUOM" style="width: 95%" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Unit Price <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="0.01" id="inputUnitPrice" style="width: 95%" placeholder="RM" required/>
                </div>
            </div>
            <div class="col">
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

        <div class="row ms-0 mb-2" style="width: 50%;">
            <div class="col">
                <div class="row">
                    <label class="label">Minimum Amount Per Order <span style="color: red;">*</span></label>
                    <input type="number" min="0" step="any" id="inputMinAmountPerOrder" style="width: 95%;" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Standard LeadTime</label>
                    <input type="number" min="0" id="inputStdLeadTime" style="width: 96%;" placeholder:"W.Weeks"/>
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
                            <select id="inputPurchaser1">
                                <option selected hidden disabled>select</option>
                                <option value="null">null</option>
                                <option>Agent 1</option>
                                <option>Agent 2</option>
                            </select>
                        </div>
                    </div>
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="label">Default Purchaser / Agent 2 </label>
                            <select id="inputPurchaser2">
                                <option selected hidden disabled>select</option>
                                <option value="null">null</option>
                                <option>Agent 1</option>
                                <option>Agent 2</option>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="row ms-0">
                    <label>Remark</label>
                    <input id="inputRemark" style="width: 98%; height: 120px" />
                </div>
            </div>

            <div class="col">
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label>Attachments</label>
                    <input type="file" id="inputAttachments" style="width: 99%; height: 90px;"/>
                    <%--<i class="fa-solid fa-file"></i>--%>
                </div>
                <div class="row ms-0 mb-2" style="width: 100%">
                    <label>Confidential Attachments</label>
                    <input type="file" id="inputConfidential" style="width: 99%; height: 90px" />
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h5>4 | Key Tech Specification</h5>
    <div class="box">
        <%--<div class="row ms-0 mb-3">--%>
            <%--<div class="spec_left m-0 1 0 1" style="background-color: #0C56AD; width: 19%">1</div>--%>
            <%--<div class="spec_right m-0 1 1 1" style="background: white; width: 30%">--%>
                <%--2--%>
                <%--<div class="end">
                    <i class="fa-solid fa-circle-minus" style="color:red"></i>
                </div>--%>
            <%--</div>--%>
        <%--</div>--%>
        <%--<div class="row ms-0">--%>
            <button id="btnAdd" style="background: #BCBCBC; width: 50%;"><i class="fa-solid fa-plus"></i></button>
        <%--</div>--%>
    </div>

    <hr />

    <h5>5 | PWP Bundle</h5>
    <%--<br />--%>
    <%--<div class="input-container label">--%>
        <button class="AddRemoveBtn"><i class="fa-solid fa-plus me-2"></i> Add New</button>
        <button class="AddRemoveBtn"><i class="fa-solid fa-minus me-2"></i> Remove </button>
    <%--</div>--%>

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

    <%--<div class="input-container"></div>--%>
    <br />
    <div class="center">
        <button class="btnOrange me-3" id="btnResetAdd">
            <i class="fa-solid fa-rotate-right me-2"></i>
            Reset
        </button>
        <button class="btnRed me-3" id="btnCloseAdd" data-bs-toggle="modal" data-bs-target="#closeAdd">
            <i class="fa-solid fa-xmark me-2"></i>
            Close
        </button>
        <button class="btnBlue me-3" id="btnSaveDraftAdd">
            <i class="fa-solid fa-inbox me-2"></i>
            Save Draft
        </button>
        <button class="btnGreen me-3" id="btnConfirmAdd" data-bs-toggle="modal" data-bs-target="#confirmAdd">
            <i class="fa-solid fa-check me-2"></i>
            Confirm
        </button>
    </div>


    <div class="modal fade" id="confirmAdd" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Item - Found Existed IPN</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <p>Found IPN already exists in system, continue adding will some value of existing item</p>
                        <p>Are you sure want to proceed?</p>
                    </div>
                </div>
                <div class="flex flex-row end">
                    <button class="btnGreen m-2" id="btnYesConfirmAdd" style="color: white;" type="submit">
                        Yes
                    </button>
                    <button class="btnRed m-2" id="btnNoConfirmAdd">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="closeAdd" tabindex="-1" data-bs-backdrop="true" >
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
                    <button class="btnBlue m-2" id="btnSaveDraftCloseAdd" onclick="SaveDraft()">
                        Save Draft
                    </button>
                    <button class="btnOrange m-2" id="btnDiscardCloseAdd" onclick="Discard()">
                        Discard
                    </button>
                    <button class="btnRed m-2" id="btnCancelCloseAdd" onclick="Cancel()">
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/EditItem") %>
</asp:Content>
