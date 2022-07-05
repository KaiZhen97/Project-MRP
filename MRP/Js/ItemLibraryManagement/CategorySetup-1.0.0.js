//var CategoryList;

function loadTable_CategoryList(reqCategoryListUrl, authToken) {
    CategoryList = $("#Table_CategoryList").DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqCategoryListUrl,
                "contentType": "application/json",
                "type": "Get",
                "dataType": "JSON",
                "beforeSend": function (xhr) {
                    xhr.setRequestHeader("Authorization", "Bearer " + authToken);
                },
                "data": function (d) {
                    return d;
                }
            },
            "columns": [
                {
                    "data": "CategoryName"
                },
                {
                    "data": "Description"
                },
                {
                    "data": "CreatedBy"
                },
                {
                    "data": "CreatedDate"
                },
                {
                    "data": "LastUpdatedBy"
                },
                {
                    "data": "LastUpdatedDate"
                },
            ],
            "initComplete": function (settings, json) {
                $('[data-toggle="tooltip"]').tooltip();
            },
            "searchable": true,
            "orderable": true,
            "responsive": true,
        }
    );
}

$(document).ready(function () {
    loadTable_CategoryList(reqCategoryListUrl, authToken);

    $.contextMenu({
        selector: "#Table_CategoryList tbody tr",
        className: "css-title-toolbox",
        items: {
            "edit": {
                name: "Edit",
                icon: "fa-edit",
                callback: function (key, options) {

                    var rowData = CategoryList.row(options.$trigger).data();

                    var category = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenCategoryID}`).val(rowData.ID)

                    $("#editCategory").modal("show");

                    loadCategoryID(reqCategoryByIDUrl, authToken, category)
                    .catch((error) => {
                        $("#editCategory").modal("hide");
                        ShowError("Load Category Failed", "Fail");
                    })
                }
            },
            "delete": {
                name: "Delete",
                icon: "fa-solid fa-trash",
                callback: function (key, options) {

                    var rowData = CategoryList.row(options.$trigger).data();

                    var category = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenCategoryID}`).val(rowData.ID);

                    $("#deleteCategory").modal("show");

                    loadCategoryID(reqCategoryByIDUrl, authToken, category)
                    .catch((error) => {
                        $("#deleteCategory").modal("hide");
                        ShowError("Load Role Details Failed", "Fail");
                    })
                }
            }
        }
    });

    $("#btnConfirmAddCategory").click(function () {
        $("#addCategory").submit();

        var dataStr = {
            CategoryName: $("#inputAddCategoryName").val(),
            Description: $("#inputAddCategoryDescription").val()
        }

        addCategory(reqAddCategoryUrl, authToken, dataStr)
        .then((data) => {
            ShowPass("Add Category Success", data.Message || "Add successfully!");

            CategoryList.ajax.reload();
            ShowInfo("Category List Reload", "Category list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Add Category", error.responseText || error);
        })
        $("#addCategory").modal("hide");

        $("input").val("");
    });

    $("#btnConfirmEditCategory").click(function () {
        $("#editCategory").submit();

        var dataStr = {
            ID: $(`#${hiddenCategoryID}`).val(),
            categoryName: $("#InputEditCategoryName").val(),
            Description: $("#InputEditCategoryDescription").val(),
        }

        editCategory(reqEditCategoryUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Edit Category Success", data.Message || "Add successfully!");

            CategoryList.ajax.reload();
            ShowInfo("Category List Reload", "Category list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Edit Category", error.responseText || error);
        })
        $("#editCategory").modal("hide");
    });

    $("#btnYesDeleteCategory").click(function () {
        $("#deleteCategory").submit();

        var dataStr = {
            ID: $(`#${hiddenCategoryID}`).val(),
            DeletedRemark: $("#DeleteCategoryRemark").val()
        }
        $("#deleteCategory").modal("hide");

        deleteCategory(reqDeleteCategoryUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Delete Category Success", data.Message || "Delete successfully!");

            CategoryList.ajax.reload();
            ShowInfo("Category List Reload", "Category list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Delete Category", error.responseText || error);
        })
        $("input").val("");
    });

    
    $("#btnAddCategory").click(function () {
        $("#addCategory").modal("show");
    });

    $(".btn-close")
    .add("#btnCancelAddCategory")
    .add("#btnNoDeleteCategory")
    .add("#btnCancelEditCategory")
    .click(function () {
        $("#addCategory").modal("hide");
        $("#editCategory").modal("hide");
        $("#deleteCategory").modal("hide");
        $("input").val("");
    });
})

function loadCategoryID(reqCategoryByIDUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqCategoryByIDUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#InputEditCategoryName").val(data.CategoryName);
                $("#InputEditCategoryDescription").val(data.Description || "");

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
};

function addCategory(reqAddCategoryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddCategoryUrl,
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
};

function editCategory(reqEditCategoryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditCategoryUrl,
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
};

function deleteCategory(reqDeleteCategoryUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteCategoryUrl,
            type: "Post",
            data: JSON.stringify(dataStr),
            dataType: "json",
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
};