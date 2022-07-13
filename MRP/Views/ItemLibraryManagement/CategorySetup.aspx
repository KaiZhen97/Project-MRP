<%@ Page Title="Category Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategorySetup.aspx.cs" Inherits="MRP.Views.ItemLibraryManagement.CategorySetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
    <%: Styles.Render("~/styleBundle/ILM/CategorySetup") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="_pageTitle"><%= Page.Title %></h3>

    <form runat="server">
        <asp:TextBox runat="server" ID="hiddenCategoryID" ClientInstanceName="hiddenCategoryID"></asp:TextBox>
    </form>

    <div class="_button-container mb-4">
        <button class="solid-button normal" id="btnAddCategory">
            <i class="fa-solid fa-plus"></i>
            New Category
        </button>
    </div>

    <div class="_text-muted mb-3">* Right click on item to open tools box.</div>

    <table id="Table_CategoryList">
        <thead>
            <tr>
                <th>Category</th>
                <th>Description</th>
                <th>Created Date</th>
                <th>Created By</th>
                <th>Last Updated Date</th>
                <th>Lastd Updated By</th>
            </tr>
        </thead>
    </table>

    <div class="modal fade" id="addCategory" tabindex="-1" data-bs-backdrop="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Category</h5>
                    <button type="button" class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div>* Field with "<span class="req">*</span>" are compulsary.</div>
                    <br />
                    <div class="input-container pd-0">
                        <label>Category <span class="req">*</span> </label>
                        <input id="inputAddCategoryName" required />
                    </div>
                    <div class="input-container">
                        <label>Description</label>
                        <input id="inputAddCategoryDescription" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 center">
                    <button class="solid-button submit m-2" id="btnConfirmAddCategory" type="submit">
                    <i class="fa-solid fa-check"></i>
                        Confirm
                    </button>
                    <button class="solid-button close m-2" id="btnCancelAddCategory">
                    <i class="fa-solid fa-xmark"></i>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="editCategory" tabindex="-1" data-bs-backdrop="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Category</h5>
                    <button type="button" class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <p class="des">* Field with "<span class="req">*</span>" are compulsary.</p>
                    <div class="input-container">
                        <label>Category <span class="req">*</span> </label>
                        <input id="InputEditCategoryName" required />
                    </div>
                    <div class="input-container">
                        <label>Description</label>
                        <input id="InputEditCategoryDescription" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 center">
                    <button class="solid-button submit m-2" id="btnConfirmEditCategory" type="submit">
                    <i class="fa-solid fa-check"></i>
                        Confirm
                    </button>
                    <button class="solid-button close m-2" id="btnCancelEditCategory">
                    <i class="fa-solid fa-xmark"></i>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteCategory" tabindex="-1" data-bs-backdrop="true" >
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Category</h5>
                    <button class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-container">
                        <h6>Are you sure to delete</h6>
                        <label>Remark</label>
                        <input id="DeleteCategoryRemark" />
                    </div>
                </div>
                <div class="flex flex-row mt-3 end">
                    <button class="solid-button submit m-2" id="btnYesDeleteCategory" type="submit">
                        Yes
                    </button>
                    <button class="solid-button close m-2" id="btnNoDeleteCategory">
                        No
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="JsContent" runat="server">
    <%: Scripts.Render("~/scriptBundle/ILM/CategorySetup") %>

    <script>
        var authToken = "<%= Session["accessToken"].ToString() %>"
        var reqCategoryListUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveCategoryList")%>";
        var reqCategoryByIDUrl = "<%= ResolveUrl("~/api/ItemLibrary/getActiveCategoryByID")%>";
        var reqAddCategoryUrl = "<%= ResolveUrl("~/api/ItemLibrary/postAddNewCategory")%>";
        var reqEditCategoryUrl = "<%= ResolveUrl("~/api/ItemLIbrary/postEditCategory")%>";
        var reqDeleteCategoryUrl = "<%= ResolveUrl("~/api/ItemLIbrary/postDeleteCategory")%>";
        
        var hiddenCategoryID = "<%= hiddenCategoryID.ClientID%>";
        $(`#${hiddenCategoryID}`).css('display', 'none');
    </script>
</asp:Content>
