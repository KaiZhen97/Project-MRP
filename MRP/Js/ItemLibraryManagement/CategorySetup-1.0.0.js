//var tableCategoryList = null;

//$(document).ready(function () {
//    loadTableCategoryList(reqTableListUrl, authToken);

//    $("#btnShowModal_NewCategory").click(function () {
//        Drawer_ResetInput("#Modal_AddNewCategory");
//        Drawer_Show("#Modal_AddNewCategory");
//    });

//    $.contextMenu({
//        selector: '#Table_CategoryList tbody tr',
//        className: 'css-title-toolbox',
//        items: {
//            'edit': {
//                name: 'Edit',
//                icon: 'fa-edit',
//                callback: function (key, options) {
//                    var targetDrawer = $("#Modal_EditCategory");
//                    var rowData = tableTabList.row(options.$trigger).data();

//                    var dataStr = {
//                        moduleID: rowData.ModuleID
//                    }

//                    $(`#${hiddenCategoryID}`).val(rowData.ModuleID);

//                    Drawer_ResetInput(targetDrawer);
//                    Drawer_Show(targetDrawer, true);

//                    loadEditCategoryDetails(rowData.ModuleID)
//                        .then(() => {
//                            Drawer_Show(targetDrawer, false);
//                        })
//                        .catch((error) => {
//                            Drawer_Hide(targetDrawer);
//                            ShowError('Failt To Load Tab Details', error.responseText || error)
//                        })
//                }
//            }
//        }
//    })
//})

//function loadTableCategoryList(reqTableListUrl, authToken) {
//    tableTabList = $("#Table_CategoryList").DataTable(
//        {
//            "processing": true,
//            "serverSide": false,
//            "ajax":
//            {
//                "url": reqTableListUrl,
//                "contentType": "application/json",
//                "type": "Get",
//                "dataType": "JSON",
//                "beforeSend": function (xhr) {
//                    xhr.setRequestHeader("Authorization",
//                        "Bearer " + authToken);
//                },
//                "data": function (d) {
//                    return d;
//                }
//            },
//            "columns": [
//                { "data": "CategoryName" },
//                { "data": "Description" },
//                { "data": "CreatedDate" },
//                { "data": "CreatedByStaffName" },
//                {
//                    "data": "LastUpdatedDate",
//                    "mRender": function (data, type, full) {
//                        var date = new Date(data);

//                        return `${date.getFullYear().toString()}-${(date.getMonth() + 1).toString()}-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
//                    }
//                },
//                { "data": "LastUpdatedByStaffName" },
//            ],
//            "initComplete": function (settings, json) {
//                //draw tooltip after the table complete
//                $('[data-toggle="tooltip"]').tooltip();
//            },
//            "searchable": true,
//            "orderable": true,
//            "responsive": true,
//        }
//    );
//}

//function loadEditCategoryDetails(id) {
//    var dataStr = {
//        ID: id
//    };

//    return new Promise((resolve, error) => {
//        $.ajax({
//            url: reqCategoryByIDUrl,
//            type: 'Post',
//            dataType: 'json',
//            data: JSON.stringify(dataStr),
//            headers: {
//                "Content-Type": 'application/json; charset=utf-8',
//                "Authorization": "Bearer " + authToken,
//            },
//            success: function (data, status, xhr) {
//                $("#inputEditCategory_Category").val(data.CategoryName);
//                $("#inputEditCategory_Description").val(data.Description);

//                resolve(data);
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                console.log(xhr);
//                error(xhr);
//            }
//        })
//    });
//}

var CategoryList;

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
                //{
                //    "data": "ID",
                //    "visible": true,
                //},
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

                    console.log(rowData);

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
                    .finally(() => {
                        $(`#${hiddenCategoryID}`).val(category.ID);

                        //console.log("cat" + category);
                        //console.log("hdn" + $(`#${hiddenCategoryID}`));
                        //console.log("rowData" + rowData);
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

                    //$("#hdnID").val(rowData.ID)
                    $(`#${hiddenCategoryID}`).val(rowData.ID)

                    $("#deleteCategory").modal("show");

                    loadCategoryID(reqCategoryByIDUrl, authToken, category)
                    .catch((error) => {
                        $("#deleteCategory").modal("hide");
                        ShowError("Load Role Details Failed", "Fail");
                    })
                    .finally(() => {
                        $(`#${hiddenCategoryID}`).val(category.ID);
                        //console.log("cat",category);
                    })
                }
            }
        }
    })

    $("#btnAddCategory").on("click", function () {
        $("#inputAddCategoryName").val("");
        $("#inputAddCategoryDescription").val("");
        $("#addCategory").modal("show");
    })

    $("#btnSaveAddCategory").click(function () {
        $("#addCategory")
        .submit()
        .modal("hide");

        var dataStr = {
            categoryName: $("#inputAddCategoryName").val(),
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
    })

    $("#btnSaveEditCategory").click(function () {
        $("#editCategory").submit();

        var dataStr = {
            ID: $(`#${hiddenCategoryID}`).val(),
            categoryName: $("#InputEditCategoryName").val(),
            Description: $("#InputEditCategoryDescription").val(),
        }
        //console.log("hdn" + dataStr);

        editCategory(reqEditCategoryUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Edit Category Success", data.Message || "Add successfully!");

            console.log("data" + data);
            console.log("datastr" + dataStr);

            CategoryList.ajax.reload();
            ShowInfo("Category List Reload", "Category list has been reloaded with latest data.");

            $("#editCategory").modal("hide");

        })
        .catch((error) => {
            ShowError("Fail To Edit Category", error.responseText || error);
        })

        $("#editCategory").modal("hide");

        //target = $(this).parents(".modal");

        //$(target)
        //.find("input")
        //.val("");
    })


    $("#btnConfirmDeleteCategory").click(function () {
        $("#deleteCategory").submit();
        
        //$("#btnConfirmDeleteCategory").addClass("busy");

        var dataStr = {
            ID: $(`#${hiddenCategoryID}`).val(),
            Description: $("#DeleteCategoryDescription").val()
        }

        deleteCategory(reqDeleteCategoryUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Delete Category Success", data.Message || "Delete successfully!");

            console.log("data" + data);
            console.log("datastr" + dataStr);

            //$("#btnConfirmDeleteCategory").removeClass('busy');

            CategoryList.ajax.reload();
            ShowInfo("Category List Reload", "Category list has been reloaded with latest data.");

            $("#deleteCategory").modal("hide");

        })
        .catch((error) => {
            ShowError("Fail To Delete Category", error.responseText || error);
        })

        $("#deleteCategory").modal("hide");

        target = $(this).parents(".modal");

        $(target)
        .find("input")
        .val("");
    })

    //$("#btnAddCategory").click(function () {
    //    $("#addCategory").modal("show");
    //})

})

function Close() {
    $(".btn-close").click(function () {
        $("#addCategory").modal("hide");
        $("#editCategory").modal("hide");
        $("#deleteCategory").modal("hide");
    })
}

function Cancel() {
    $("#cancelAddCategory").on("click", function () {
        $("#addCategory").modal("hide");

        target = $(this).parents(".modal");

        $(target)
        .find("input")
        .val("")
    });

    $("#cancelEditCategory").on("click", function () {
        $("#editCategory").modal("hide");
    });

    $("#cancelDeleteCategory").on("click", function (e) {
        $("#deleteCategory").modal("hide");

        target = $(this).parents(".modal");

        $(target)
        .find("input")
        .val("")
    });
}

function loadCategoryID(reqCategoryByIDUrl, authToken, dataStr) {
    //console.log("dataStr"+ dataStr);

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
                //console.log("data" + data);
                //console.log("hdn" + $(`#${hiddenCategoryID}`));
                //$(`#${hiddenCategoryID}`)
                $(`#${hiddenCategoryID}`).val(data.ID);
                $("#InputEditCategoryName").val(data.CategoryName);
                //console.log(data.CategoryName);
                $("#InputEditCategoryDescription").val(data.Description || "");

                resolve();
                //resolve(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
}

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
}

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
}

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
}