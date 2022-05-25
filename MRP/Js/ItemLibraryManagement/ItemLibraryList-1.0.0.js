var ItemLibraryList;

function loadTable_ItemLibraryList(reqTableItemLibraryListUrl, authToken) {
    ItemLibraryList = $('#Table_ItemLibraryList').DataTable(
        {
            "processing": true,
            "serverSide": false,
            //"columnDefs": [
            //    { "targets": 0, "searchable": false, "className": "never" },
            //    { "className": "center", "targets": 1, "width": "2%", "orderable": false },
            //    { "targets": 2, "orderable": true, "width": "20%", "className": "left" }
            //],
            "ajax":
            {
                "url": reqTableItemLibraryListUrl,
                "contentType": "application/json",
                "type": "GET",
                "dataType": "JSON",
                "beforeSend": function (xhr) {
                    xhr.setRequestHeader("Authorization",
                        "Bearer " + authToken);
                },
                "data": function (d) {
                    return d;
                }
            },
            "columns": [
                {
                    "data": "ID",
                    "visible": false
                },
                { "data": "CategoryName", },
                { "data": "IPN", },
                { "data": "Manufacturer", },
                { "data": "MPN" },
                { "data": "ItemDescription", },
                { "data": "SupplierName", },
                { "data": "Currency", },
                { "data": "UnitPrice", },
                {
                    "data": "Std_LeadTime_Days",
                    "mRender": function (data, type, full) {
                        return data + " Days";
                    }
                },
                { "data": "IsDefault", },
                { "data": "CreatedDate", },
                { "data": "LastUpdatedDate", },
            ],

            "initComplete": function (settings, json) {
                //draw tooltip after the table complete
                $('[data-toggle="tooltip"]').tooltip();
            },

            "searchable": true,
            "orderable": true,
            "responsive": true,
        }
    );
}


$(document).ready(function () {
    loadTable_ItemLibraryList(reqTableItemLibraryListUrl, authToken);

    $.contextMenu({
        selector: "#Table_ItemLibraryList tbody tr",
        className: "css-title-toolbox",
        items: {
            "Item Details": {
                name: "Item Details",
                //icon: "fa-edit",
                icon: "fa-solid fa-circle-info",

                //callback: function (key, options) {

                //}
            },

            "Duplicate": {
                name: "Duplicate",
                //icon: "fa-solid fa-trash",
                icon: "fa-solid fa-copy",

                //callback: function (key, options) {

                //}
            },
            "edit": {
                name: "Edit",
                icon: "fa-edit",
                //url: "http://localhost:57709/Views/ItemLibraryManagement/ItemLibraryAdd",
                callback: function () {
                    window.location.href = "http://localhost:57709/Views/ItemLibraryManagement/AddNewItem";

                    //var rowData = CategoryList.row(options.$trigger).data();

                    //var categoryID = {
                    //    categoryID: rowData.ID
                    //}

                    //$("#editCategory").modal("show");

                    //loadCategoryID(reqCategoryIdUrl, authToken, categoryID)
                    //.catch((error) => {
                    //    $("#editCategory").modal("hide");
                    //    ShowError("Load Category Failed", "Fail");
                    //})
                    //.finally(() => {
                    //    $("#hdnID").val(categoryID.categoryID);
                    //})

                }
            },
            "delete": {
                name: "Delete",
                icon: "fa-solid fa-trash",
                callback: function (key, options) {

                    $("#deleteItem").modal("show");

                    loadCategoryID(reqCategoryIdUrl, authToken, categoryID)
                    .catch((error) => {
                        $("#deleteItem").modal("hide");
                        ShowError("Load Role Details Failed", "Fail");
                    })
                    .finally(() => {
                        $("#hdnID").val(categoryID.categoryID);
                    })


                    //var rowData = CategoryList.row(options.$trigger).data();

                    //$("#deleteCategory").modal("show");

                    //var categoryID = {
                    //    categoryID: rowData.ID
                    //}

                    //loadCategoryID(reqCategoryIdUrl, authToken, categoryID)
                    //.catch((error) => {
                    //    $("#deleteCategory").modal("hide");
                    //    ShowError("Load Role Details Failed", "Fail");
                    //})
                    //.finally(() => {
                    //    $("#hdnID").val(categoryID.categoryID);
                    //})

                }
            }
            //}
        }
    })

    //$("#btnDraftItem").click(function () {
    //    $("#deleteDraft").modal("show");
    //})
})



function AddNewItem() {
    window.location.href = "http://localhost:57709/Views/ItemLibraryManagement/AddNewItem";
}

function DraftItem() {
    $("#btnDraftItem").click(function () {
        $("#draftList").modal("show");
    })
}

function DeleteDraft() {
    $("#btnDeleteDraft").click(function () {
        $("#deleteDraft").modal("show");
    })
}

function Close() {
    $(".btn-close").click(function () {
        $("#draftList").modal("hide");
        $("#deleteDraft").modal("hide");
        $("#deleteItem").modal("hide");
    })
}

function Cancel() {
    //$("#cancelAddCategory").on("click", function () {
    //    $("#addCategory").modal("hide");
    //});

    //$("#cancelEditCategory").on("click", function () {
    //    $("#editCategory").modal("hide");

    //    target = $(this).parents(".modal");
    //    $(target)
    //    .find("input")
    //    .val("")
    //});

    $("#cancelDeleteItem").on("click", function (e) {
        //$("#deleteItem").modal("hide");

        target = $(this).parents(".modal");
        $(target)
        .find("input")
        .val("")
    });
}

//function Reset() {
//    $("#btnReset").on("click", function (e) {
//        //$("#")
//        target = $(this).parents(".input-container");
//        $(target)
//        //$(this)
//        .find("input")
//        .val("")
//    });
//}


function loadItemLibraryID(reqItemLibraryIdUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqItemLibraryIdUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#InputEditItemLibraryName").val(data.ItemLibraryName);
                console.log(data.ItemLibraryName);
                $("#InputEditItemLibraryDescription").val(data.Description || "");

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //console.log(xhr);
                error(xhr);
            }
        })
    })
}

function addItemLibrary(reqAddItemLibraryUrl, authToken, dataStr) {
    //var dataStr = { ID: id };
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddItemLibraryUrl,
            type: "POST",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
}

function editItemLibrary(reqEditItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditItemLibraryUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
}

function deleteItemLibrary(reqDeleteItemLibraryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteItemLibraryUrl,
            type: "Post",
            data: JSON.stringify(dataStr),
            dataType: "json",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken
            },
            success: function (data, status, xhr) {
                //$("#deleteItemLibrary").show()
                resolve(data);
            },
            error: function (xhr, ajaxOptions, thriwnError) {
                error(xhr);
            }
        })
    })

}

function cancel() {
    $("#addItemLibrary").addClass("hide");
}
