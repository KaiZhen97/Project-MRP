<%@ Page Title="Supplier Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierSetup.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.SupplierSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/SupplierSetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="_pageTitle"><%= Page.Title %></h3>

    <form runat="server">
        <%--<asp:TextBox runat="server" ID="hiddenSupplierID" ClientInstanceName="hiddenSupplierID"></asp:TextBox>--%>
    </form>

    <div class="_button-container mb-4">
        <button class="solid-button normal" id="btnAddSupplier">
            <i class="fa-solid fa-plus"></i>
            New Supplier Relation
        </button>
    </div>

    <div class="_text-muted mb-3">* Right click on item to open tools box.</div>

    <table id="Table_SupplierList">
        <thead>
            <tr>
                <th>CODE</th>
                <th>Supplier Name</th>
                <th>SST Charge</th>
                <th>Created Date</th>
                <th>Created By</th>
                <th>Last Updated Date</th>
                <th>Last Updated By</th>
            </tr>
        </thead>
    </table>

    <div class="modal fade" id="addSupplier" tabindex="-1" data-bs-backdrop="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title ms-3">Item Library Changes Details</h5>
                    <button type="button" class="btn-close"></button>
                </div>
                <div class="modal-body ms-2 me-2">
                    <div class="flex flex-row center">
                        <button class="solid-button normal me-4" id="OriginalDetail" style="color: white">
                            ORIGINAL
                        </button>
                        <button class="solid-button white" id="EditedDetail">
                            EDITED
                        </button>
                    </div>

                    <div class="title mt-3"> 1 | Basic Information </div>
                    <br />
                    <div class="input-container ms-3">
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 100%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 17%;">Item Description</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="mt-0" style="width: 97.5%"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Category</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">HS Code / Tariff</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Inti. Part Number</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">More Details</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Manufacturer</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Required SN</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Mfr. Part Number</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Required Calibration</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Remark</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Attachments</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                    </div>

                    <div class="flex flex-row">
                        <div class="flex flex-col me-5">
                            <div class="title">2 | Supplier & Pricing</div> 
                        </div>
                    </div>
                    <br />
                    <div class="input-container tab" style="background: #F8F8F8">
                        <span class="center p-3" style="background-color: #E5C860;  border-radius: 5px; font-weight: 500">D E F A U L T</span>
                        <div class="row m-2 ps-2 mt-3" style="width: 99%">
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
                        <div class="row m-2 ps-2" style="width: 99%">
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
                        <div class="row m-2 ps-2 mb-4" style="width: 49%;">
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

                    <div class="title" >3 | Other Attachments</div>
                    <br />
                    <div class="input-container ms-3">
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Default Purchaser 1</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Purchaser 2</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Attachments</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Confi. Attachments</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                        <div class="flex flex-row mb-1">
                            <div class="flex flex-col" style="width: 50%;">
                                <div class="flex flex-row" >
                                    <div class="label" style="width: 34%;">Remark</div>
                                    <span class="info">12345</span>
                                </div>
                                <hr class="line mt-0"/>
                            </div>
                        </div>
                    </div>


                    <div class="title">4 | Key Tech Specification</div>
                    <br />
                    <div class="box me-0">
                        <div class="container-fluid ms-1">
                            <div class="row" id="KeyTechSpec">
                                <div class="input col-6 mt-2 mb-2">
                                    <div class="input-group row ms-1" style="width: 99%">
                                        <input class="spec_left" style="color: white; width: 15%; flex-grow: 1" disabled/>
                                        <input class="spec_right" style="width: 34%; flex-grow: 1" disabled/>
                                        <%--<div class="spec_right" style="width: auto;">
                                            <i class="fa-solid fa-circle-minus" id="btnRemoveKTS" style="color: red;"></i>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="input col-6 mt-2 mb-2">
                                    <div class="input-group row" style="width: 99%">
                                        <input class="spec_left" style="color: white; width: 15%; flex-grow: 1"/>
                                        <input class="spec_right" style="width: 34%; flex-grow: 1"/>
                                    </div>
                                </div>
                                <div class="input col-6 mt-2 mb-2">
                                    <div class="input-group row" style="width: 99%">
                                        <input class="spec_left" style="color: white; width: 15%; flex-grow: 1"/>
                                        <input class="spec_right" style="width: 35%; flex-grow: 1"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                        

                        <%--<div class="row ms-2 mt-1">
                            <button id="btnAdd" style="background: #BCBCBC; border: 1px solid #B2B2B2; width: 48%;"><i class="fa-solid fa-plus"></i></button>
                        </div>--%>
                    <%--</div>--%>
                    <br />


                    <div class="title" >5 | PWP Bundle</div>
    
                    <%--<button class="AddRemoveBtn" id="btnAddPWPItem"><i class="fa-solid fa-plus me-2"></i> Add New</button>
                    <button class="AddRemoveBtn" id="btnEditPWPItem"><i class="fa-solid fa-minus me-2"></i> Remove</button>

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
                    </table>--%>


                    <%--<div>* Field with "<span class="req">*</span>" are compulsary.</div>
                    <br />
                    <div class="input-container pd-0">
                        <label>Supplier <span class="req">*</span> </label>
                        <input id="inputAddSupplierName" required />
                    </div>
                    <div class="input-container">
                        <label>Description</label>
                        <input id="inputAddSupplierDescription" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 center">
                    <button class="solid-button submit m-2" id="btnConfirmAddSupplier" type="submit">
                    <i class="fa-solid fa-check"></i>
                        Confirm
                    </button>
                    <button class="solid-button close m-2" id="btnCancelAddSupplier">
                    <i class="fa-solid fa-xmark"></i>
                        Cancel
                    </button>
                </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editSupplier" tabindex="-1" data-bs-backdrop="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <span class="modal-title">Edit Supplier</span>
                    <button type="button" class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <p class="des">* Field with "<span class="req">*</span>" are compulsary.</p>
                    <div class="input-container">
                        <label>Supplier <span class="req">*</span> </label>
                        <input id="InputEditSupplierName" required />
                    </div>
                    <div class="input-container">
                        <label>Description</label>
                        <input id="InputEditSupplierDescription" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 center">
                    <button class="solid-button submit m-2" id="btnConfirmEditSupplier" type="submit">
                    <i class="fa-solid fa-check"></i>
                        Confirm
                    </button>
                    <button class="solid-button close m-2" id="btnCancelEditSupplier">
                    <i class="fa-solid fa-xmark"></i>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteSupplier" tabindex="-1" data-bs-backdrop="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Supplier</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete</h6>
                        <label>Remark</label>
                        <input id="DeleteSupplierRemark" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnYesDeleteSupplier" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoDeleteSupplier">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/SupplierSetup") %>

    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>"
        var reqSupplierListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveSupplierList")%>";
        var reqSupplierByIDUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveSupplierByID")%>";
        var reqAddSupplierUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddNewSupplier")%>";
        var reqEditSupplierUrl = "<%= ResolveUrl("~/api/ItemLIbrary/postEditSupplier")%>";
        var reqDeleteSupplierUrl = "<%= ResolveUrl("~/api/ItemLIbrary/postDeleteSupplier")%>";
        
        <%--var hiddenSupplierID = "<%= hiddenSupplierID.ClientID%>";--%>
        //$(`#${hiddenSupplierID}`).css('display', 'none');
    </script>
</asp:Content>
