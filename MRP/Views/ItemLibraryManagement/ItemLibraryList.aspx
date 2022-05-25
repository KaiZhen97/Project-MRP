<%@ Page Title="Item Library List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemLibraryList.aspx.cs" Inherits="MRP.Views.ItemLibrary.ItemLibraryList" %>

<%--------------------------------------------------------------------%>
<%------------------------- CSS Style Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/ItemLibraryList") %>
</asp:Content>

<%--------------------------------------------------------------------%>
<%------------------------- HTML Body Render -------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h4 class="_pageTitle"><%= Page.Title %></h4>--%>
    <h3 class="_pageTitle"><span><%= Page.Title %></span></h3>

    <div class="d-flex flex-row">
        <button class="solid-button normal mb-2 me-2" id="btnAddItemLibrary" onClick="AddNewItem()">
            <i class="fa-solid fa-plus"></i>
            Add New (NPI)
        </button>

        <%--<button class="solid-button draft normal mb-2 ms-2" id="btnDraftItem" data-bs-toggle="modal" data-bs-target="#deleteDraft">--%>
        <button class="solid-button draft normal mb-2 ms-2" id="btnDraftItem" onClick="DraftItem()">
            <i class="fa-solid fa-envelope-open me-3"></i>
            <%--<span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-secondary">+99 <span class="visually-hidden">unread messages</span></span>--%>
            <span class="translate-middle badge rounded-pill bg-warning">99</span>
            <%--<span class="visually-hidden">unread messages</span>--%>
            Draft Item
        </button>
    </div>
    <br />
    <p>* Right click an item to open tools box</p>

    <table id="Table_ItemLibraryList">
        <thead>
            <tr>
                <th></th>
                <th>Category</th>
                <th>Intl Part No.</th>
                <th>Manufacturer</th>
                <th>Mnf Part No.</th>
                <th>Item Description</th>
                <th>Supplier</th>
                <th>Currency</th>
                <th>U/Price</th>
                <th>Lead Time</th>
                <th>IsDefault</th>
                <th>CreatedDate</th>
                <th>LastUpdateDate</th>
            </tr>
        </thead>
    </table>

        <div class="modal" id="deleteItem" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Item</h5>
                    <button class="btn-close" onClick="Close()"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete<span></span></h6>
                        <label>Description</label>
                        <input id="DeleteItemDescription" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnConfirmDeleteItem" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="cancelDeleteItem" onClick="Cancel()">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="deleteDraft" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Draft</h5>
                    <button class="btn-close" onclick="Close()"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete the draft?</h6>
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnConfirmDeleteDraft" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="cancelDeleteDraft" onclick="Cancel()">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="draftList" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Draft List</h5>
                    <button class="btn-close" onclick="Close()"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>* Draft item will permanently delete after 30 days</h6>
                        <%--<input type="search" />--%>
                        Search...
                        <hr />
                    </div>
                </div>
                <%--<div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnConfirmDeleteDraft" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="cancelDeleteDraft" onclick="Cancel()">
                        No
                    </button>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>

<%--------------------------------------------------------------------%>
<%---------------------------- JS Render -----------------------------%>
<%--------------------------------------------------------------------%>
<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/ItemLibraryList") %>

    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>";
        //var reqTableURL = "<%= ResolveUrl("~/api/ItemLibrary/getActiveItemLibraryList") %>";

        var reqTableItemLibraryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveItemLibraryList") %>";
        var reqAddItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddItemLibrary")%>";
        var reqEditItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postEditItemLibrary")%>";
        var reqDeleteItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteItemLibrary")%>";
        var reqItemLibraryIdUrl = "<%= ResolveUrl("~/api/ItemLibrary/postItemLibraryByID")%>";

        //$.contextMenu({

        //})
    </script>
</asp:Content>
