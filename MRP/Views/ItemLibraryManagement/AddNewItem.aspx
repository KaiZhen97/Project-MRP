<%@ Page Title="Add New Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewItem.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.AddNewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/AddNewItem") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h4 class="_pageTitle"><%= Page.Title %></h4>--%>

    <%--<div style="font-size: .75rem; margin-top: 5px; margin-bottom: 10px;">All fields with denoted (<span style="color: #ff0000;">*</span>) are required.</div>--%>

    <div class="d-flex flex-row m-1">
        <h4 class="_pageTitle">
            <i class="fa-solid fa-arrow-left me-3"></i>
            <span><%= Page.Title %></span>
        </h4>
    </div>

    <p> * Field with " <span style="color: red;">*</span> " is compulsory  * New Item with existed IPN will replace "Key Tech Spe" value</p>
    <hr />

    <h4>1 | Basic Information</h4>
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
                    <select id="inputSerialNumber" style="width: 95%;">
                        <option>Yes</option>
                        <option>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Required Calibration</label>
                    <select id="inputCalibration" style="width: 95%;">
                        <option>Yes</option>
                        <option>No</option>
                    </select>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Is Default</label>
                    <select id="inputIsDefault" style="width: 95%;" required>
                        <option>Yes</option>
                        <option>No</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label class="label">More Details</label>
                    <input id="inputDetails" style="width: 49%; height: 120px" />
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h4>2 | Pricing & Shipping</h4>
    <div class="input-container">
        <div class="row ms-0 mb-2">
            <div class="col">
                <div class="row">
                    <label>Supplier <span style="color: red;">*</span></label>
                    <select id="inputSuppiler" style="width: 95%;" required>
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
                    <label>Quotation Validdity</label>
                    <input id="inputQuotationValiddity" style="width: 95%"/>
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
                    <input type="number" id="inputUnitPrice" style="width: 95%" placeholder="RM" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Unit Price Discount</label>
                    <input type="number" id="inputUnitPriceDiscount" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Final Unit Price</label>
                    <input type="number" id="inputFinalUnitPrice" style="width: 95%" placeholder="RM"/>
                </div>
            </div>
        </div>

        <div class="row ms-0 mb-2" style="width: 50%;">
            <div class="col">
                <div class="row">
                    <label class="label">Minimum Amount Per Order <span style="color: red;">*</span></label>
                    <input type="number" id="inputMinimumOrder" style="width: 95%;" required/>
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label class="label">Standard LeadTime</label>
                    <input type="number" id="inputStandardLeadTime" style="width: 96%;" placeholder:"W.Weeks"/>
                </div>
            </div>
        </div>
    </div>

    <hr />

    <h4>3 | Other Attachments</h4>
    <div class="input-container">
        <div class="row">
            <div class="col">
                <div class="row ms-0 mb-3">
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="label">Default Purchaser / Agent 1 </label>
                            <select id="inputAgent1">
                                <option>Agent 1</option>
                            </select>
                        </div>
                    </div>
                    <div class="col">
                        <div class="row" style="width: 100%;">
                            <label class="label">Default Purchaser / Agent 2 </label>
                            <select id="inputAgent2">
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

    <h4>4 | Key Tech Specification</h4>
    <div class="input-container">

    </div>

    <hr />

    <h4>5 | PWP Bundle</h4>
    <br />
    <%--<div class="input-container">--%>
        <button class="AddRemoveBtn"><i class="fa-solid fa-plus me-2"></i> Add New</button>
        <button class="AddRemoveBtn"><i class="fa-solid fa-minus me-2"></i> Remove </button>
    <%--</div>--%>

    <br />

    <%--<div class="input-container"></div>--%>
    <br />
    <div class="center">
        <button class="btnReset me-3" id="btnReset" onclick="Reset()"><i class="fa-solid fa-rotate-right me-2"></i> Reset</button>
        <button class="btnClose me-3"><i class="fa-solid fa-xmark me-2"></i> Close</button>
        <button class="btnSaveDraft me-3"><i class="fa-solid fa-inbox me-2"></i> Save Draft</button>
        <button class="btnConfirm me-3"><i class="fa-solid fa-check me-2"></i> Confirm</button>
    </div>
            

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/AddNewItem") %>
</asp:Content>

