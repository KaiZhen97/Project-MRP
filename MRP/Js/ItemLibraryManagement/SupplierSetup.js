//var SupplierList;

function loadTable_SupplierList(reqSupplierListUrl, authToken) {
    SupplierList = $("#Table_SupplierList").DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqSupplierListUrl,
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
                    "data": "SupplierName"
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
    loadTable_SupplierList(reqSupplierListUrl, authToken);

    $.contextMenu({
        selector: "#Table_SupplierList tbody tr",
        className: "css-title-toolbox",
        items: {
            "edit": {
                name: "Edit",
                icon: "fa-edit",
                callback: function (key, options) {

                    var rowData = SupplierList.row(options.$trigger).data();

                    var Supplier = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenSupplierID}`).val(rowData.ID)

                    $("#editSupplier").modal("show");

                    loadSupplierID(reqSupplierByIDUrl, authToken, Supplier)
                    .catch((error) => {
                        $("#editSupplier").modal("hide");
                        ShowError("Load Supplier Failed", "Fail");
                    })
                }
            },
            "delete": {
                name: "Delete",
                icon: "fa-solid fa-trash",
                callback: function (key, options) {

                    var rowData = SupplierList.row(options.$trigger).data();

                    var Supplier = {
                        ID: rowData.ID
                    }

                    $(`#${hiddenSupplierID}`).val(rowData.ID);

                    $("#deleteSupplier").modal("show");

                    loadSupplierID(reqSupplierByIDUrl, authToken, Supplier)
                    .catch((error) => {
                        $("#deleteSupplier").modal("hide");
                        ShowError("Load Role Details Failed", "Fail");
                    })
                }
            }
        }
    });

    $("#btnConfirmAddSupplier").click(function () {
        $("#addSupplier").submit();

        var dataStr = {
            SupplierName: $("#inputAddSupplierName").val(),
            Description: $("#inputAddSupplierDescription").val()
        }

        addSupplier(reqAddSupplierUrl, authToken, dataStr)
        .then((data) => {
            ShowPass("Add Supplier Success", data.Message || "Add successfully!");

            SupplierList.ajax.reload();
            ShowInfo("Supplier List Reload", "Supplier list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Add Supplier", error.responseText || error);
        })
        $("#addSupplier").modal("hide");

        $("input").val("");
    });

    $("#btnConfirmEditSupplier").click(function () {
        $("#editSupplier").submit();

        var dataStr = {
            ID: $(`#${hiddenSupplierID}`).val(),
            SupplierName: $("#InputEditSupplierName").val(),
            Description: $("#InputEditSupplierDescription").val(),
        }

        editSupplier(reqEditSupplierUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Edit Supplier Success", data.Message || "Add successfully!");

            SupplierList.ajax.reload();
            ShowInfo("Supplier List Reload", "Supplier list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Edit Supplier", error.responseText || error);
        })
        $("#editSupplier").modal("hide");
    });

    $("#btnYesDeleteSupplier").click(function () {
        $("#deleteSupplier").submit();

        var dataStr = {
            ID: $(`#${hiddenSupplierID}`).val(),
            DeletedRemark: $("#DeleteSupplierRemark").val()
        }
        $("#deleteSupplier").modal("hide");

        deleteSupplier(reqDeleteSupplierUrl, authToken, dataStr)
        .then((data) => {

            ShowPass("Delete Supplier Success", data.Message || "Delete successfully!");

            SupplierList.ajax.reload();
            ShowInfo("Supplier List Reload", "Supplier list has been reloaded with latest data.");
        })
        .catch((error) => {
            ShowError("Fail To Delete Supplier", error.responseText || error);
        })
        $("input").val("");
    });

    
    $("#btnAddSupplier").click(function () {
        $("#addSupplier").modal("show");
    });

    $(".btn-close")
    .add("#btnCancelAddSupplier")
    .add("#btnNoDeleteSupplier")
    .add("#btnCancelEditSupplier")
    .click(function () {
        $("#addSupplier").modal("hide");
        $("#editSupplier").modal("hide");
        $("#deleteSupplier").modal("hide");
        $("input").val("");
    });
})

function loadSupplierID(reqSupplierByIDUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqSupplierByIDUrl,
            type: "Post",
            dataType: "json",
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                $("#InputEditSupplierName").val(data.SupplierName);
                $("#InputEditSupplierDescription").val(data.Description || "");

                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
};

function addSupplier(reqAddSupplierUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqAddSupplierUrl,
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

function editSupplier(reqEditSupplierUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqEditSupplierUrl,
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

function deleteSupplier(reqDeleteSupplierUrl, authToken, dataStr) {
    return new Promise((resolve, error) => {
        $.ajax({
            url: reqDeleteSupplierUrl,
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