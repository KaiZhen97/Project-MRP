<%@ Page Title="Item Library List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemLibraryList.aspx.cs" Inherits="MRP.Views.ItemLibrary.ItemLibraryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/ItemLibraryList") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="_pageTitle"><span><%= Page.Title %></span></h3>

    <input type="hidden" id="hdnID"/>

    <div class="d-flex flex-row">
        <button class="solid-button normal mb-2 me-2" id="btnAddItemLibrary" onClick="AddNewItem()">
            <i class="fa-solid fa-plus"></i>
            Add New (NPI)
        </button>

        <button class="solid-button draft normal mb-2 ms-2" id="btnDraftItemLibrary">
            <i class="fa-solid fa-envelope-open me-3"></i>
            <span class="translate-middle badge rounded-pill bg-warning" id="DraftNumber"></span>
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
                <%--<th>Supplier</th>--%>
                <%--<th>Currency</th>--%>
                <%--<th>U/Price</th>--%>
                <%--<th>Lead Time</th>--%>
                <%--<th>IsDefault</th>--%>
                <th>CreatedDate</th>
                <th>LastUpdateDate</th>
            </tr>
        </thead>
    </table>

    <div class="modal fade" id="deleteItem" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Item</h5>
                    <button class="btn-close""></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete<span></span></h6>
                        <label>Remark</label>
                        <input id="DeleteItemRemark" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnYesDeleteItem" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoDeleteItem">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteDraft" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Draft</h5>
                    <button class="btn-close""></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete the draft?</h6>
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnYesDeleteDraft" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoDeleteDraft">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="draftList" tabindex="1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Draft List</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <form class="container-fluid">
                        <p>* Draft item will permanently delete after 30 days</p>
                        <input type="text" id="keywordSearch" placeholder="Search">
                        <ul id="draftItemList"></ul>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/ItemLibraryList") %>

    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>";
        var reqTableItemLibraryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveItemLibraryList") %>";
        var reqItemLibraryIdUrl = "<%= ResolveUrl("~/api/ItemLibrary/postItemLibraryByID")%>";
        var reqAddItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddItemLibrary")%>";
        var reqEditItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postEditItemLibrary")%>";
        var reqDeleteItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteItemLibrary")%>";

        var reqDraftItemLibraryUrl = "<%= ResolveUrl("~/api/ItemLibrary/getDraftItemLibraryList") %>";

        var reqAddDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddDraftItem")%>";
        var reqSaveDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postSaveDraftItem")%>";
        var reqDeleteDraftItemUrl = "<%= ResolveUrl("~/api/ItemLibrary/postDeleteDraftItem")%>";

    </script>
</asp:Content>
